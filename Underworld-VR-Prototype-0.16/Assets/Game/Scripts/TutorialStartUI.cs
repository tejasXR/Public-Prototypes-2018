using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialStartUI : MonoBehaviour {

    public SteamVR_TrackedObject trackedRight;
    public SteamVR_TrackedObject trackedLeft;

    private SteamVR_Controller.Device controllerRight;
    private SteamVR_Controller.Device controllerLeft;

    //public TMP_FontAsset[] fonts;

    public Color32[] textColor;

    private TutorialManager tutorialManager;

    private TextMeshPro text;

    //public GameObject interactionUI;

    private Renderer rend;

    public bool tutorialStart;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<Renderer>();
        //rend.material = mats[0];
        text = GetComponent<TextMeshPro>();
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

        text.color = textColor[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //rend.material = mats[1];

        text.color = textColor[1];

        if (trackedRight.gameObject.activeInHierarchy && trackedLeft.gameObject.activeInHierarchy)
        {
            controllerRight = SteamVR_Controller.Input((int)trackedRight.index);
            controllerLeft = SteamVR_Controller.Input((int)trackedLeft.index);

            if (controllerRight.GetPress(SteamVR_Controller.ButtonMask.Trigger) || controllerLeft.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                tutorialStart = true;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!tutorialStart)
        {
            //rend.material = mats[0];

            text.color = textColor[0];

        }
    }
}
