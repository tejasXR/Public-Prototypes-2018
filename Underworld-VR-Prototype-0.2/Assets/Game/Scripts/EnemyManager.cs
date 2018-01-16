using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameManager gameManager;
    public float enemySpawnTimer; //The time in seconds in which an enemy spawns
    public int enemiesToSpawn;
    public GameObject enemyPrefab;

    private float enemySpawnTimerMin;
    private float enemySpawnTimerMax;



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

        Instantiate(enemyPrefab, transform.position, transform.rotation);
        //enemiesToSpawn -= 1;
        enemySpawnTimer = Random.Range(enemySpawnTimerMin, enemySpawnTimerMax);

    }

    void CheckWave()
    {
        switch (gameManager.wave)
        {
            case 1:
                enemySpawnTimerMin = 8f;
                enemySpawnTimerMax = 12f;
                //enemiesToSpawn = 10;
                break;
            case 2:
                enemySpawnTimerMin = 7f;
                enemySpawnTimerMax = 10f;
                //enemiesToSpawn = 15;
                break;
            case 3:
                enemySpawnTimerMin = 6f;
                enemySpawnTimerMax = 9f;
                //enemiesToSpawn = 20;
                break;
            case 4:
                enemySpawnTimerMin = 6f;
                enemySpawnTimerMax = 7f;
                //enemiesToSpawn = 25;
                break;
            case 5:
                enemySpawnTimerMin = 5f;
                enemySpawnTimerMax = 6f;
                //enemiesToSpawn = 30;
                break;
        }
    }
}
