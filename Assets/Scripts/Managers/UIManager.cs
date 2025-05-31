using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [Header("Turret Buttons")]
    public List<TurretButton> turretButtons;
    [SerializeField] TextMeshProUGUI playerCoins;
    private GameManager gameManager;
   public UnityEvent UI_Coin;
    public UnityEvent UI_Turret;
    private void Start()
    {
        if (UI_Coin != null) UI_Coin = new UnityEvent();
        UI_Coin.AddListener(UpdateCoinUI);
        if (UI_Coin != null) UI_Turret = new UnityEvent();
        UI_Turret.AddListener(UpdateTurretUI);
        UpdateTurretButtons();
    }

    private void Update()
    {
        // Controlla e aggiorna i pulsanti ogni frame (opzionale, ma semplice)
        // TODO: si potrebbe gestire meglio usando i DesignPattern...
        UpdateTurretButtons();
       // UpdatePlayerCoins();
    }
    void UpdateCoinUI()
    {
        playerCoins.text = $"{GameManager.Instance.CurrentCoins}";
    }
     void UpdateTurretUI()
    {
        int playerCoins = GameManager.Instance.CurrentCoins;

        foreach (var button in turretButtons)
        {
            button.UpdateButtonState(playerCoins);
        }
    }
    /*
    private void UpdatePlayerCoins()
    {
        playerCoins.text = $"{GameManager.Instance.CurrentCoins}";
    }
    */
    public void UpdateTurretButtons()
    {
        int playerCoins = GameManager.Instance.CurrentCoins;

        foreach (var button in turretButtons)
        {
            button.UpdateButtonState(playerCoins);
        }
    }
    
}
