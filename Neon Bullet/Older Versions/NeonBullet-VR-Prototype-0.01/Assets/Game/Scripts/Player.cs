using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float playerBullets;

    public TextMesh bulletText;
    //public string health;

	// Use this for initialization
	void Start () {
        bulletText.text = "" + playerBullets.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (bulletText.text != playerBullets.ToString())
        {
            bulletText.text = "" + playerBullets.ToString();
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
