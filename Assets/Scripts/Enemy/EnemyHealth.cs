using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, Health
{
    [SerializeField] private GameObject enemyDeath;
    [SerializeField] private GameObject coinPrefab;
    private SpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    public void TakeDamage(float damage)
    {
        var enemyTransform = transform;
        GameManager.AddScore(100);
        Instantiate(enemyDeath, enemyTransform.position, enemyTransform.rotation);
        Instantiate(coinPrefab, enemyTransform.position, enemyTransform.rotation);
        spawnManager.RemoveEnemy(this.gameObject);
        Destroy(gameObject);
    }
}
