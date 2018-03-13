using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour {

    public PostProcessProfile postVolume;
    private DepthOfField depthOfField;
    private Bloom bloom;

    public float depthOfFieldVolume;

    public UpgradeMenu upgradeMenu;
    public WeaponsMenu weaponsMenu;

    // Use this for initialization
    void Start () {
        //var profile = postVolume.profile;
        //depthOfField = postVolume.profile.settings.;
            
            //AddSettings<DepthOfField>().focalLength.value = 10;
        //depthOfField = ScriptableObject.CreateInstance<DepthOfField>();
        //depthOfField.enabled.Override(true);
        //depthOfField.focalLength.Override(1f);

        //postProfile = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, depthOfField);

        //postProfile.profile.settings
    }
	
	// Update is called once per frame
	void Update () {
        
        //depthOfField.focalLength.value = depthOfFieldVolume;
        //postProfile.settings.
        /*if (upgradeMenu.upgradeMenuOpen || weaponsMenu.weaponsMenuOpen && !tutorialManager.tutorialStart)
        {
            var depthOfField = postProfile.settings;
            depthOfField.
        }
        else
        {
            blurredProjection.SetActive(false);
            move = false;
        }*/

    }
}
