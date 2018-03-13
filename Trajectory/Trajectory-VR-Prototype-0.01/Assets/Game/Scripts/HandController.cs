using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public bool grabbedObj = false;

    private Rigidbody rb;



	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        

        
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
        grabbedObj = true;
        if (grabbedObj)
        {
            print("graddedObj true");
        }
    }

    void ThrowObject(Collider other)
    {
        
        Rigidbody cubeRb = other.gameObject.GetComponent<Rigidbody>();
        cubeRb.isKinematic = false;
        cubeRb.useGravity = true;

        cubeRb.velocity = device.velocity;
        cubeRb.angularVelocity = device.angularVelocity;

        other.transform.parent = null;
        grabbedObj = false;
    }

    
}
