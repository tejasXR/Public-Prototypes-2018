using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public TimeManager timeManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            timeManager.slowDown = true;

            timeManager.DoSlowMotion();
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            timeManager.slowDown = false;

        }
    }
}
