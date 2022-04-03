using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, Health
{
    [SerializeField] private GameObject enemyDeath;

    public void TakeDamage(float damage)
    {
        var enemyTransform = transform;
        Instantiate(enemyDeath, enemyTransform.position, enemyTransform.rotation);
        Destroy(gameObject);
    }
}
