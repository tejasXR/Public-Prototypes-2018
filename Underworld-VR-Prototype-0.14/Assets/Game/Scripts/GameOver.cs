using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public SteamVR_TrackedObject trackedRight;
    public SteamVR_TrackedObject trackedLeft;

    private SteamVR_Controller.Device controllerRight;
    private SteamVR_Controller.Device controllerLeft;

    public float progress;
    public GameObject progressBar;
    public float scaleXOriginal;
    //public float scaleXCurrent;


    // Use this for initialization
    void Start () {
        scaleXOriginal = progressBar.transform.localScale.x;
        progress = 0;
	}
	
	// Update is called once per frame
	void Update () {
        controllerRight = SteamVR_Controller.Input((int)trackedRight.index);
        controllerLeft = SteamVR_Controller.Input((int)trackedLeft.index);

        //scaleXCurrent = progress * scaleXOriginal;
        progressBar.transform.localScale = new Vector3(progress * scaleXOriginal, transform.localScale.y, transform.localScale.z);

        if (trackedRight.gameObject.activeInHierarchy && trackedLeft.gameObject.activeInHierarchy)
        {
            if (controllerLeft.GetPress(SteamVR_Controller.ButtonMask.Trigger) || controllerLeft.GetPress(SteamVR_Controller.ButtonMask.Touchpad)
                    || controllerRight.GetPress(SteamVR_Controller.ButtonMask.Touchpad) || controllerRight.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                progress += .01f;
                if (progress >= 1)
                {
                    ResetScene();
                }
            }
            else
            {
                progress -= .01f;
                if (progress <= 0)
                {
                    progress = 0;
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
