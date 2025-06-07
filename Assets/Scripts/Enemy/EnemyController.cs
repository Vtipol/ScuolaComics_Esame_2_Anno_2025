using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] public List<Transform> pathPoints;
    private int currentPointIndex = 0;
    private Transform startingPosition;

    [Header("Movement")]
    [SerializeField] float speed = 2f;

    [Header("Health")]
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float currentHealth;
    [SerializeField] GameObject canvasLife;
    [SerializeField] Image lifeBar;

    [Header("Damage")]
    [SerializeField] public int damageToPlayer = 1;

    [Header("Graphics")]
    [SerializeField] SpriteRenderer graphicsObject;
    [Header("Value")]
    [SerializeField] int ScrapValue = 50;
    // DONE: Utilizza rb.linearvelocity e incrementata drasticamente la velocità
    private Rigidbody2D rb;
    private GameManager gameManager;
    private void Start()
    {
        currentHealth = maxHealth;
        startingPosition = GameObject.Find("EnemySpawnPoint - FACTORY").transform;
        gameManager = FindAnyObjectByType<GameManager>();
        if (startingPosition == null) Debug.LogWarning("There is no startingPosition");
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (pathPoints == null || pathPoints.Count == 0) return;

        Vector3 targetPoint = pathPoints[currentPointIndex].position;
        Vector3 direction = (targetPoint - transform.position).normalized;

        rb.linearVelocity = direction * speed * Time.deltaTime;

        UpdateGraphicsRotation(direction);

        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= pathPoints.Count)
                ReachExit();
        }
    }

    private void UpdateGraphicsRotation(Vector3 direction)
    {
        if (graphicsObject == null) return;

        float angle;
        bool horizontal = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

        if (horizontal)
        {
            if (direction.x > 0f)
            {
                angle = 90f;
                graphicsObject.flipY = true;
            }
            else
            {
                angle = -90f;
                graphicsObject.flipY = true;
            }
        }
        else
        {
            if (direction.y > 0f)
            {
                angle = 0f;
            }
            else
            {
                angle = 180f;
            }
            graphicsObject.flipY = false;
        }

        graphicsObject.transform.localEulerAngles = new Vector3(0f, 0f, angle);
    }

    public void ReachExit()
    {
        Vector3 startingPos = startingPosition.position;
        if (startingPos == null) { Debug.Log("there is no starting position"); return; }
        transform.position = startingPos;
        currentHealth = maxHealth;
        currentPointIndex = 0;
    }
    public void OnDeath()
    {
        gameManager.AddCoins(ScrapValue);
        Vector3 startingPos = startingPosition.position;
        if (startingPos == null) { Debug.Log("there is no starting position"); return; }
        transform.position = startingPos;
        currentHealth = maxHealth;
        currentPointIndex = 0;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (!canvasLife.activeSelf)
            canvasLife.SetActive(true);

        lifeBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0f) Die();
    }

    private void Die()
    {
        // DONE: Invece di eliminarlo ritorna nella posizione iniziale
        // TODO: logica per Abilitare e disabilitare Nemici
        OnDeath();
        //Destroy(gameObject);
    }

    internal void Initialize(Path path)
    {
        pathPoints = new List<Transform>(path.GetComponentsInChildren<Transform>());
    }
}
