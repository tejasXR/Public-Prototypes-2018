using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameManager gameManager;
    public float enemySpawnTimer; //The time in seconds in which an enemy spawns
    //public int enemiesToSpawn;
    
    //public GameObject nextEnemySpawned; //The next enemy to be spawned by the spawner;


    public Transform enemySpawnPosition;

    public float enemySpawnTimerMin;
    public float enemySpawnTimerMax;

    //private float enemyDroneChance; //chance of drone double to be spawned;
    //private float enemyDroneDoubleChance; //chance of drone double to be spawned;
    //private float enemyBomberChance; //chance of drone double to be spawned;

    public float[] enemyProbability; // 0 = single, 1 = double, 2 = bomber
    public GameObject[] enemyTypes; // 0 = single, 1 = double, 2 = bomber







    //public bool isActive;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //isActive = true;
        CheckWave();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.waveActive)
        {
            enemySpawnTimer -= Time.deltaTime;

            if (enemySpawnTimer <= 0)
            {
                SpawnEnemy();
            }
        }

        

        /*if (enemiesToSpawn == 0)
        {
            CheckWave();
        }*/

    }

    void SpawnEnemy()
    {
        CheckWave();
        int enemy = Mathf.RoundToInt(EnemyProbability(enemyProbability));
        print(enemy);

        Instantiate(enemyTypes[enemy], enemySpawnPosition.transform.position, enemySpawnPosition.transform.rotation);
        enemySpawnTimer = Random.Range(enemySpawnTimerMin, enemySpawnTimerMax);

    }

    void CheckWave()
    {
        switch (gameManager.wave)
        {
            case 1:
                enemySpawnTimerMin = 8f;
                enemySpawnTimerMax = 12f;

                enemyProbability[0] = 100; // Single drones
                enemyProbability[1] = 0; // Double Drones
                enemyProbability[2] = 0; // Bombers
                break;
            case 2:
                enemySpawnTimerMin = 7f;
                enemySpawnTimerMax = 10f;

                enemyProbability[0] = 80; // Single drones
                enemyProbability[1] = 20; // Double Drones
                enemyProbability[2] = 0; // Bombers
                break;
            case 3:
                enemySpawnTimerMin = 6f;
                enemySpawnTimerMax = 9f;

                enemyProbability[0] = 50; // Single drones
                enemyProbability[1] = 50; // Double Drones
                enemyProbability[2] = 0; // Bombers
                break;
            case 4:
                enemySpawnTimerMin = 6f;
                enemySpawnTimerMax = 7f;

                enemyProbability[0] = 30; // Single drones
                enemyProbability[1] = 70; // Double Drones
                enemyProbability[2] = 0; // Bombers
                break;
            case 5:
                enemySpawnTimerMin = 5f;
                enemySpawnTimerMax = 6f;

                enemyProbability[0] = 10; // Single drones
                enemyProbability[1] = 50; // Double Drones
                enemyProbability[2] = 40; // Bombers
                break;
        }
    }

    float EnemyProbability (float[] probs)
    {
        float total = 0;

        foreach(float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i <probs.Length; i++)
        {
            if (randomPoint <= probs[i])
            {
                return i;
            } else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }
}
