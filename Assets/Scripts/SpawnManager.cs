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
    [SerializeField] private float difficultyIncreaseModifier;
    [SerializeField] private TextMeshProUGUI currentSpawnFreqText;

    [SerializeField] private int initialEnemiesAmount;
    private int _enemiesAmount;
    private int _spawnedEnemies;

    private List<GameObject> _activeEnemies;

    private float _spawnFrequency;
    private float _spawnTimer;
    private float _difficultyIncreaseTimer;
    private int _waveNumber;

    private GameObject _spawnedEnemy;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        _activeEnemies = new List<GameObject>();
        _spawnFrequency = initialSpawnFrequency;
        currentSpawnFreqText.text = "" + _spawnFrequency;
        _enemiesAmount = initialEnemiesAmount;
        _waveNumber = 1;
    }

    void Update()
    {
        if(_spawnedEnemies < _enemiesAmount)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer > _spawnFrequency)
            {
                SpawnEnemy();
                _spawnedEnemies += 1;
                _spawnTimer = 0;
            }
        }
    }

    private void IncreaseDifficulty()
    {
        _waveNumber++;
        _spawnFrequency *= difficultyIncreaseModifier;
        _enemiesAmount += initialEnemiesAmount * _waveNumber;
        currentSpawnFreqText.text = "" + _spawnFrequency;
    }

    private void SpawnEnemy()
    {
        int randomSpawnIndx = Random.Range(0, spawnPoints.Count);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndx];
        _spawnedEnemy = Instantiate(enemy, randomSpawnPoint.position, enemy.transform.rotation);
        _activeEnemies.Add(_spawnedEnemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _activeEnemies.Remove(enemy);
        if (_spawnedEnemies == _enemiesAmount && _activeEnemies.Count == 0)
        {
            gameManager.WaveComplete(_waveNumber);
            IncreaseDifficulty();
        }
    }
}