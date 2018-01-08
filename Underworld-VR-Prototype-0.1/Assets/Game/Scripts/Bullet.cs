using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletDestroyRate;
    private Rigidbody rb;
    public float bulletSpeed;


    // Use this for initialization
    void Start () {
        Destroy(this, bulletDestroyRate);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //Add velocity to bullet when first created
		rb.velocity = transform.forward * bulletSpeed;
    }
}
