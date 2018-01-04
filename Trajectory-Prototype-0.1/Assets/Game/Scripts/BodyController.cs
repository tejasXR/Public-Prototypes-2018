using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    public GameObject player; //cameraEye object
    public GameObject cameraRig;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.transform.position.x, cameraRig.transform.position.y + .3f, player.transform.position.z);
	}
}
