using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Vector3 bulletSpawnStart;

    public float bulletFireRate; //bullets fires per second
    public float bulletTimer;
    public float bulletSpeed;

    public float gunAccuracy;



	// Use this for initialization
	void Start () {
        bulletSpawnStart = bulletSpawn.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Fire();
        }

        if (bulletTimer < bulletFireRate)
        {
            bulletTimer += Time.deltaTime;

        } else
        {
            bulletTimer = bulletFireRate;
        }

    }

    void Fire()
    {
        Ray ray = new Ray(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        Debug.DrawRay(bulletSpawn.transform.position, bulletSpawn.transform.forward);
        Vector3 gunDownSights = ray.GetPoint(15); //Gets point X units of distance point out of gun
        Vector3 randomFire = gunDownSights + (new Vector3(0f, Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * (1-gunAccuracy));

        //bulletSpawn.transform.position = bulletSpawnStart *

        if (bulletTimer == bulletFireRate)
        {
            //Instantiate bullet
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = randomFire * bulletSpeed;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);

            bulletTimer = 0;
        }


        //Destroy bullet after 2 seconds
    }
}
