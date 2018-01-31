using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour {

    public GameObject swordEnd;
    public LightsaberTrail lightsaberTrail;

    public float swordScaleCurrent;
    public float swordScaleOriginal;

    public float swordYCurrent;
    public float swordYOriginal;

    public float trailScaleOriginal;
    public float trailScaleCurrent;

	// Use this for initialization
	void Start () {
        swordScaleOriginal = swordEnd.transform.localScale.y;
        swordYOriginal = swordEnd.transform.localPosition.y;

        trailScaleOriginal = lightsaberTrail.height;
	}

    private void OnEnable()
    {
        swordScaleCurrent = 0;
        swordYCurrent = .053f;
        trailScaleCurrent = 0;
    }

    // Update is called once per frame
    void Update () {

        swordScaleCurrent = Mathf.Lerp(swordScaleCurrent, swordScaleOriginal, Time.deltaTime * 1.5f);
        swordYCurrent = Mathf.Lerp(swordYCurrent, swordYOriginal, Time.deltaTime * 1.5f);
        trailScaleCurrent = Mathf.Lerp(trailScaleCurrent, trailScaleOriginal, Time.deltaTime * 1.5f);


        swordEnd.transform.localScale = new Vector3(swordEnd.transform.localScale.x, swordScaleCurrent, swordEnd.transform.localScale.z);
        swordEnd.transform.localPosition = new Vector3(swordEnd.transform.localPosition.x, swordYCurrent, swordEnd.transform.localPosition.z);
        lightsaberTrail.height = trailScaleCurrent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            var enemyBullet = other.gameObject.GetComponent<EnemyBullet>();
            var enemyBulletRb = other.gameObject.GetComponent<Rigidbody>();

            Vector3 bulletDirection = enemyBullet.enemyParent.transform.position - other.transform.position;

            if (!enemyBullet.enemyParent.gameObject.activeInHierarchy)
            {
                bulletDirection = -other.transform.position;
            }

            other.transform.rotation = Quaternion.LookRotation(bulletDirection);
            enemyBulletRb.constraints = RigidbodyConstraints.FreezeRotation;
            enemyBulletRb.velocity = bulletDirection * 3f;

            other.gameObject.tag = "DeflectedBullet";
        }
    }
}
