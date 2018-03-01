using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyParent : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public EnemySpawnManager enemySpawnManager;
    [HideInInspector] public EnemyEffectsManager enemyEffectsManager;

    //Define Basic Player Variables
    private Player playerController; //Whole of player object
    [HideInInspector] public GameObject player; //cameraEye object
    //public GameObject hitbodyProjection;
    //private Renderer rendHitbody;
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
    //private float enemyHitSoundPitchOriginal;

    //private float enemyFlashHitCounter;

    public bool flash;

    private Rigidbody rb;

    void Start () {

        //Define player variables for enemy prefab
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawnManager = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        enemyEffectsManager = GameObject.Find("EnemyEffectsManager").GetComponent<EnemyEffectsManager>();

        //rendHitbody = hitbodyProjection.GetComponent<Renderer>();


        rb = GetComponent<Rigidbody>();

        if (!isRedemptionDrone)
        {
            explosionTextObj.GetComponent<EarnedBulletText>().bulletNumber = enemyGiveBullets;
        }

        enemyHealth += enemyEffectsManager.addEnemyHealth;

        //explosionSoundPitchOriginal = explosionSound.pitch;
        //enemyHitSoundPitchOriginal = enemyHitSound.pitch;

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
        if (gameManager.gameOver)// && !isRedemptionDrone)
        {
            EnemyDestroyNoBullets();
        }

        // Slow Down Audio
        //enemyHitSound.pitch = Mathf.Lerp(enemyHitSound.pitch,  enemyHitSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);
        //explosionSound.pitch = Mathf.Lerp(explosionSound.pitch, explosionSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);

        // Lerp Glow to 0.1f
        /*for (int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshToChangeOnFlash[i].GetComponent<Renderer>().material.SetFloat("_MKGlowPower", Mathf.Lerp(meshToChangeOnFlash[i].GetComponent<Renderer>().material.GetFloat("_MKGlowPower"), .1f, Time.deltaTime));
        }*/


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
            //enemyHitSound.pitch = enemyHitSound.pitch + Random.Range(-.05f, .05f);
            enemyHitSound.Play();

            StartCoroutine(EnemyHitFlash(enemyFlashHitDuration));

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
                

                damage = other.gameObject.GetComponent<Bullet>().damage;
            } else if (other.gameObject.tag == "DeflectedBullet")
            {
                damage = other.gameObject.GetComponent<EnemyBullet>().damage;
            }
            //if ((enemyHealth - damage) <= 0)
            {
                //rb.mass = 1;
                //rb.drag = 0;
                //rb.angularDrag = .05f;
                //rb.useGravity = true;
                //rb.AddForce(Physics.gravity * .25f);
                //rb.AddForceAtPosition(new Vector3(Random.Range(-10f, 10), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), other.contacts[0].point);
                //rb.AddExplosionForce(1500f, other.contacts[0].point, 5f);
            }
            enemyHealth -= damage;
            Destroy(other.gameObject);
            if (enemyHealth <= 0)
            {
                //rb.AddForce(Physics.gravity * .25f);
               

                StartCoroutine(EnemyExplosion());

                float healthChance = Random.Range(0f, 1f);
                if (healthChance <= playerController.playerHealthChance)
                {
                    playerController.playerHealth++;
                }
            }

            //if (enemyHealth <= 0)
            {
                //print("enemyDestroyCalled");
                //EnemyDestroy();
                //Destroy(this.gameObject);
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

    IEnumerator EnemyHitFlash(float flashDuration)
    {
        for (int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshToChangeOnFlash[i].GetComponent<Renderer>().material = enemyFlashMat;
        }
        yield return new WaitForSeconds(flashDuration);
        for (int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshToChangeOnFlash[i].GetComponent<Renderer>().material = meshRendererOriginals[i];
        }

    }

    IEnumerator EnemyExplosion()
    {

        //rb.AddForce(Physics.gravity * .25f);

        //rb.mass = 1;
        //rb.drag = 0;
        //rb.angularDrag = .05f;
        //rb.useGravity = true;
        //rb.angularVelocity = new Vector3(Random.Range(-100f, 100), Random.Range(-100f, 100f), Random.Range(-100f, 100f));
        StartCoroutine(EnemyHitFlash(enemyFlashHitDuration));
        yield return new WaitForSeconds(.05f);

        /*StartCoroutine(EnemyHitFlash(.05f));
        yield return new WaitForSeconds(.1f);

        StartCoroutine(EnemyHitFlash(.05f));
        yield return new WaitForSeconds(.1f);

        StartCoroutine(EnemyHitFlash(.05f));
        yield return new WaitForSeconds(.1f);*/


        if (enemyGiveBullets > 0)
        {
            playerController.playerBullets += enemyGiveBullets + playerController.enemyGiveAdditionalBullets;
            Instantiate(explosionTextObj, transform.position, Quaternion.identity);
        }

        if (gameManager.redemptionActive)
        {
            Instantiate(explosionTextObj, transform.position, transform.rotation);
            //gameManager.redemptionMeter = gameManager.redemptionMeterMax; // Fill up the redemption meter so it can properly count down again
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);

        

        Destroy(this.gameObject);

    }

    public void EnemyTalkingGlow()
    {
        /*for (int i = 0; i < meshToChangeOnFlash.Length; i++)
        {
            meshToChangeOnFlash[i].GetComponent<Renderer>().material.SetFloat("_MKGlowPower", 1f);
        }*/
    }

    void EnemyDestroy()
    {
        //rb.AddForce(Physics.gravity * .25f);
        //rb.angularVelocity = new Vector3(Random.Range(-10f, 10), Random.Range(-10f, 10f), Random.Range(-10f, 10f));

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

    private void OnDestroy()
    {
        gameManager.enemiesOnScreen--;
        gameManager.enemiesDestroyed++;
    }


}
