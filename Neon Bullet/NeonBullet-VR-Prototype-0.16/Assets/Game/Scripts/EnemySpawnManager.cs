using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    private GameManager gameManager;
    private GameObject playerController;
    public EnemySpawnBoards[] enemySpawnBoards;

    public float enemySpawnTimer; //The time in seconds in which an enemy spawns

    //public Vector3 enemySpawnPos;

    //public GameObject enemySpawner;

    public bool spawnNow;
    public Vector3 enemySpawnPosition;

    public float enemySpawnTimerMin;
    public float enemySpawnTimerMax;

    //private float enemyDroneChance; //chance of drone double to be spawned;
    //private float enemyDroneDoubleChance; //chance of drone double to be spawned;
    //private float enemyBomberChance; //chance of drone double to be spawned;

    public float[] enemyProbability; // 0 = light, 1 = fast, 2 = heavy, 3 = bomber, 4 = leviathan, 5 = redemption
    //public GameObject[] enemyTypes; // 0 = light, 1 = fast, 2 = heavy, 3 = bomber, 4 = leviathan, 5 = redemption

    public bool redemptionResetTimer; //Used to reset the enemy timer for the redemption mode
    private int roundCount;

    //public bool isActive;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("PlayerController");
        //isActive = true;
        CheckRound();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.roundActive)// || gameManager.redemptionActive)
        {
            if (roundCount != gameManager.roundCurrent)
            {
                enemySpawnTimer = 7;
                roundCount = gameManager.roundCurrent;
            }

            if (!spawnNow)
            {
                enemySpawnTimer -= Time.deltaTime;
            }

            if (((gameManager.enemiesToSpawn - gameManager.enemiesDestroyed) > gameManager.enemiesOnScreen) && gameManager.enemiesOnScreen < gameManager.enemiesOnScreenMax && !spawnNow)
            {
                if (enemySpawnTimer <= 0)
                {
                    //spawnNow = true;
                    CheckRound();
                    SpawnEnemy();
                }
            }
        }

        // Resets the enemy spawn timer so that the timer isn't counting down from a previos round
        if (gameManager.redemptionActive && !redemptionResetTimer)
        {
            CheckRound();
            ResetSpawnTimer();
            redemptionResetTimer = true;
        }

        if (gameManager.roundStart)
        {
            // When the round starts, check the initialization variables, not every time an enemy is spawned
            CheckRound(); 
        }

    }

    void SpawnEnemy()
    {

        int enemyType = Mathf.RoundToInt(EnemyProbability(enemyProbability));
        int randomSide = Random.Range(0, 3);
        //print(enemy);

        //RandomPosition(); // removing since noe we spawn through the spawn boards

        // Spawns enemy spawner objects at the spawn positions
        if (!spawnNow)
        {
            //Instantiate(enemyTypes[enemy], enemySpawnPosition, Quaternion.identity);
            if (!enemySpawnBoards[randomSide].shouldFlicker)
            {
                enemySpawnBoards[randomSide].enemySpawnType = enemyType;
                enemySpawnBoards[randomSide].shouldFlicker = true;
                gameManager.enemiesOnScreen++;
                spawnNow = true;
                ResetSpawnTimer();

            }
            else
            {
                randomSide = Random.Range(0, 3);
            }
            
        }


        // OLD -->>Instantiate(enemySpawner, enemySpawnPos, Quaternion.identity);
        //enemySpawnTimer = Random.Range(enemySpawnTimerMin, enemySpawnTimerMax);

    }

    void CheckRound()
    {
        if (gameManager.roundActive)// && !gameManager.redemptionActive)
        {
            // Resets enemy probabilities
            ResetEnemyProbability();

            switch (gameManager.roundCurrent)
            {
                case 1:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 6f;

                    


                    enemyProbability[0] = 100; // Light drones
                   
                    break;
                case 2:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 6f;

                   

                    enemyProbability[0] = 100; // Light drones
                   
                    break;
                case 3:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 6f;

                  

                    enemyProbability[0] = 50; // Light drones
                    enemyProbability[1] = 50; // Fast Drones
                 
                    break;
                case 4:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    enemyProbability[0] = 33; // Light drones
                    enemyProbability[1] = 66; // Fast Drones
                   
                    break;

                case 5:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    enemyProbability[0] = 25; // Light drones
                    enemyProbability[1] = 75; // Fast Drones
                    
                    break;

                case 6:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    
                    enemyProbability[1] = 50; // Fast Drones
                    enemyProbability[2] = 50; // Heavy Drones

                    break;

                case 7:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    enemyProbability[0] = 30; // Light drones
                    enemyProbability[1] = 40; // Fast Drones
                    enemyProbability[2] = 30; // Heavy Drones


                    break;

                case 8:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    enemyProbability[0] = 16; // Light drones
                    enemyProbability[1] = 16; // Fast Drones
                    enemyProbability[2] = 16; // Heavy Drones
                    enemyProbability[3] = 50; // Heavy Drones
                    break;

                case 9:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    enemyProbability[0] = 10; // Light drones
                    enemyProbability[1] = 10; // Fast Drones
                    enemyProbability[2] = 40; // Heavy Drones
                    enemyProbability[3] = 40; // Heavy Drones
                    break;

                case 10:
                    enemySpawnTimerMin = 4f;
                    enemySpawnTimerMax = 5f;

                    enemyProbability[0] = 10; // Light drones
                    enemyProbability[1] = 10; // Fast Drones
                    enemyProbability[2] = 40; // Heavy Drones
                    enemyProbability[3] = 40; // Heavy Drones
                    break;
            }
        }

        if (gameManager.redemptionActive)
        {
            ResetEnemyProbability();

            enemySpawnTimerMin = .1f;
            enemySpawnTimerMax = .5f;

            //enemyProbability[0] = 0; // Light drones
            //enemyProbability[1] = 0; // Fast Drones
            //enemyProbability[2] = 0; // Heavy Drones
            enemyProbability[5] = 100; // Redemption Drone
        }
        
    }

    void ResetEnemyProbability()
    {
        enemyProbability[0] = 0; // Light drones
        enemyProbability[1] = 0; // Fast Drones
        enemyProbability[2] = 0; // Heavy Drone
        enemyProbability[3] = 0; // Bomber Drone
        enemyProbability[4] = 0; // Leviathan Drone
        enemyProbability[5] = 0; // Redemption Drone
    }

    void ResetSpawnTimer()
    {
        enemySpawnTimer = Random.Range(enemySpawnTimerMin, enemySpawnTimerMax);
        spawnNow = false;
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
        if (!spawnNow)
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

            // if there are no enemies with a 2 unit radius
            if (!Physics.CheckSphere(enemySpawnPosition, 2f))
            {
                spawnNow = true;
                ResetSpawnTimer();
            }
        }
        
    }
}
