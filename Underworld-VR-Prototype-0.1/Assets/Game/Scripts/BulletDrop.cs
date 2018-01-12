using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour {

    public GameObject player; //bodyCollider Object
    public float speed;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("BodyCollider");

        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(player.transform.position);
        rb.AddRelativeForce(0, 0, speed);
        
        //rb.AddForce()

        //transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed);
	}
}
