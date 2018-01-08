using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float enemyHealth;
    public Player playerController;
    //public float enemyGiveHealth; //Amount of health enemy gives to player after it is destroyed

    //public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

        if (enemyHealth <= 0)
        {
           Destroy(this.gameObject);
        }
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            var damage = collision.gameObject.GetComponent<Bullet>().damage;
            enemyHealth -= damage;
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        playerController.playerHealth += enemyHealth;

    }
}
