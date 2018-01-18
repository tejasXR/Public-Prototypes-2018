using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumCreator : MonoBehaviour {

    public float radius;
    public float degrees;
    private float x;
    private float z;


	// Use this for initialization
	void Start () {

        x = radius * Mathf.Sin(degrees);
        z = radius * Mathf.Cos(degrees);

		//transform.position = new Vector3()
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
