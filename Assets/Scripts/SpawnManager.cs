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

    [SerializeField] private int enemiesAmount;
    private int spawnedEnemies;

    private List<GameObject> activeEnemies;

    private float _spawnFrequency;
    private float _spawnTimer;
    private float _difficultyIncreaseTimer;

    private GameObject spawnedEnemy;

    [SerializeField] GameManager gameManager;

    private void Start()
    {
        activeEnemies = new List<GameObject>();
        _spawnFrequency = initialSpawnFrequency;
        currentSpawnFreqText.text = "" + _spawnFrequency;
    }


    // Update is called once per frame
    void Update()
    {
        if(spawnedEnemies < enemiesAmount)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer > _spawnFrequency)
            {
                SpawnEnemy();
                spawnedEnemies += 1;
                _spawnTimer = 0;
            }
            //Debug.Log(activeEnemies.Count + " - " + enemiesAmount);
        }

        /*_difficultyIncreaseTimer += Time.deltaTime;
        nextDifficultyIncreaseInText.text = "" + (difficultyIncreaseFrequency - _difficultyIncreaseTimer);
        if (_difficultyIncreaseTimer > difficultyIncreaseFrequency)
        {
            IncreaseDifficulty();
            _difficultyIncreaseTimer = 0;
        }*/
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
        spawnedEnemy = Instantiate(enemy, randomSpawnPoint.position, enemy.transform.rotation);
        activeEnemies.Add(spawnedEnemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        if (spawnedEnemies == enemiesAmount && activeEnemies.Count == 0)
        {
            gameManager.WaveComplete();
        }
        //Debug.Log(activeEnemies.Count);
    }
}