using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public UpgradeManager upgradeManager;
    public Player playerController;

    public float upgradeCost;

    // Player Health Upgrades
    public float addPlayerHealth;
    public float addPlayerHealthMaxMultiplier;

    // Player Attack Upgrades
    public float addBulletFireRateMultiplier;
    public float addBulletDamageMultiplier;
    public float addBulletSpeedMultiplier;
    public float addBulletAccuracyMultiplier;

    // Use this for initialization
    void Start () {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            playerController.playerBullets -= upgradeCost;
            AddUpgradeEffect();
            upgradeManager.upgradeSelected = true;
        }
    }

    void AddUpgradeEffect()
    {
        
        playerController.bulletDamageMultiplier += addBulletFireRateMultiplier;
        playerController.bulletDamageMultiplier += addBulletDamageMultiplier;
        
        
    }
}
