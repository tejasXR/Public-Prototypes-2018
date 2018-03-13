using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    // Player Basic Variables
    public float playerBullets;
    public string bulletString;

    public float playerHealth;
    public float playerHealthMax;

    // Player Health Multipliers
    public float playerHealthMaxMultiplier = 1;

    // Player Attack Multipliers
    public float bulletFireRateMultiplier = 1;
    public float bulletDamageMultiplier = 1;
    public float bulletSpeedMultiplier = 1;
    public float bulletAccuracyMultiplier = 1;

    public TextMeshPro[] bulletCounters;
    public GameObject hitBody;

	void Start ()
    {
        foreach (TextMeshPro bulletCounter in bulletCounters)
        {
            bulletCounter.text = "" + playerBullets.ToString();
        }
        bulletString = playerBullets.ToString();        
	}
	
	void Update ()
    {
        if (bulletString != playerBullets.ToString())
        {
            foreach (TextMeshPro bulletCounter in bulletCounters)
            {
                bulletCounter.text = "" + playerBullets.ToString();                
            }
        }
    }
}
