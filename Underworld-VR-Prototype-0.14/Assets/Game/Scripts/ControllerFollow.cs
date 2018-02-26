using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFollow : MonoBehaviour {

    public GameObject controller;

    public float moveSpeed;
    public float rotationSpeed;

    private void OnEnable()
    {
        transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y - 1, controller.transform.position.z);
        //transform.rotation = new Quaternion(controller.transform.rotation);

    }

    // Update is called once per frame
    void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, controller.transform.position, Time.unscaledDeltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, controller.transform.rotation, Time.unscaledDeltaTime * rotationSpeed);
	}
}
