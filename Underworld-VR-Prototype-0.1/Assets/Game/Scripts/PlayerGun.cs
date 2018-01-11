using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float bulletFireRate; //bullets fires per second
    public float bulletTimer;
    public float bulletSpeed;

    public float gunAccuracy;



	// Use this for initialization
	void Start () {
		
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

        if (bulletTimer == bulletFireRate)
        {
            //Instantiate bullet
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);

            bulletTimer = 0;
        }


        //Destroy bullet after 2 seconds
    }
}
