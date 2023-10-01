using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isSpawning = false;

    public GameObject enemyPrefab;

    public float spawnRate = 1f;

    public BoxCollider2D spawnArea;
    public float spawnRateIncrease;
    public float healthIncrease;
    public float speedIncrease;
    public float startDelay;
    public float delayCount;

    private float timeSinceLastSpawn;
    private float timeSinceLastIncrease;
    private Timer timer;

    private void Awake()
    {
        delayCount = 0;
        timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        
        if (isSpawning)
        {
            timeSinceLastSpawn += Time.deltaTime;
            timeSinceLastIncrease += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnRate)
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0;
            }
            if(timeSinceLastIncrease >= 60f)
            {
                if (spawnRate <= spawnRateIncrease)
                {
                    spawnRate = 0.01f;
                   
                }
                else
                {
                    spawnRate -= spawnRateIncrease;
                    
                }
                timeSinceLastIncrease = 0;

            }
        }

        if (delayCount >= startDelay)
        {
            isSpawning = true;
        }
        else
        {
            delayCount += Time.deltaTime;
        }
       
    }

    private void SpawnEnemy()
    {
        // Spawn an enemy
        GameObject enemy = Instantiate(enemyPrefab);

        // Set the enemy's position to a random position within the spawn area
        enemy.transform.position = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            0
        );
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}