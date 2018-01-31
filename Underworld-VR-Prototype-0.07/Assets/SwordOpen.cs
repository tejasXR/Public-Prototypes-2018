using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOpen : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public GameObject saberSword;

    public bool swordOpen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (!swordOpen)
            {
                saberSword.SetActive(true);
                swordOpen = true;
            } else
            {
                saberSword.SetActive(false);
                swordOpen = false;
            }
        }
    }
}
