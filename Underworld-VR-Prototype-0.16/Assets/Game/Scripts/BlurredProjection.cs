using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurredProjection : MonoBehaviour {

    public GameObject player; //player Eye object
    public GameObject blurredProjection;

    public UpgradeMenu upgradeMenu;
    public WeaponsMenu weaponsMenu;
    public TutorialManager tutorialManager;

    public bool move;

    // Use this for initialization
    void Start () {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
	}

    private void OnEnable()
    {
        //transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update () {

        if (upgradeMenu.upgradeMenuOpen || weaponsMenu.weaponsMenuOpen && !tutorialManager.tutorialStart)
        {
            blurredProjection.SetActive(true);
            if (!move)
            {
                blurredProjection.transform.position = player.transform.position;
                move = true;
            }
        }
        else
        {
            blurredProjection.SetActive(false);
            move = false;
        }

    }
}
