using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionFollow : MonoBehaviour {

    public HandController handController;
    public float distanceFromPlayer;
    private GameObject cube;

	// Use this for initialization
	void Start () {
        cube = GameObject.Find("Cube");
	}
	
	// Update is called once per frame
	void Update () {

        if (!handController.grabbedObj)
        {
            Ray ray = new Ray(cube.transform.position, transform.position);
            Vector3 destPoint = ray.GetPoint(distanceFromPlayer);

            transform.position = Vector3.Lerp(transform.position, new Vector3(destPoint.x, cube.transform.position.y - .25f, destPoint.z), Time.deltaTime);
        }
		
	}
}
