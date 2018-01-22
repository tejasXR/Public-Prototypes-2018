using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    public float playerBullets;
     

    public string bulletString;

    public float playerHealth;
    public float playerHealthMax;

    public float playerHealthMaxMultiplier = 1;


    public float bulletFireRateMultiplier = 1;
    public float bulletDamageMultiplier = 1;
    public float bulletSpeedMultiplier = 1;
    public float bulletAccuracyMultiplier = 1;




    public TextMeshPro[] bulletCounters;
    public GameObject hitBody;

    //public string health;

	// Use this for initialization
	void Start () {
        foreach (TextMeshPro bulletCounter in bulletCounters)
        {
            bulletCounter.text = "" + playerBullets.ToString();
        }
        bulletString = playerBullets.ToString();        
	}
	
	// Update is called once per frame
	void Update () {

        if (bulletString != playerBullets.ToString())
        {
            foreach (TextMeshPro bulletCounter in bulletCounters)
            {
                bulletCounter.text = "" + playerBullets.ToString();                
            }
        }
    }
}
