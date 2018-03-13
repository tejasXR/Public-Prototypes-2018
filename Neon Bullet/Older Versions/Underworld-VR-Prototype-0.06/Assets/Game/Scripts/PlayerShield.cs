using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    public float shieldHealthMax;
    public float shieldHealth;
    private float shieldHealthSmooth;
    public float shieldRechargeSpeed;

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
        shieldHealth = shieldHealthMax;
        rend = shieldMesh.GetComponent<Renderer>();

        scanTileOriginal = rend.material.GetFloat("_ScanTiling");
	}
	
	// Update is called once per frame
	void Update () {

        shieldHealthSmooth = Mathf.SmoothStep(shieldHealthSmooth, shieldHealth, Time.deltaTime * 10f);

        shieldHealth += Time.deltaTime * shieldRechargeSpeed;

        if (shieldHealth >= shieldHealthMax)
        {
            shieldHealth = shieldHealthMax;
        }

        flickerSpeedCurrent = Mathf.Lerp(flickerSpeedCurrent, 0, Time.deltaTime * 3f);
        scanTileCurrent = Mathf.Lerp(scanTileCurrent, scanTileOriginal, Time.deltaTime * 3f);

        rend.material.SetFloat("_FlickerSpeed", flickerSpeedCurrent);
        rend.material.SetFloat("_ScanTiling", scanTileCurrent);

        float modelScale = shieldHealthSmooth / shieldHealthMax;

        transform.localScale = new Vector3(modelScale, modelScale, modelScale);
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            float otherDamage = other.gameObject.GetComponent<EnemyBullet>().damage;
            shieldHealth -= otherDamage;

            scanTileCurrent = 0;
            flickerSpeedCurrent = flickerSpeedMax;

            //print("Hit");

            Destroy(other.gameObject);
        }
    }
}
