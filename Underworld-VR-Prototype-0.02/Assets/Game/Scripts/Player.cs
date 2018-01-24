using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {

    public float playerBullets;

    public TextMeshPro bulletCounter;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BulletDrop")
        {
            playerBullets += 1;
            Destroy(collision.gameObject);
        }
    }
}
