using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesMove : MonoBehaviour {

    private float changeTimer;
    private Quaternion rotation;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

        changeTimer -= Time.deltaTime;

        if (changeTimer <= 0)
        {
            RandomAngle();
            changeTimer = Random.Range(1, 5);
        }


        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);

    }

    void RandomAngle()
    {
        var randomAngle = Random.Range(0, 15);
        rotation.eulerAngles = new Vector3(0, 0, randomAngle);


    }
}
