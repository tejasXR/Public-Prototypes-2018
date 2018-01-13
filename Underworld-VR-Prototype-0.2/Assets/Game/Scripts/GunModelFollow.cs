using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModelFollow : MonoBehaviour {

    public GameObject controller;

    public float moveSpeed;
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(transform.position, controller.transform.position, Time.unscaledDeltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, controller.transform.rotation, Time.unscaledDeltaTime * rotationSpeed);
	}
}
