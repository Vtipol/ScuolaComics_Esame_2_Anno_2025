using UnityEngine;

public class PlayerBaseHP : MonoBehaviour
{
    [SerializeField] private float playerHP = 5;
    [SerializeField] private EnemyController enemyController;

    // cambiato che possa solo interagire con enemy cambiando il layer collision matrix nel project setings
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyController.ReachExit();
        playerHP -= enemyController.damageToPlayer;
    }
}
