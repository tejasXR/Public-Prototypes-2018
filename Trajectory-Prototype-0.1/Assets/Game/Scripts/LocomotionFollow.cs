using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionFollow : MonoBehaviour {

    public HandController leftHand;
    public HandController rightHand;
    public GameObject player; //cameraEye object

    public float distanceFromPlayer;
    private GameObject cube;

	// Use this for initialization
	void Start () {
        cube = GameObject.Find("Cube");
	}
	
	// Update is called once per frame
	void Update () {

        if (!leftHand.grabbedObj && !rightHand.grabbedObj)
        {
            Ray ray = new Ray(cube.transform.position, (player.transform.position - cube.transform.position));
            Vector3 destPoint = ray.GetPoint(distanceFromPlayer);
            Debug.DrawRay(cube.transform.position, (player.transform.position - cube.transform.position), Color.green, .1f);
            transform.position = Vector3.Lerp(transform.position, new Vector3(destPoint.x, cube.transform.position.y - .25f, destPoint.z), Time.deltaTime);
        }
		
	}
}
