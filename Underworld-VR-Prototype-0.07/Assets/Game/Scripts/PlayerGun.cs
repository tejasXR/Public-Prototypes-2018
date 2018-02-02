using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerGun : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public Player playerController;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Vector3 bulletSpawnStart;

    public float bulletFireRate; //bullets fires per second
    public float bulletTimer;
    public float bulletSpeed;
    public float bulletDamage;

    public float bulletAccuracy;
    public float bulletsInstantiated;

    //public float bulletCriticalHitChance;

    public float gunRecoilMultiplier;

    public float gunRecoilThrowbackMin;
    public float gunRecoilThrowbackMax;

    public float gunRecoilBackAngleMin;
    public float gunRecoilBackAngleMax;

    public GameObject gunBody;
    private Quaternion gunBodyBaseRotation;

    public float gunRecoilAngleSpeed;
    public float gunRecoilThrowbackSpeed;

    public GameObject gunSparkEffect;
    public Transform sparkPoint;




    // Use this for initialization
    void Start () {
        bulletSpawnStart = bulletSpawn.transform.position;

        // Recheck multipliers after upgrades
        bulletFireRate = bulletFireRate * playerController.bulletFireRateMultiplier;
        
        bulletAccuracy = bulletAccuracy * playerController.bulletAccuracyMultiplier;
        
        // Bullet timer calculation so fire rate is in bullets per second
        bulletTimer = 0;

        gunBodyBaseRotation = gunBody.transform.rotation;

	}

    private void Update()
    {
        bulletTimer -= Time.deltaTime;

        if (bulletTimer <= 0)
        {
            bulletTimer = 0;
        }

        gunBody.transform.position = Vector3.Lerp(gunBody.transform.position, transform.position, Time.unscaledDeltaTime * gunRecoilThrowbackSpeed);
        gunBody.transform.localRotation = Quaternion.Lerp(gunBody.transform.localRotation, gunBodyBaseRotation, Time.unscaledDeltaTime * gunRecoilAngleSpeed);



    }

    // Update is called once per frame
    void FixedUpdate () {

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;
        
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && playerController.playerBullets >= 1)
        {
            Fire();
        }             
    }

    void Fire()
    {
        if (bulletTimer <= 0)
        {
            Instantiate(gunSparkEffect, sparkPoint.position, sparkPoint.transform.rotation);

            // Adds ability to instantiate multiple bullets
            for (int i = 0; i < bulletsInstantiated; i++)
            {
                // Adds gun accuray
                float spreadFactor = 1 - (bulletAccuracy * playerController.bulletAccuracyMultiplier);
                if (spreadFactor <= 0) {spreadFactor = 0;}

                var bulletDirection = bulletSpawn.transform.forward;
                

                bulletDirection.x += Random.Range(-spreadFactor, spreadFactor);
                bulletDirection.y += Random.Range(-spreadFactor, spreadFactor);
                bulletDirection.z += Random.Range(-spreadFactor, spreadFactor);

                //Instantiate bullet
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(bulletDirection));

                // Creates a chance that the gun will use no bullets when firing
                float bulletNoUseChance = Random.Range(0f, 1f);
                print(bulletNoUseChance);
                if (bulletNoUseChance > playerController.playerNoUseBulletChance)
                {
                    playerController.playerBullets -= 1;
                    print("bullet used");

                }

                // Add velocity to the bullet
                bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;
                bullet.GetComponent<Bullet>().damage = bulletDamage * playerController.bulletDamageMultiplier;

                //bullet.GetComponent<Bullet>().bulletDirection = bulletDirection;
                //bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;


                // Destroy the bullet after 2 seconds
                Destroy(bullet, 2.0f);

                //transform.localPosition -= transform.;
                //transform.localRotation = Quaternion.Euler(Random.Range(0, -20), 0, 0);
                //transform.Rotate(Vector3.left);

                
                

            }
            // Recalculated bullet timer
            bulletTimer = (1 / bulletFireRate) * playerController.bulletFireRateMultiplier;
            gunBody.transform.position -= gunBody.transform.forward * Random.Range(gunRecoilThrowbackMin, gunRecoilThrowbackMax) * gunRecoilMultiplier;
            gunBody.transform.localRotation = Quaternion.Euler(gunBody.transform.localRotation.x + Random.Range(gunRecoilBackAngleMin, gunRecoilBackAngleMax) * gunRecoilMultiplier, gunBody.transform.localRotation.y, gunBody.transform.localRotation.z);


        }
        
        
    }
}
