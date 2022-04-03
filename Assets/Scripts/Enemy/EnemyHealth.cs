using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, Health
{
    [SerializeField] private GameObject enemyDeath;
    [SerializeField] private GameObject coinPrefab;

    public void TakeDamage(float damage)
    {
        var enemyTransform = transform;
        Instantiate(enemyDeath, enemyTransform.position, enemyTransform.rotation);
        Instantiate(coinPrefab, enemyTransform.position, enemyTransform.rotation);
        Destroy(gameObject);
    }
}
