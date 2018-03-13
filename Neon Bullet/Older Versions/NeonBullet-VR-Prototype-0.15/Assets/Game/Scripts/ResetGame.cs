using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour {

    public SteamVR_TrackedObject trackedRight;
    public SteamVR_TrackedObject trackedLeft;

    private SteamVR_Controller.Device controllerRight;
    private SteamVR_Controller.Device controllerLeft;

    public float progress;

    // Use this for initialization
    void Start () {
        progress = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (trackedRight.gameObject.activeInHierarchy && trackedLeft.gameObject.activeInHierarchy)
        {
            controllerRight = SteamVR_Controller.Input((int)trackedRight.index);
            controllerLeft = SteamVR_Controller.Input((int)trackedLeft.index);

            if (controllerRight.GetPress(SteamVR_Controller.ButtonMask.Grip) || controllerLeft.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                progress += Time.deltaTime;
                if (progress >= 1)
                {
                    ResetScene();
                }
            }
        }

    }

    void ResetScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
