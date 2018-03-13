using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGroundMove : MonoBehaviour {

    public float speed;

    private Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(0, 0, speed);


    }
}
