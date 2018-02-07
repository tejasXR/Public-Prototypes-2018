using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyParent : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public EnemySpawnManager enemySpawnManager;

    //Define Basic Player Variables
    private Player playerController; //Whole of player object
    [HideInInspector] public GameObject player; //cameraEye object
    //public Vector3 playerDirection; //Direction towards the player

    //Define Basic Enemy Variables
    public float enemyHealth;
    public float enemyGiveBullets; //Amount of bullets enemy gives to player after it is destroyed
    public float enemyRedemptionPoints;

    //Enemy On Destroy
    public GameObject explosionPrefab; //the explosion effect when destroyed
    public GameObject earnedBulletObj; //the text object that tells players how many bullets they've earned
    public TextMeshPro earnedBulletText;
    private float enemyDestroyTimer = 2f;



    void Start () {

        //Define player variables for enemy prefab
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawnManager = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();

        earnedBulletText.text = "+ " + enemyGiveBullets.ToString();

        //enemyHealth -= (enemyHealth * playerController.enemyNegativeHealthMultiplier);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "DeflectedBullet")
        {
            float damage = 0;

            if (other.gameObject.tag == "Bullet")
            {
                // Add chance for critical hit, one-hit kill
                /*float criticalHit = Random.Range(0f, 1f);
                if (criticalHit > playerController.playerBulletCriticalHitChance)
                {
                    damage = enemyHealth;
                }*/

                // Add chance for health to regenerate by a point
                float healthRegenChance = Random.Range(0f, 1f);
                if (healthRegenChance > playerController.playerHealthChance)
                {
                    playerController.playerHealth++;
                }

                damage = other.gameObject.GetComponent<Bullet>().damage;
            } else if (other.gameObject.tag == "DeflectedBullet")
            {
                damage = other.gameObject.GetComponent<EnemyBullet>().damage;
            }
            enemyHealth -= damage;            

            if (enemyHealth <= 0)
            {
                EnemyDestroy();
                Destroy(this.gameObject);
            }
        }
    }

    /*void RandomPosition()
    {
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
        targetPosition = ray.GetPoint(Random.Range(4f, 7f));
    }*/

    //If destroyed by a bullet, explode into shiny things and give the player bullets
    void EnemyDestroy()
    {
        if (enemyGiveBullets > 0)
        {
            playerController.playerBullets += enemyGiveBullets + playerController.enemyGiveAdditionalBullets;
            Instantiate(earnedBulletObj, transform.position, transform.rotation);
        }

        if (enemyRedemptionPoints > 0)
        {
            gameManager.redemptionMeter += enemyRedemptionPoints;
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        gameManager.enemiesOnScreen--;
        gameManager.enemiesDestroyed++;
    }

    public void DisappearAfterWave()
    {
        enemyDestroyTimer -= Time.deltaTime;
        if (enemyDestroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void BomberDestroy()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
