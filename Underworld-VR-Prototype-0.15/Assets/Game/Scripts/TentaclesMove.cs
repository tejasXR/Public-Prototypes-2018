using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesMove : MonoBehaviour {

    public float changeTimer;
    public float changeFrequency;

    private Quaternion rotation;
    private Vector3 toAngle;

    public float rotationXMin;
    public float rotationXMax;

    public float rotationYMin;
    public float rotationYMax;

    public float rotationZMin;
    public float rotationZMax;

    public float refVelocity = .5f;
    public float smooth = 1f;



    // Use this for initialization
    void Start () {
        rotation = Quaternion.Euler(0, 0, 0);
		
	}
	
	// Update is called once per frame
	void Update () {

        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
        {
            RandomAngle();
            changeTimer = Random.Range(0, 1 * changeFrequency);
        }

        //float xAngle = Mathf.SmoothDampAngle(transform.localEulerAngles.x, rotation.eulerAngles.x, ref refVelocity, smooth);
        //float yAngle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, rotation.eulerAngles.y, ref refVelocity, smooth);
        //float zAngle = Mathf.SmoothDampAngle(transform.localEulerAngles.z, rotation.eulerAngles.z, ref refVelocity, smooth);

        float xAngle = Mathf.SmoothDampAngle(transform.localEulerAngles.x, rotation.eulerAngles.x, ref refVelocity, smooth);
        float yAngle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, rotation.eulerAngles.y, ref refVelocity, smooth);
        float zAngle = Mathf.SmoothDampAngle(transform.localEulerAngles.z, rotation.eulerAngles.z, ref refVelocity, smooth);

        //Vector3 toAngle = new Vector3(xAngle, yAngle, zAngle);


        //transform.Rotate(toAngle, Time.deltaTime * 10f);

        //transform.localEulerAngles = Vector3.up;

        //transform.Rotate(Vector3.right * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(xAngle, yAngle, zAngle);

    }

    void RandomAngle()
    {

        var randomXAngle = Random.Range(rotationXMin, rotationXMax);
        var randomYAngle = Random.Range(rotationYMin, rotationYMax);
        var randomZAngle = Random.Range(rotationZMin, rotationZMax);



        rotation.eulerAngles = new Vector3(randomXAngle, randomYAngle, randomZAngle);

        toAngle = new Vector3(randomXAngle, randomYAngle, randomZAngle);

        //rotation = Mathf.SmoothDampAngle
    }
}
