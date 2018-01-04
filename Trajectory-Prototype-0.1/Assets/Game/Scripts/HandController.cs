using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public bool cubeFollow = false;

    private Rigidbody rb;

    private GameObject cube;
    public GameObject player; //cameraRig object


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        cube = GameObject.Find("Cube");
	}
	
	// Update is called once per frame
	void Update ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (cubeFollow)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(cube.transform.position.x, cube.transform.position.y - .5f, cube.transform.position.z), Time.deltaTime);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Teleport();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "cube")
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }

            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(other);
            }
        }
    }

    void GrabObject(Collider other)
    {
        Rigidbody cubeRb = other.gameObject.GetComponent<Rigidbody>();
        cubeRb.isKinematic = true;
        cubeRb.useGravity = false;
        other.transform.parent = trackedObj.transform;
        cubeFollow = false;
    }

    void ThrowObject(Collider other)
    {
        
        Rigidbody cubeRb = other.gameObject.GetComponent<Rigidbody>();
        cubeRb.isKinematic = false;
        cubeRb.useGravity = true;

        cubeRb.velocity = device.velocity;
        cubeRb.angularVelocity = device.angularVelocity;

        other.transform.parent = null;
        cubeFollow = true;

    }

    void Teleport()
    {
        player.transform.position = cube.transform.position;
        //GrabObject(other);
    }
}
