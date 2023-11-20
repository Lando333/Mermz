using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //List of groups of enemies to spawn this wave
        public int waveQuota;       //Total number of enemies to spawn this wave
        public float spawnInterval; //Interval between waves
        public int spawnCount;      
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;          //Number of enemies to spawn for current waves
        public int spawnCount;          //Number of enemies already spawned this wave
        public GameObject enemyPrefab;  //Enemies for current wave
    }

    public List<Wave> waves;        //List of all waves in game
    public int currentWaveCount;    //Starts at wave 0

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;   //Max allowed on the map
    public bool maxEnemiesReached = false;
    public float waveInterval;  // Time between each wave
    bool isWaveActive = false;

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints;     // Enemy spawn positions

    Transform player; 
    
    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();
    }

    void Update()
    {
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;

        // Check if its time to spawn next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        isWaveActive = true;

        yield return new WaitForSeconds(waveInterval);

        // If there are more waves to start after the current wave, move on to next wave
        if (currentWaveCount < waves.Count -1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //print(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        // Check if min number of enemies in wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            // Spawn each type of enemy uptil the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // Check if min number of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    // Spawn enemy at random spawn point close to player
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    // Limit number of enemies spawned
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }
    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;

        // Resets max enemies flag if number of enemies drops below max
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
}
