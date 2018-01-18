using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    public float playerBullets;
    public float playerHealth;
    public float playerHealthMax;

    public TextMeshPro bulletCounter;
    public GameObject hitBody;

    //public string health;

	// Use this for initialization
	void Start () {
        bulletCounter.text = "" + playerBullets.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (bulletCounter.text != playerBullets.ToString())
        {
            bulletCounter.text = "" + playerBullets.ToString();
        }
	}
}
