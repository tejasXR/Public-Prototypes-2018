using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject playerController;
    public float enemySpawnTimer; //The time in seconds in which an enemy spawns

    //public Vector3 enemySpawnPos;

    //public GameObject enemySpawner;

    public Vector3 enemySpawnPosition;

    public float enemySpawnTimerMin;
    public float enemySpawnTimerMax;

    //private float enemyDroneChance; //chance of drone double to be spawned;
    //private float enemyDroneDoubleChance; //chance of drone double to be spawned;
    //private float enemyBomberChance; //chance of drone double to be spawned;

    public float[] enemyProbability; // 0 = light, 1 = fast, 2 = heavy, 3 = bomber
    public GameObject[] enemyTypes; // 0 = light, 1 = fast, 2 = heavy, 3 = bomber

    //public bool isActive;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("PlayerController");
        //isActive = true;
        CheckWave();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.roundActive)
        {
            enemySpawnTimer -= Time.deltaTime;

            if (enemySpawnTimer <= 0)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        CheckWave();
        int enemy = Mathf.RoundToInt(EnemyProbability(enemyProbability));
        //print(enemy);

        RandomPosition();

        // Spawns enemy spawner objects at the spawn positions
        Instantiate(enemyTypes[enemy], enemySpawnPosition, Quaternion.identity);


        // OLD -->>Instantiate(enemySpawner, enemySpawnPos, Quaternion.identity);
        enemySpawnTimer = Random.Range(enemySpawnTimerMin, enemySpawnTimerMax);
    }

    void CheckWave()
    {
        switch (gameManager.roundCurrent)
        {
            case 1:
                enemySpawnTimerMin = 8f;
                enemySpawnTimerMax = 12f;

                enemyProbability[0] = 100; // Light drones
                enemyProbability[1] = 0; // Fast Drones
                enemyProbability[2] = 0; // Heavy Drones
                break;
            case 2:
                enemySpawnTimerMin = 7f;
                enemySpawnTimerMax = 10f;

                enemyProbability[0] = 80; // Light drones
                enemyProbability[1] = 20; // Fast Drones
                enemyProbability[2] = 0; // Heavy Drones
                break;
            case 3:
                enemySpawnTimerMin = 6f;
                enemySpawnTimerMax = 9f;

                enemyProbability[0] = 50; // Light drones
                enemyProbability[1] = 50; // Fast Drones
                enemyProbability[2] = 0; // Heavy Drones
                break;
            case 4:
                enemySpawnTimerMin = 6f;
                enemySpawnTimerMax = 7f;

                enemyProbability[0] = 30; // Light drones
                enemyProbability[1] = 70; // Fast Drones
                enemyProbability[2] = 0; // Heavy Drones
                break;
            case 5:
                enemySpawnTimerMin = 5f;
                enemySpawnTimerMax = 6f;

                enemyProbability[0] = 10; // Light drones
                enemyProbability[1] = 50; // Fast Drones
                enemyProbability[2] = 40; // Heavy Drones
                break;
        }
    }

    float EnemyProbability(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint <= probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }

    public void RandomPosition()
    {
        // Creates a random position within a raycast range to spawn the triangle enemy spawner
        Vector3 randomPosition;

        int coinFlip = Random.Range(0, 2);
        if (coinFlip == 0)
        {
            randomPosition = new Vector3(Random.Range(-1f, -.5f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
        }
        else
        {
            randomPosition = new Vector3(Random.Range(.5f, 1f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
        }

        Ray ray = new Ray(playerController.transform.position, randomPosition);
        enemySpawnPosition = ray.GetPoint(Random.Range(10f, 12f));
    }
}
