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

    public float bulletAccuracy;
    public float bulletsInstantiated;


	// Use this for initialization
	void Start () {
        bulletSpawnStart = bulletSpawn.transform.position;

        // Recheck multipliers after upgrades
        bulletFireRate = bulletFireRate * playerController.bulletFireRateMultiplier;
        bulletSpeed = bulletSpeed * playerController.bulletSpeedMultiplier;
        bulletAccuracy = bulletAccuracy * playerController.bulletAccuracyMultiplier;
        
        // Bullet timer calculation so fire rate is in bullets per second
        bulletTimer = 1 / bulletFireRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;
        

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger) && playerController.playerBullets >= 1)
        {
            Fire();
        }

        
        //if (bulletTimer >= bulletFireRate)
        {
            bulletTimer -= Time.deltaTime;

        } if (bulletTimer <= 0)
        {
            bulletTimer = 0;
        }       
    }

    void Fire()
    {
        //Ray ray = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        //Debug.DrawRay(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        //Vector3 gunDownSights = ray.GetPoint(15); //Gets point X units of distance point out of gun
        //Vector3 randomFire = gunDownSights + (new Vector3(0f, Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * (1-gunAccuracy));

        //bulletSpawn.transform.position = bulletSpawnStart *

        if (bulletTimer <= 0)
        {
            for (int i = 0; i < bulletsInstantiated; i++)
            {

                float spreadFactor = 1 - bulletAccuracy;

                var bulletDirection = bulletSpawn.transform.forward;

                bulletDirection.x += Random.Range(-spreadFactor, spreadFactor);
                bulletDirection.y += Random.Range(-spreadFactor, spreadFactor);
                bulletDirection.z += Random.Range(-spreadFactor, spreadFactor);

                //Instantiate bullet
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.LookRotation(bulletDirection));
                playerController.playerBullets -= 1;
                // Add velocity to the bullet
                //bullet.GetComponent<Rigidbody>().velocity = randomFire * bulletSpeed;

                bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;

                // Destroy the bullet after 2 seconds
                Destroy(bullet, 2.0f);

            }
            bulletTimer = 1 / bulletFireRate;
        }

       
    }



}
