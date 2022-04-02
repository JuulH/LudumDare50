using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float initialSpawnFrequency;
    [SerializeField] private float difficultyIncreaseFrequency;
    private float _spawnTimer;


    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer > initialSpawnFrequency)
        {
            SpawnEnemy();
            _spawnTimer = 0;
        }
    }

    private void SpawnEnemy()
    {
        int randomSpawnIndx = Random.Range(0, spawnPoints.Count);
        Transform randomSpawnPoint = spawnPoints[randomSpawnIndx];
        Instantiate(enemy, randomSpawnPoint.position, enemy.transform.rotation);
    }
}
