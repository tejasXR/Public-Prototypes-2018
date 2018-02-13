using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour {

    //public UpgradeManager upgradeManager;
    public Player playerController;
    public PlayerShield playerShield;
    public WeaponActive weaponActive;

    public float upgradeCost;

    // Player Attack Upgrades
    public float addBulletFireRateMultiplier;
    public float addBulletDamageMultiplier;
    public float addBulletAccuracyMultiplier;
    public float addBulletCapacity;
    public float addEnemyAdditionalBullets;

    // Player Defense Upgrades
    public float addShieldHealthMaxMultiplier;
    public float addShieldRegenerationMultiplier;
    public float addShieldAbsorbtionBulletAmount;
    public float addPlayerHealthMaxMultiplier;
    public float addPlayerHealthChance;

    // Player Weapon Unlocks
    public bool unlockPistol;
    public bool unlockRifle;
    public bool unlockShotgun;
    public bool unlockSaberSword;
    public bool unlockHyperRifle;

    // UNUSED UPGRADE VARIABLES
    //public float addBulletSpeedMultiplier; <--- No one really know why we should have this
    //public float addNoUseBulletChance;
    //public float addBulletCriticalHitChance;
    //public float addBulletRegeneration;
    //public float addShieldSizeMultipler;
    //public float addPlayerHealthRegenMultiplier;
    //public float addEnemyNegativeHealthMultiplier;

    private void Awake()
    {
        //upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        //playerShield = GameObject.FindGameObjectWithTag("Shield").GetComponent<PlayerShield>();
        weaponActive = GameObject.FindGameObjectWithTag("WeaponHand").GetComponent<WeaponActive>();
    }


    // Use this for initialization
    void Start () {
        //upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        //playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        //playerShield = GameObject.Find("PlayerShield").GetComponent<PlayerShield>();
    }

    private void OnEnable()
    {
        //upgradeManager = GameObject.Find("UpgradeManager").GetComponent<UpgradeManager>();
        //playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        //playerShield = GameObject.FindGameObjectWithTag("Shield").GetComponent<PlayerShield>();
        //weaponActive = GameObject.FindGameObjectWithTag("WeaponHand").GetComponent<WeaponActive>();
    }

    void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.tag == "Bullet" && !upgradeManager.upgradeSelected)
        {
            if (playerController.playerBullets >= upgradeCost)
            {
                playerController.playerBullets -= upgradeCost;
                AddUpgradeEffect();
                upgradeManager.upgradeSelected = true;
            }            
        }

        Destroy(other.gameObject);*/
    }

    public void AddUpgradeEffect()
    {
        // Attack Upgrade Effects
        playerController.bulletFireRateMultiplier += addBulletFireRateMultiplier;
        playerController.bulletDamageMultiplier += addBulletDamageMultiplier;
        playerController.bulletAccuracyMultiplier += addBulletAccuracyMultiplier;
        playerController.playerBulletCapacity += addBulletCapacity;
        playerController.enemyGiveAdditionalBullets += addEnemyAdditionalBullets;

        // Player Defense Upgrade Effects
        playerShield.shieldHealthMaxMultiplier += addShieldHealthMaxMultiplier;
        playerShield.shieldRegenMultiplier += addShieldRegenerationMultiplier;
        playerShield.shieldBulletAbsorbtionAmount += addShieldAbsorbtionBulletAmount;
        playerController.playerHealthMaxMultiplier += addPlayerHealthMaxMultiplier;
        playerController.playerHealthChance += addPlayerHealthChance;

        // Player Weapon Unlocks
        if (unlockPistol)
        {
            weaponActive.unlockPistol = true;
            weaponActive.WeaponToActivate("PISTOL");
        }
        else if (unlockRifle)
        {
            weaponActive.unlockRifle = true;

            weaponActive.WeaponToActivate("RIFLE");
        }
        else if (unlockShotgun)
        {
            weaponActive.unlockShotgun = true;

            weaponActive.WeaponToActivate("SHOTGUN");
        }
        else if (unlockSaberSword)
        {
            weaponActive.unlockSaberSword = true;

            weaponActive.WeaponToActivate("SABER SWORD");
        }
        else if (unlockHyperRifle)
        {
            weaponActive.unlockHyperRifle = true;

            weaponActive.WeaponToActivate("HYPER RIFLE");
        }

        // UNUSED Upgrade Effects

        //playerController.playerNoUseBulletChance += addNoUseBulletChance;
        //playerController.playerBulletCriticalHitChance += addBulletCriticalHitChance;
        //playerController.playerHealthRegenMultiplier += addPlayerHealthRegenMultiplier;       
        //playerShield.shieldSizeMultiplier += addShieldSizeMultipler;      
        //playerController.enemyNegativeHealthMultiplier += addEnemyNegativeHealthMultiplier;





    }
}
