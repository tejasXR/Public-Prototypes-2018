using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float scaleXCurrent;

    private GameManager gameManager;
    public TextMeshPro roundsSurvivedText;


    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scaleXOriginal = progressBar.transform.localScale.x - .5f;
        scaleXCurrent = 0;
        progress = 0;
	}

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update () {

        roundsSurvivedText.text = "YOU HAVE SURVIVED UNTIL ROUND " + gameManager.roundCurrent;

        scaleXCurrent = Mathf.Lerp(scaleXCurrent, progress * scaleXOriginal + .5f, Time.deltaTime * 10f);
        progressBar.transform.localScale = new Vector3(scaleXCurrent, progressBar.transform.localScale.y, progressBar.transform.localScale.z);

        if (trackedRight.gameObject.activeInHierarchy && trackedLeft.gameObject.activeInHierarchy)
        {
            controllerRight = SteamVR_Controller.Input((int)trackedRight.index);
            controllerLeft = SteamVR_Controller.Input((int)trackedLeft.index);

            if (controllerLeft.GetPress(SteamVR_Controller.ButtonMask.Trigger) || controllerRight.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                progress = Mathf.Lerp(progress , 1, Time.deltaTime * 2f);
                //ushort haptic = (ushort)(3000 * progress);
                controllerLeft.TriggerHapticPulse((ushort)(3000 * progress));
                controllerRight.TriggerHapticPulse((ushort)(3000 * progress));
                if (progress >= .98)
                {
                    ResetScene();
                }
            }
            else
            {
                progress = Mathf.Lerp(progress, 0, Time.deltaTime * 2f);
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
