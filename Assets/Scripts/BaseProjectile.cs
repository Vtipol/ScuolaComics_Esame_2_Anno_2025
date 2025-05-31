using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float speed = 1;
    [SerializeField] private float LifeTime = 5f;
    private void Start()
    {
        ProjectileLife();
    }
    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(LifeTime);
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }
}
