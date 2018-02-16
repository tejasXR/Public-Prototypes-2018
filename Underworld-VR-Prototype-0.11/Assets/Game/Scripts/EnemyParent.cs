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
    //public float enemyRedemptionPoints;

    //Enemy On Destroy
    public GameObject explosionPrefab; //the explosion effect when destroyed
    public GameObject explosionTextObj; //the text object that tells players how many bullets they've earned
    //public TextMeshPro[] explosionText;
    private float enemyDestroyTimer = 2f;

    public bool isRedemptionDrone;

    public GameObject[] meshToChangeOnFlash;
    private Material[] meshRendererOriginals;
    public Material enemyFlashMat;
    public float enemyFlashHitDuration;

    //public AudioSource explosionSound;
    public AudioSource enemyHitSound;

    //private float explosionSoundPitchOriginal;
    private float enemyHitSoundPitchOriginal;

    //private float enemyFlashHitCounter;

    public bool flash;



    void Start () {

        //Define player variables for enemy prefab
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawnManager = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();

        if (!isRedemptionDrone)
        {
            explosionTextObj.GetComponent<EarnedBulletText>().bulletNumber = enemyGiveBullets;
        }

        //explosionSoundPitchOriginal = explosionSound.pitch;
        enemyHitSoundPitchOriginal = enemyHitSound.pitch;


        /*if (!isRedemptionDrone)
        {
            foreach(TextMeshPro text in explosionText)
            {
                text.text = "+ " + enemyGiveBullets.ToString();

            }
        }*/

        // Sets all the mesh renderers stored safely in another array
        meshRendererOriginals = new Material[meshToChangeOnFlash.Length];
        for(int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshRendererOriginals[i] = meshToChangeOnFlash[i].GetComponent<Renderer>().material;
        }

        //enemyHealth -= (enemyHealth * playerController.enemyNegativeHealthMultiplier);
    }

    private void Update()
    {
        if (gameManager.inRedemption && !isRedemptionDrone)
        {
            EnemyDestroyNoBullets();
        }

        // Slow Down Audio
        enemyHitSound.pitch = Mathf.Lerp(enemyHitSound.pitch,  enemyHitSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);
        //explosionSound.pitch = Mathf.Lerp(explosionSound.pitch, explosionSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);



        /*if (flash)
        {
            StartCoroutine(EnemyHitFlash());
            flash = false;
        }*/
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "DeflectedBullet")
        {
            enemyHitSound.pitch = enemyHitSound.pitch + Random.Range(-.05f, .05f);
            enemyHitSound.Play();

            StartCoroutine(EnemyHitFlash());

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
                if (healthRegenChance <= playerController.playerHealthChance)
                {
                    playerController.playerHealth++;
                }

                damage = other.gameObject.GetComponent<Bullet>().damage;
            } else if (other.gameObject.tag == "DeflectedBullet")
            {
                damage = other.gameObject.GetComponent<EnemyBullet>().damage;
            }
            Destroy(other.gameObject);
            enemyHealth -= damage;            

            if (enemyHealth <= 0)
            {
                //print("enemyDestroyCalled");
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

    IEnumerator EnemyHitFlash()
    {
        for (int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshToChangeOnFlash[i].GetComponent<Renderer>().material = enemyFlashMat;
        }
        yield return new WaitForSeconds(enemyFlashHitDuration);
        for (int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshToChangeOnFlash[i].GetComponent<Renderer>().material = meshRendererOriginals[i];
        }

    }

    void EnemyDestroy()
    {
        if (enemyGiveBullets > 0)
        {
            playerController.playerBullets += enemyGiveBullets + playerController.enemyGiveAdditionalBullets;
            Instantiate(explosionTextObj, transform.position, transform.rotation);
        }

        if (gameManager.redemptionActive)
        {
            Instantiate(explosionTextObj, transform.position, transform.rotation);
            //gameManager.redemptionMeter = gameManager.redemptionMeterMax; // Fill up the redemption meter so it can properly count down again
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        gameManager.enemiesOnScreen--;
        gameManager.enemiesDestroyed++;

        //explosionSound.pitch = explosionSound.pitch + Random.Range(-.05f, .05f);
        //explosionSound.Play();
    }

    public void DisappearAfterWave()
    {
        enemyDestroyTimer -= Time.deltaTime;
        if (enemyDestroyTimer <= 0)
        {
            //gameManager.enemiesOnScreen--;
            Destroy(this.gameObject);
        }

    }

    public void BomberDestroy()
    {
        gameManager.enemiesOnScreen--;



        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

    void EnemyDestroyNoBullets()
    {
        //Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);

        //gameManager.enemiesOnScreen--;
        //gameManager.enemiesDestroyed++;
    }


}
