using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class wave
    {
        public string waveNume;
        public int waveQuota; //The total number of enemines in the wave
        public List<EnemyGroup> enemyGroup;
        public float spawnInterval; // The interval to which to spawn enemies
        public int spawnCount; // The current number of spawn enemy
    }

    public List<wave> waves;

    public int currentWave;

    Transform Player;

    [Header("Spawner Attribute")]
    float spawnTimer;// Timer use to determine when to spawn
    public float waveInterval; // The interval between each wave

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName; //name
        public int enemyCount; // total count
        public int spawnCount; // curr count
        public GameObject enemyprefab; // prefab
    }

    void calcuateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWave].enemyGroup)
        {
             currentWaveQuota += enemyGroup.enemyCount;

        }
        waves[currentWave].waveQuota = currentWaveQuota;
    }
    void Start()
    {
        Player = FindObjectOfType<PlayerStats>().transform;
        calcuateWaveQuota();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWave < waves.Count && waves[currentWave].spawnCount == 0) // if wave is new 
        {
            StartCoroutine(BeginNextWave());
        }

        spawnTimer += Time.deltaTime;
        if(spawnTimer >= waves[currentWave].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
        if(currentWave == waves.Count - 1)
        {
            
        }
        
    }

    IEnumerator BeginNextWave()
    {
        //Wave for wave interval sconds before starting next wave
        yield return new WaitForSeconds(waveInterval);
        if(currentWave < waves.Count - 1)
        {
            currentWave++;
            calcuateWaveQuota();
        }
        
    }
    void SpawnEnemies()
    {
        if(waves[currentWave].spawnCount < waves[currentWave].waveQuota)
        {
            foreach(var enemyGroup in waves[currentWave].enemyGroup)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount) {

                    Vector2 spawnPos = new Vector2(Player.transform.position.x + Random.Range(-10, 10f), Player.transform.position.y + Random.Range(-10f, 10f));
                    Instantiate(enemyGroup.enemyprefab, spawnPos, Quaternion.identity);

                    waves[currentWave].spawnCount++;
                    enemyGroup.spawnCount++;
                }
            }

        }
    }



}
