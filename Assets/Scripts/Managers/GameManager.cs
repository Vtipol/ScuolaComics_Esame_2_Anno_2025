using System.Runtime.Serialization;
using System.Collections.Generic;
using DesignPatterns.Generics;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [Header("Monete del giocatore")]
    [SerializeField] private int startingCoins = 100;
    private GameObject Enemy;
    private EnemyController enemyController;
    private UIManager UImanager;
    //private List<Transform> Paths;
    // private GameObject Path;
    // private EnemyController enemyController;
    private int currentCoins;

    public int CurrentCoins => currentCoins;

    public override void Awake()
    {
        base.Awake();
        currentCoins = startingCoins;
    }
    private void Start()
    {
        UImanager = FindAnyObjectByType<UIManager>();
        GetEnemies();
    }
    private void GetEnemies() // funzione per trovare enemy se non già instanziati in scena
    {
        Enemy = GameObject.Find("Enemy"); // ha bisogno di un nemico già instanziato
       // Path = GameObject.Find("Path");
        if (Enemy == null  /* Path != null*/)
        {
            GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
            if (enemyPrefab != null)
            {
                Enemy = enemyPrefab;
                Enemy.name = "Enemy";
               // enemyController = FindAnyObjectByType<EnemyController>();
            }
            else Debug.LogError("Enemy Was not Found");
        }
        enemyController = Enemy.GetComponent<EnemyController>();
       /* if (Path == null) // rimosso a causa di mancanza di tempo
        {
            Debug.LogWarning("there is no Path in the current scene");
        }
        else
        {
            foreach (Transform Child in Path.transform)
            {
                Paths.Add(Child);
            }
            enemyController.pathPoints = Paths;
        }*/
    }
    public void AddCoins(int amount)
    {
        // DONE: Chiamato quando un nemico viene distrutto nel EnemyController

        currentCoins += amount;
        Debug.Log($"Aggiunti {amount} coins. Coins totali: {currentCoins}");
        UImanager.UI_Coin.Invoke();
        // TODO: Aggiungere un evento qui per aggiornare l'UI
    }
    
    // Metodo per spendere monete
    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            Debug.Log($"Spesi {amount} coins. Coins rimasti: {currentCoins}");
            UImanager.UI_Turret.Invoke();
            // TODO: Aggiungere un evento qui per aggiornare l'UI
            return true;
        }
        else
        {
            Debug.LogWarning("Non hai abbastanza monete!");
            // TODO: Aggiungere un evento qui per aggiornare l'UI
            return false;
        }
    }
}
