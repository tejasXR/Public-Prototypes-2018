using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesMove : MonoBehaviour {

    public float changeTimer;
    private Quaternion rotation;

    public float rotationXMin;
    public float rotationXMax;

    public float rotationYMin;
    public float rotationYMax;

    public float rotationZMin;
    public float rotationZMax;


    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
        {
            RandomAngle();
            changeTimer = Random.Range(1, 2);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

    }

    void RandomAngle()
    {
        var randomXAngle = Random.Range(rotationXMin, rotationXMax);
        var randomYAngle = Random.Range(rotationYMin, rotationYMax);
        var randomZAngle = Random.Range(rotationZMin, rotationZMax);


        rotation.eulerAngles = new Vector3(randomXAngle, randomYAngle, randomZAngle);
    }
}
