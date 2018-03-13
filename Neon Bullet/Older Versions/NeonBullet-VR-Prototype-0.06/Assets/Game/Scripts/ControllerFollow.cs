using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFollow : MonoBehaviour {

    public GameObject controller;

    public float moveSpeed;
    public float rotationSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, controller.transform.position, Time.unscaledDeltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, controller.transform.rotation, Time.unscaledDeltaTime * rotationSpeed);
	}
}
