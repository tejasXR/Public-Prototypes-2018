using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerGun : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public Player playerController;
    private GameManager gameManager;

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

    public bool isPistol;
    public bool isRifle;
    public bool isShotgun;
    public bool isHyperRifle;

    public bool noBulletHaptic;

    public GameObject bulletUsedObj;
    public GameObject noBulletsObj;

    public AudioSource noBulletsSound;

    //private AudioSource gunFireSound;

    //public ushort pulseStrength;



    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        bulletSpawnStart = bulletSpawn.transform.position;

        // Recheck multipliers after upgrades
        bulletFireRate = bulletFireRate * playerController.bulletFireRateMultiplier;
        
        bulletAccuracy = bulletAccuracy * playerController.bulletAccuracyMultiplier;
        
        // Bullet timer calculation so fire rate is in bullets per second
        bulletTimer = 0;

        gunBodyBaseRotation = gunBody.transform.rotation;

        //gunFireSound = GetComponent<AudioSource>();

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



        // Add dynamic pulse feed back to weapon haptics
        /*if (pulseStart > 0)
        {
            for (ushort i = pulseStart; i > 0; i -= Time.deltaTime)
            {
                device.TriggerHapticPulse(i);
                pulseStart -= 1;
                //print(pulseStart);
            }
        }*/
        

    }

    // Update is called once per frame
    void FixedUpdate () {

       device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;
        
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (playerController.playerBullets >= bulletsInstantiated)
            {
                Fire();
            }
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && playerController.playerBullets < bulletsInstantiated)
        {
            StartCoroutine(NoBulletsLeft());
            noBulletsSound.Play();
            Instantiate(noBulletsObj, sparkPoint.position, sparkPoint.transform.rotation);
        }
    }

    void Fire()
    {
        if (bulletTimer <= 0)
        {
            Instantiate(gunSparkEffect, sparkPoint.position, sparkPoint.transform.rotation);
            
            GunHaptics();
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
                //float bulletNoUseChance = Random.Range(0f, 1f);
                //print(bulletNoUseChance);
                //if (bulletNoUseChance > playerController.playerNoUseBulletChance)
                {
                    //if (gameManager.roundActive)
                    {
                        playerController.playerBullets -= 1;
                        //Instantiate(bulletUsedObj, sparkPoint.position, sparkPoint.transform.rotation);
                    }
                    //print("bullet used");

                }

                // Add velocity to the bullet
                bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;
                bullet.GetComponent<Bullet>().damage = bulletDamage * playerController.bulletDamageMultiplier;

                //bullet.GetComponent<Bullet>().bulletDirection = bulletDirection;
                //bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;

                
                // Destroy the bullet after 2 seconds
                //Destroy(bullet, 2.0f);

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

    void GunHaptics()
    {
        if (isPistol)
        {
            StartCoroutine(GunVibration(.4f, 2500));
        }

        if (isRifle)
        {
            StartCoroutine(GunVibration(.25f, 2000));

        }

        if (isShotgun)
        {
            StartCoroutine(GunVibration(1f, 3500));

        }

        if (isHyperRifle)
        {
            StartCoroutine(GunVibration(1f, 4000));
        }
    }

    IEnumerator GunVibration(float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse(strength);
            strength = (ushort)Mathf.Lerp(strength, 0, Time.deltaTime * 5);
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }

    IEnumerator NoBulletsLeft()
    {
        device.TriggerHapticPulse(2000);
        yield return new WaitForSeconds(.1f);
        device.TriggerHapticPulse(2000);
        yield return new WaitForSeconds(.1f);
        device.TriggerHapticPulse(2000);
        //yield return new WaitForSeconds(.1f);
    }


}
