using UnityEngine;

public class PlayerBaseHP : MonoBehaviour
{
    [SerializeField] private float playerHP = 5;
    private EnemyController enemyController;
    private void Awake()
    {
        enemyController = FindAnyObjectByType<EnemyController>();
    }
    // cambiato che possa solo interagire con enemy cambiando il layer collision matrix nel project setings
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyController.ReachExit();
        playerHP -= enemyController.damageToPlayer;
    }
}
