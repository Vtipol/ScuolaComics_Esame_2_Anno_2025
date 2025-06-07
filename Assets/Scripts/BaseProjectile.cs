using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] protected float speed = 1;
    public ObjectPooler<BaseProjectile> pool;
    [SerializeField] private float LifeTime = 5f;
    private void Start()
    {
        pool.Get();
        StartCoroutine(ProjectileLife());
    }
    private IEnumerator ProjectileLife()
    {
        yield return new WaitForSeconds(LifeTime);
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }
}
