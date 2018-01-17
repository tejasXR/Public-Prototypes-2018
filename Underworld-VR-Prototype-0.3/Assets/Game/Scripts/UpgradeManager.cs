using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public GameManager gameManager;

    // A list of all the total upgrades between the different rarities
    public GameObject[] blueUpgrades; //Common upgrades
    public GameObject[] greenUpgrades; //Uncommon upgrades
    public GameObject[] redUpgrades; //Rare upgrades
    public GameObject[] silverUpgrades; //Epic upgrades
    public GameObject[] goldUpgrades; //Legendary upgrades

    // A list of the 3 upgrdaes that will be chosen
    public GameObject[] upgrades;

    //public GameObject upgrade2;
    //public GameObject upgrade3;

    public Transform[] upgradeStationSlots; //Transforms for where the chosen upgrades need to go
    //public Transform upgradeStation2;
    //public Transform upgradeStation3;

    public bool upgradesRandomized = false;
    public bool upgradeSelected = false;

    private float upgradeBufferTimer = 1.5f; //the buffer time between after the player upgrades and the wave starts
   

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       
    }

    // Update is called once per frame
    void Update () {

        if (gameManager.upgradeActive)
        {
            if (!upgradesRandomized)
            {
                RandomizeUpgrades();
                //UpgradesNull();
                UpgradesCreated();
                upgradeBufferTimer = 1.5f;
                upgradesRandomized = true;
            }
            UpgradePlacement();
        }

        if (upgradeSelected)
        {
            UpgradeReturn();
        }
		
	}

    void RandomizeUpgrades()
    {
        // For each of the 3 upgrade choices, assign a random probability to each of the classes of upgrades to choose from
        for (int i = 0; i < 3; i++)
        {
            var randomChance = Random.Range(0, 100);

            if (randomChance < 50) { upgrades[i] = blueUpgrades[Random.Range(0, blueUpgrades.Length)]; }
            if (randomChance > 50 && 75 > randomChance) { upgrades[i] = greenUpgrades[Random.Range(0, greenUpgrades.Length)]; }
            if (randomChance > 75 && 90 > randomChance) { upgrades[i] = redUpgrades[Random.Range(0, redUpgrades.Length)]; }
            if (randomChance > 90 && 95 > randomChance) { upgrades[i] = silverUpgrades[Random.Range(0, silverUpgrades.Length)]; }
            if (randomChance > 95 && 99 > randomChance) { upgrades[i] = goldUpgrades[Random.Range(0, goldUpgrades.Length)]; }
            print(randomChance);
        }
    }

    void UpgradesCreated()
    {
        for (int i = 0; i < 3; i++)
        {            
            upgrades[i] = Instantiate(upgrades[i], transform.position, transform.rotation) as GameObject;
        }
    }

    void UpgradePlacement()
    {
        for (int i = 0; i < 3; i++)
        {
            upgrades[i].transform.position = Vector3.Lerp(upgrades[i].transform.position, upgradeStationSlots[i].transform.position, Time.deltaTime * 2f);
            upgrades[i].transform.rotation = Quaternion.Slerp(upgrades[i].transform.rotation, upgradeStationSlots[i].transform.rotation, Time.deltaTime);
            //print("upgrade moving");
        }
    }

    void UpgradeReturn()
    {
        foreach (GameObject item in upgrades)
        {
            item.transform.position = Vector3.Lerp(item.transform.position, new Vector3(0, -15, 0), Time.deltaTime * 2f);
        }

        upgradeBufferTimer -= Time.deltaTime;
        if (upgradeBufferTimer <= 0)
        {
            gameManager.upgradeActive = false;
            upgradeSelected = false;
            upgradesRandomized = false;
        }
    }

    void UpgradesNull()
    {
        for (int i = 0; i < 3; i++)
        {
            upgrades[i] = null;
        }
    }
}
