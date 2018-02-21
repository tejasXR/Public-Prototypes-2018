using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    public Player playerController;

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public float shieldHealthMax;
    public float shieldHealth;
    public float shieldHealthSmooth;
    public float shieldHealthPercent;
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

    public GameObject[] meshesToChange;
    public Color32 normalColor;
    public Color32 flashColor;

    public float flickerSpeedMax;
    private float flickerSpeedCurrent;

    private float scanTileCurrent;
    private float scanTileOriginal;

    public TextMeshPro healthText;
    public GameObject bulletGainedObj;

    //public AudioSource shieldHitSound;

    public GameObject shieldHitEffect;

    private bool hit;

    // Outline Health meters
    public GameObject[] outlines;

    public float[] scaleXOriginal;
    public float[] scaleXCurrent;

    public float[] meterXCurrent;
    public float[] meterXOriginal;

    public float[] scaleZOriginal;
    public float[] scaleZCurrent;

    public float[] meterZCurrent;
    public float[] meterZOriginal;

    //public GameObject[] 

    // Bottom Outline

    private void Awake()
    {
        shieldHealth = shieldHealthMax * shieldHealthMaxMultiplier;
        //rend = shieldMesh.GetComponent<Renderer>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        //scanTileOriginal = rend.material.GetFloat("_ScanTiling");

        scaleXOriginal = new float[outlines.Length];
        scaleXCurrent = new float[outlines.Length];

        meterXCurrent = new float[outlines.Length];
        meterXOriginal = new float[outlines.Length];

        scaleZOriginal = new float[outlines.Length];
        scaleZCurrent = new float[outlines.Length];

        meterZCurrent = new float[outlines.Length];
        meterZOriginal = new float[outlines.Length];

    }


    // Use this for initialization
    void Start () {
        
        for(int i = 0; i < outlines.Length; i++)
        {
            scaleXOriginal[i] = outlines[i].transform.localScale.x;
            scaleZOriginal[i] = outlines[i].transform.localScale.z;

            meterXOriginal[i] = outlines[i].transform.localPosition.x;
            meterZOriginal[i] = outlines[i].transform.localPosition.z;
        }

    }

    private void OnEnable()
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            scaleXCurrent[i] = 0;
            scaleZOriginal[i] = 0;

            meterXCurrent[i] = meterXOriginal[i] - scaleXOriginal[i] / 2;
            meterZCurrent[i] = meterZOriginal[i] - scaleZOriginal[i] / 2;

        }
    }

    // Update is called once per frame
    void Update () {


        shieldHealthPercent = Mathf.Lerp(shieldHealthPercent, (shieldHealthSmooth / (shieldHealthMax * shieldHealthMaxMultiplier)), Time.deltaTime * 2f);


        shieldHealthSmooth = Mathf.SmoothStep(shieldHealthSmooth, shieldHealth, Time.deltaTime * 10f);

        for (int i = 0; i < outlines.Length; i++)
        {
            scaleXCurrent[i] = Mathf.Lerp(scaleXCurrent[i], scaleXOriginal[i] * shieldHealthPercent, Time.deltaTime * 3f);
            //scaleZCurrent[i] = Mathf.Lerp(scaleZCurrent[i], scaleZOriginal[i] * shieldHealthPercent, Time.deltaTime * 3f);

            meterXCurrent[i] = Mathf.Lerp(meterXCurrent[i], (meterXOriginal[i] - scaleXOriginal[i] / 2) + ((scaleXOriginal[i] / 2) * shieldHealthPercent), Time.deltaTime * 3f);
            meterZCurrent[i] = Mathf.Lerp(meterZCurrent[i], (meterZOriginal[i] - scaleZOriginal[i] / 2) + ((scaleZOriginal[i] / 2) * shieldHealthPercent), Time.deltaTime * 3f);

            outlines[i].transform.localScale = new Vector3(scaleXCurrent[i], outlines[i].transform.localScale.y, outlines[i].transform.localScale.z);
            outlines[i].transform.localPosition = new Vector3(meterXCurrent[i], outlines[i].transform.localPosition.y, meterZCurrent[i]);
            //outlines[i].transform.localPosition = new
            //outlines[i].transform.Translate(meterXCurrent[i], 0, 0);
        }

        shieldHealth += Time.deltaTime * shieldRechargeSpeed * shieldRegenMultiplier;

        if (shieldHealth >= shieldHealthMax * shieldHealthMaxMultiplier)
        {
            shieldHealth = shieldHealthMax * shieldHealthMaxMultiplier;
        }
        
        if (shieldHealth <= 0)
        {
            shieldHealth = 0;
        }


        flickerSpeedCurrent = Mathf.Lerp(flickerSpeedCurrent, 0, Time.deltaTime * 3f);
        //scanTileCurrent = Mathf.Lerp(scanTileCurrent, scanTileOriginal * shieldSizeMultiplier, Time.deltaTime * 3f);

        //scanTileCurrent = Mathf.Lerp(scanTileCurrent, scanTileOriginal, Time.deltaTime * 3f);


        //rend.material.SetFloat("_FlickerSpeed", flickerSpeedCurrent);
        //rend.material.SetFloat("_ScanTiling", scanTileCurrent);

        //float modelScale = shieldHealthSmooth / (shieldHealthMax * shieldHealthMaxMultiplier);

        //transform.localScale = new Vector3(modelScale, modelScale, modelScale);

        healthText.text = "shield health: " + Mathf.RoundToInt(shieldHealthSmooth) + " / " + Mathf.RoundToInt(shieldHealthMax * shieldHealthMaxMultiplier);
        healthText.text = "" + Mathf.RoundToInt(shieldHealthPercent * 100) + "%";
        //healthText.material.SetColor("_GlowColor", Color.Lerp(healthText.material.GetColor("_GlowColor"), normalColor, Time.deltaTime * 3f));


        for(int i = 0; i < meshesToChange.Length; i++)
        {
            meshesToChange[i].GetComponent<Renderer>().material.SetFloat("_Alpha", shieldHealthPercent);
            meshesToChange[i].GetComponent<Renderer>().material.SetColor("_MainColor", Color.Lerp(meshesToChange[i].GetComponent<Renderer>().material.GetColor("_MainColor"), normalColor, Time.deltaTime * 3f));
            meshesToChange[i].GetComponent<Renderer>().material.SetFloat("_FlickerSpeed", flickerSpeedCurrent);
        }

    }

    private void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Shield collision with something");

        if (other.gameObject.tag == "EnemyBullet" && !hit)
        {
            hit = true;

            Instantiate(shieldHitEffect, other.transform.position, transform.localRotation);
            Destroy(other.gameObject);


            //print("Shield found bullet");
            float otherDamage = other.gameObject.GetComponent<EnemyBullet>().damage;

            for (int i = 0; i < meshesToChange.Length; i++)
            {
                meshesToChange[i].GetComponent<Renderer>().material.SetColor("_MainColor", flashColor);
            }

            healthText.material.SetColor("_GlowColor", flashColor);

            shieldHealth -= otherDamage;

            //scanTileCurrent = 0;
            flickerSpeedCurrent = flickerSpeedMax;

            StartCoroutine(ShieldVibration(1, 3000));

            // A chance to absorb an incoming bullet
            //float shieldAbsorption = Random.Range(0f, 1f);
            //if (shieldAbsorption > shieldBulletAbsorbtionAmount)
            {
                playerController.playerBullets += shieldBulletAbsorbtionAmount;
                //Instantiate(bulletGainedObj, other.gameObject.transform.position, other.gameObject.transform.rotation);
            }

            //print("Hit");

            hit = false;

        }
    }

    /*

    private void OnCollisionEnter(Collision other)
    {
        print("Shield collision with something");

        if (other.gameObject.tag == "EnemyBullet")
        {

            print("Shield found bullet");
            float otherDamage = other.gameObject.GetComponent<EnemyBullet>().damage;

            for (int i = 0; i < meshesToChange.Length; i++)
            { 
                meshesToChange[i].GetComponent<Renderer>().material.SetColor("_MainColor", flashColor);
            }

            healthText.material.SetColor("_GlowColor", flashColor);

            shieldHealth -= otherDamage;

            //scanTileCurrent = 0;
            flickerSpeedCurrent = flickerSpeedMax;

            StartCoroutine(ShieldVibration(1, 2000));

            // A chance to absorb an incoming bullet
            //float shieldAbsorption = Random.Range(0f, 1f);
            //if (shieldAbsorption > shieldBulletAbsorbtionAmount)
            {
                playerController.playerBullets += shieldBulletAbsorbtionAmount;
                Instantiate(bulletGainedObj, other.gameObject.transform.position, other.gameObject.transform.rotation);
            }

            //print("Hit");

            Destroy(other.gameObject);
        }
    }
    */

    /*private void OnTriggerEnter(Collider other)
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
    }*/

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
