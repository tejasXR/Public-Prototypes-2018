using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public UpgradeManager upgradeManager;
    public Player playerController;

    public float upgradeCost;

    public float addFireRateMultiplier;
    public float addDamageMultiplier;

    // Use this for initialization
    void Start () {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            playerController.playerBullets -= upgradeCost;

            AddUpgradeEffect();

            upgradeManager.upgradeSelected = true;
            
            //gameManager
        }
    }

    void AddUpgradeEffect()
    {
        foreach (int effects in effects)
        {
            playerController.bulletDamageMultiplier += addFireRateMultiplier;
            playerController.bulletDamageMultiplier += addDamageMultiplier;
        }
        
    }
}
