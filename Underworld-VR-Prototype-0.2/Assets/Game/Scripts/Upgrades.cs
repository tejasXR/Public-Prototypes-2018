using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public UpgradeManager upgradeManager;

    // Use this for initialization
    void Start () {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            upgradeManager.upgradeSelected = true;
            //gameManager
        }
    }
}
