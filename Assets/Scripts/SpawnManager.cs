using System;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TextMeshProUGUI nextDifficultyIncreaseInText;
    [SerializeField] private TextMeshProUGUI currentSpawnFreqText;

    private float _spawnFrequency;
    private float _spawnTimer;
    private float _difficultyIncreaseTimer;

    private void Start()
    {
        _spawnFrequency = initialSpawnFrequency;
        currentSpawnFreqText.text = "" + _spawnFrequency;
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
        nextDifficultyIncreaseInText.text = "" + (difficultyIncreaseFrequency - _difficultyIncreaseTimer);
        if (_difficultyIncreaseTimer > difficultyIncreaseFrequency)
        {
            IncreaseDifficulty();
            _difficultyIncreaseTimer = 0;
        }
    }

    private void IncreaseDifficulty()
    {
        _spawnFrequency *= difficultyIncreaseModifier;
        currentSpawnFreqText.text = "" + _spawnFrequency;
    }

    private void SpawnEnemy()
    {
        int randomSpawnIndx = Random.Range(0, spawnPoints.Count);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndx];
        Instantiate(enemy, randomSpawnPoint.position, enemy.transform.rotation);
    }
}