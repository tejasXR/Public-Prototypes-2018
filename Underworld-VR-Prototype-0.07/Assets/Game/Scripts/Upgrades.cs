using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    public UpgradeManager upgradeManager;
    public Player playerController;
    public PlayerShield playerShield;
    public EnemyParent enemyParent;

    public float upgradeCost;

    // Player Health Upgrades
    public float addPlayerHealth;
    public float addPlayerHealthMaxMultiplier;

    // Player Attack Upgrades
    public float addBulletFireRateMultiplier;
    public float addBulletDamageMultiplier;
    //public float addBulletSpeedMultiplier; <--- No one really know why we should have this
    public float addBulletAccuracyMultiplier;

    // Player Defense Upgrades
    public float addShieldRegenerationMultiplier;
    public float addShieldHealthMaxMultiplier;
    public float addShieldAbsorptionChanceMultiplier;

    // Use this for initialization
    void Start () {
        upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        playerShield = GameObject.Find("PlayerShield").GetComponent<PlayerShield>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet" && !upgradeManager.upgradeSelected)
        {
            playerController.playerBullets -= upgradeCost;
            AddUpgradeEffect();
            upgradeManager.upgradeSelected = true;
        }
    }

    void AddUpgradeEffect()
    {
        // Attack Effects
        playerController.bulletFireRateMultiplier += addBulletFireRateMultiplier;
        playerController.bulletDamageMultiplier += addBulletDamageMultiplier;
        playerController.bulletAccuracyMultiplier += addBulletAccuracyMultiplier;

        // Player Health Effects
        playerController.playerHealthMaxMultiplier += addPlayerHealthMaxMultiplier;


        // Player Defense Effects
        playerShield.shieldRechargeSpeedMultiplier += addShieldRegenerationMultiplier;
        playerShield.shieldHealthMaxMultiplier += addShieldHealthMaxMultiplier;
        playerShield.shieldAbsorptionChanceMultiplier += addShieldAbsorptionChanceMultiplier;


        
        
    }
}
