using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    public Player playerController;

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public float shieldHealthMax;
    public float shieldHealth;
    private float shieldHealthSmooth;
    public float shieldRechargeSpeed;

    public float shieldRegenMultiplier = 1;
    public float shieldHealthMaxMultiplier = 1;

    //public float shieldSizeMultiplier = 1;

    public float shieldBulletAbsorbtionAmount = 1;

    //private float scaleOriginal;
    //private float scaleCurrent;
    //private float scaleMax;

    public GameObject shieldMesh;

    private Renderer rend;

    public float flickerSpeedMax;
    private float flickerSpeedCurrent;

    private float scanTileCurrent;
    private float scanTileOriginal;


	// Use this for initialization
	void Start () {
        shieldHealth = shieldHealthMax * shieldHealthMaxMultiplier;
        rend = shieldMesh.GetComponent<Renderer>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        scanTileOriginal = rend.material.GetFloat("_ScanTiling");
	}
	
	// Update is called once per frame
	void Update () {

        shieldHealthSmooth = Mathf.SmoothStep(shieldHealthSmooth, shieldHealth, Time.deltaTime * 10f);

        shieldHealth += Time.deltaTime * shieldRechargeSpeed * shieldRegenMultiplier;

        if (shieldHealth >= shieldHealthMax * shieldHealthMaxMultiplier)
        {
            shieldHealth = shieldHealthMax * shieldHealthMaxMultiplier;
        }

        flickerSpeedCurrent = Mathf.Lerp(flickerSpeedCurrent, 0, Time.deltaTime * 3f);
        //scanTileCurrent = Mathf.Lerp(scanTileCurrent, scanTileOriginal * shieldSizeMultiplier, Time.deltaTime * 3f);

        scanTileCurrent = Mathf.Lerp(scanTileCurrent, scanTileOriginal, Time.deltaTime * 3f);


        rend.material.SetFloat("_FlickerSpeed", flickerSpeedCurrent);
        rend.material.SetFloat("_ScanTiling", scanTileCurrent);

        float modelScale = shieldHealthSmooth / (shieldHealthMax * shieldHealthMaxMultiplier);

        transform.localScale = new Vector3(modelScale, modelScale, modelScale);
	}

    private void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            float otherDamage = other.gameObject.GetComponent<EnemyBullet>().damage;
            shieldHealth -= otherDamage;

            scanTileCurrent = 0;
            flickerSpeedCurrent = flickerSpeedMax;

            StartCoroutine(ShieldVibration(1, 2000));

            // A chance to absorb an incoming bullet
            //float shieldAbsorption = Random.Range(0f, 1f);
            //if (shieldAbsorption > shieldBulletAbsorbtionAmount)
            {
                playerController.playerBullets += shieldBulletAbsorbtionAmount;
            }

            //print("Hit");

            //Destroy(other.gameObject);
        }
    }

    IEnumerator ShieldVibration(float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse(strength);
            strength = (ushort)Mathf.Lerp(strength, 0, Time.deltaTime * 5);
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
