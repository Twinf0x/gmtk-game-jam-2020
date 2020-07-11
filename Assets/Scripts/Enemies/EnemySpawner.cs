using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemySpawnData[] spawnOptions;
    [SerializeField] private Transform playerTransform;

    [Header("Settings")]
    [SerializeField] private float playerDistanceThreshold = 2f;
    [SerializeField] private float maxSpawnTimer = 5f;
    [SerializeField] private float timerMultiplicator = 0.95f;

    private int totalWeight = 0;
    private float spawnTimer;

    private void Start()
    {
        foreach(var data in spawnOptions)
        {
            totalWeight += data.weight;
        }

        spawnTimer = Random.Range(0f, maxSpawnTimer);
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(Vector3.Distance(transform.position, playerTransform.position) < playerDistanceThreshold)
        {
            return;
        }

        if(spawnTimer <= 0f)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var weightSelector = Random.Range(0, totalWeight);

        foreach(var data in spawnOptions)
        {
            if(weightSelector > data.weight)
            {
                weightSelector -= data.weight;
                continue;
            }

            var enemyObject = Instantiate(data.prefab, transform.position, Quaternion.identity);
            var enemy = enemyObject.GetComponent<EnemyMouse>();
            enemy.SetTarget(playerTransform);
            break;
        }

        maxSpawnTimer *= timerMultiplicator;
        spawnTimer = maxSpawnTimer;
    }
}
