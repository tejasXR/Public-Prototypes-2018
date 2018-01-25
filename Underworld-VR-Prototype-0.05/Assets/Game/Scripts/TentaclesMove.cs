﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesMove : MonoBehaviour {

    public float changeTimer;
    public float changeFrequency;

    private Quaternion rotation;

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

		
	}
	
	// Update is called once per frame
	void Update () {

        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
        {
            RandomAngle();
            changeTimer = Random.Range(0, 1 * changeFrequency);
        }

        float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, rotation.eulerAngles.x, ref refVelocity, smooth);
        float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation.eulerAngles.y, ref refVelocity, smooth);
        float zAngle = Mathf.SmoothDampAngle(transform.eulerAngles.z, rotation.eulerAngles.z, ref refVelocity, smooth);


        transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);

    }

    void RandomAngle()
    {

        var randomXAngle = Random.Range(rotationXMin, rotationXMax);
        var randomYAngle = Random.Range(rotationYMin, rotationYMax);
        var randomZAngle = Random.Range(rotationZMin, rotationZMax);



        rotation.eulerAngles = new Vector3(randomXAngle, randomYAngle, randomZAngle);
        //rotation = Mathf.SmoothDampAngle
    }
}
