using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : MonoBehaviour {

    public Player playerController;

    public GameObject bodyCollider; //bodyCollider Object
    public float speed;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        bodyCollider = GameObject.Find("BodyCollider");
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        rb = GetComponent<Rigidbody>();

        //rb.centerOfMass = Vector3.zero;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //rb.AddForce((bodyCollider.transform.position - transform.position).normalized * speed * Time.smoothDeltaTime);

        rb.MovePosition(Vector3.zero * Time.deltaTime);
        
        //transform.LookAt(bodyCollider.transform.position);
        //rb.AddRelativeForce(0, 0, speed);

        //rb.AddForce()

        //transform.position = Vector3.Lerp(transform.position, bodyCollider.transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == bodyCollider)
        {
            playerController.playerBullets += 5;
            Destroy(gameObject);
        }
    }
}
