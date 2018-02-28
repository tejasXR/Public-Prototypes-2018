using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurredProjection : MonoBehaviour {

    public GameObject player; //player Eye object
    public GameObject blurredProjection;

    public UpgradeMenu upgradeMenu;
    public WeaponsMenu weaponsMenu;

    // Use this for initialization
    void Start () {
		
	}

    private void OnEnable()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update () {

        if (upgradeMenu.upgradeMenuOpen || weaponsMenu.weaponsMenuOpen)
        {
            blurredProjection.SetActive(true);
        }
        else
        {
            blurredProjection.SetActive(false);
        }

    }
}
