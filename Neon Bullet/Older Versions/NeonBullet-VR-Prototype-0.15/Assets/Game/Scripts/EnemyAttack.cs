using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private EnemyParent enemyParent;
    private EnemyMovement enemyMovement;

    //Enemy Attack Behavior
    public float enemyBulletFireRate; //bullets fires per second
    public float enemyAttackTimer; //timer to know when to attack next
    public float enemyBulletSpeed; //speed of enemy bullet
    public float enemyBulletDamage;
    //public Vector3 randomDirection; //random direction needed for enemy gun accuracy
    //private Ray ray; //ray needed for enemy gun accuracy
    public float enemyBulletAccuracy; //enemy accuray in % form
    //public Vector3 enemyBulletDirection; //direction of the enemy bullet when generated

    public GameObject enemyBulletPrefab; //the enemy bullet gameObject
    public Transform[] enemyBulletSpawns; //the object for where to spawn the enemy bullet
    private int enemyBulletSpawnCounter;

    public bool isSingleDrone;
    public bool isDoubleDrone;
    public bool isRedemptiionDrone;
    public bool isTutorialDrone;

    public EnemyEyeGlow[] eyeObject;

    public EnemyEffectsManager enemyEffectsManager;

    // Use this for initialization
    void Start () {
        enemyParent = GetComponent<EnemyParent>();
        enemyEffectsManager = GameObject.Find("EnemyEffectsManager").GetComponent<EnemyEffectsManager>();

        enemyMovement = GetComponent<EnemyMovement>();

        enemyAttackTimer = (1 /enemyBulletFireRate) + Random.Range(-.05f, .05f);

        enemyBulletFireRate += enemyEffectsManager.addEnemyFireRate;
        enemyBulletAccuracy += enemyEffectsManager.addEnemyAccuracy;
        if (enemyBulletAccuracy >= 1)
        {
            enemyBulletAccuracy = 1;
        }

    }

    // Update is called once per frame
    void Update () {



        if (enemyParent.gameManager.roundActive || (isRedemptiionDrone && enemyParent.gameManager.redemptionActive) || isTutorialDrone && enemyParent.enemyHealth > 0)
        {
            enemyAttackTimer -= Time.deltaTime;
            //If the attack timer equals the fire rate, then attack, else keep increasing the timer
            if (enemyAttackTimer <= .25f)
            {
                if (isDoubleDrone)
                {
                    foreach(EnemyEyeGlow eye in eyeObject)
                    {
                        eye.beforeAttack = true;
                    }
                } else
                {
                    eyeObject[0].beforeAttack = true;

                }
            }

            if (enemyAttackTimer <= 0 && (enemyMovement.canFire || isRedemptiionDrone))
            {
                Fire();
                if (isDoubleDrone)
                {
                    foreach (EnemyEyeGlow eye in eyeObject)
                    {
                        eye.beforeAttack = false;
                        eye.Flash();
                    }
                } else
                {
                    eyeObject[0].beforeAttack = false;
                    eyeObject[0].Flash();
                }
                
                // print("fireCalled");
            }
        }
    }

    void Fire()
    {
        //if (enemyMovement.canFire || isRedemptiionDrone)
        {
            //print("firing");
            float spreadFactor = 1 - enemyBulletAccuracy;

            var enemyBulletDirection = enemyParent.player.transform.position - transform.position;

            enemyBulletDirection.x += Random.Range(-spreadFactor, spreadFactor);
            enemyBulletDirection.y += Random.Range(-spreadFactor, spreadFactor);
            enemyBulletDirection.z += Random.Range(-spreadFactor, spreadFactor);

            enemyBulletDirection = enemyBulletDirection.normalized;

            //Vector3 randomFire = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * (1 - enemyAccuracy);
            //enemyBulletDirection = enemyParent.player.transform.position + randomFire;

            //enemyBulletSpawns[enemyBulletSpawnCounter].LookAt(enemyBulletDirection);

            //if (enemyAttackTimer == enemyBulletFireRate)
            //{

            // Eye Glow effect

                //Instantiate bullet
                var bullet = Instantiate(enemyBulletPrefab, enemyBulletSpawns[enemyBulletSpawnCounter].position, Quaternion.LookRotation(enemyBulletDirection));

                // Add velocity to the bullet
                //bullet.GetComponent<Rigidbody>().velocity = enemyBulletSpawns[enemyBulletSpawnCounter].transform.forward * enemyBulletSpeed;
                bullet.GetComponent<Rigidbody>().velocity = enemyBulletDirection * enemyBulletSpeed;
                bullet.GetComponent<EnemyBullet>().enemyParent = this.gameObject;
                bullet.GetComponent<EnemyBullet>().damage = enemyBulletDamage;
                //bullet.transform.position
                //print(enemyBulletDirection);

                // Destroy the bullet after 2 seconds and reset attack timer
                Destroy(bullet, 2.0f);
                
            // }


            //enemyBulletFireRate = Random.Range(.5f, 2f);

            enemyAttackTimer = (1 / (enemyBulletFireRate + Random.Range(0, 2)));

            enemyBulletSpawnCounter++;

            if (isSingleDrone || isRedemptiionDrone || isTutorialDrone)
            {
                if (enemyBulletSpawnCounter > 0) { enemyBulletSpawnCounter = 0; }
            } else if (isDoubleDrone)
            {
                if (enemyBulletSpawnCounter > 1) { enemyBulletSpawnCounter = 0; }
            }
        }
    }


}
