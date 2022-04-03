using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float initialSpawnFrequency;
    [SerializeField] private float difficultyIncreaseFrequency;
    [SerializeField] private float difficultyIncreaseModifier;

    private float _spawnFrequency;
    private float _spawnTimer;
    private float _difficultyIncreaseTimer;

    private void Start()
    {
        _spawnFrequency = initialSpawnFrequency;
    }


    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > _spawnFrequency)
        {
            SpawnEnemy();
            _spawnTimer = 0;
        }

        _difficultyIncreaseTimer += Time.deltaTime;
        if (_difficultyIncreaseTimer > difficultyIncreaseFrequency)
        {
            IncreaseDifficulty();
            _difficultyIncreaseTimer = 0;
        }
    }

    private void IncreaseDifficulty()
    {
        _spawnFrequency *= difficultyIncreaseModifier;
    }

    private void SpawnEnemy()
    {
        int randomSpawnIndx = Random.Range(0, spawnPoints.Count);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndx];
        Instantiate(enemy, randomSpawnPoint.position, enemy.transform.rotation);
    }
}
