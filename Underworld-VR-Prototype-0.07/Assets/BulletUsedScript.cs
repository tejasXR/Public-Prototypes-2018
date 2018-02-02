using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUsedScript : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(Random.Range(-.5f, .5f), Random.Range(0f, .75f), Random.Range(0f, 1f));

        Destroy(this.gameObject, .75f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
