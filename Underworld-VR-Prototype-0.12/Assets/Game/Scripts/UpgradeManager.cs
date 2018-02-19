using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    private GameManager gameManager;

    // A list of all the total upgrades between the different rarities
    public GameObject[] blueUpgrades; //Common upgrades
    public GameObject[] greenUpgrades; //Uncommon upgrades
    public GameObject[] redUpgrades; //Rare upgrades
    public GameObject[] silverUpgrades; //Epic upgrades
    public GameObject[] goldUpgrades; //Legendary upgrades

    // A list of the 3 upgrdaes that will be chosen
    public GameObject[] upgrades;
    public GameObject upgradeTitle;
    public GameObject upgradeInstructions;
    public GameObject upgradeTimerBlock;
    //public GameObject upgrade2;
    //public GameObject upgrade3;

    public Transform[] upgradeStationSlots; //Transforms for where the chosen upgrades need to go
    public Transform upgradeTitleSlot; // Transform for where the upgrade text goes
    public Transform upgradeInstructionsSlot;
    public Transform upgradeTimerBlockSlot;

    //public Transform upgradeStation2;
    //public Transform upgradeStation3;

    private Vector3 originTextPos;

    public bool upgradeStart = false; // To know that we are in the upgrade process
    public bool upgradesRandomized = false;
    public bool upgradeSelected = false;
    public bool upgradeDone;

    // Booleans used for making sure upgrades are not the same as they are created
    bool upgradesSet;
    bool upgrade1Set;
    bool upgrade2Set;

    public float[] upgradeClassProbability;

    public float upgradeBufferTimer = 1.5f; //the buffer time between after the player upgrades and the wave starts
   

    // Use this for initialization
    void Start () {
        originTextPos = new Vector3(transform.position.x, 5, transform.position.z);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //upgradeTitle = Instantiate(upgradeTitle, transform.position, transform.rotation) as GameObject;
        upgradeTitle.transform.position = originTextPos;
        upgradeInstructions.transform.position = originTextPos;
        upgradeTimerBlock.transform.position = originTextPos;

        upgradeTitle.SetActive(false);
        upgradeInstructions.SetActive(false);
        upgradeTimerBlock.SetActive(false);

        upgradeBufferTimer = 1.5f;
    }

    // Update is called once per frame
    void Update () {

        if (gameManager.upgradeActive)
        {
            upgradeStart = true;
            upgradeDone = false;
            if (!upgradesRandomized)
            {
                upgradeTitle.SetActive(true);
                upgradeInstructions.SetActive(true);
                upgradeTimerBlock.SetActive(true);
                CheckNoRepeat();
                //RandomizeUpgrades();
                //UpgradesNull();
                //UpgradesCreated();
            } else
            {
                UpgradePlacement();
            }
        }

        if ((upgradeSelected || !gameManager.upgradeActive) && upgradeStart)
        {
            UpgradeReturn();
            //UpgradesNull();
        }
		
	}

    // Only creates a probability of the CLASS / RARITY of upgrades, not which one within the class to choose
    float UpgradeClassProbability(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            // Adds all probabilities to a total
            total += elem;
        }

        // Creates a random value within the total
        float randomPoint = Random.value * total;

        // Subtracts each part of the probabilities, and sees if this random value is in that part
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint <= probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }

        return probs.Length - 1;
    }

    GameObject RandomizeUpgrades(int i)
    {
        // Checks the upgrade class probability to create

        //for (int i = 0; i < 3; i++)
        {
            CheckRound();

            int upgradeClass = Mathf.RoundToInt(UpgradeClassProbability(upgradeClassProbability)); // chooses the actual class of upgrade

            switch (upgradeClass)
            {
                case 0:
                    upgrades[i] = blueUpgrades[Random.Range(0, blueUpgrades.Length)];
                    break;
                case 1:
                    upgrades[i] = greenUpgrades[Random.Range(0, greenUpgrades.Length)];
                    break;
                case 2:
                    upgrades[i] = redUpgrades[Random.Range(0, redUpgrades.Length)];
                    break;
                case 3:
                    upgrades[i] = silverUpgrades[Random.Range(0, silverUpgrades.Length)];
                    break;
                case 4:
                    upgrades[i] = goldUpgrades[Random.Range(0, goldUpgrades.Length)];
                    break;
            }
            //print("Hi");

            //if ()
            //upgrades[i] = Instantiate(upgrades[i], transform.position, transform.rotation) as GameObject;
            return upgrades[i];

           
        }

       


        /*
         OLD METHOD
        // For each of the 3 upgrade choices, assign a random probability to each of the classes of upgrades to choose from
        for (int i = 0; i < 3; i++)
        {
            var randomChance = Random.Range(0, 100);

            if (randomChance < 50) { upgrades[i] = blueUpgrades[Random.Range(0, blueUpgrades.Length)]; }
            if (randomChance > 50 && 75 > randomChance) { upgrades[i] = greenUpgrades[Random.Range(0, greenUpgrades.Length)]; }
            if (randomChance > 75 && 90 > randomChance) { upgrades[i] = redUpgrades[Random.Range(0, redUpgrades.Length)]; }
            if (randomChance > 90 && 95 > randomChance) { upgrades[i] = silverUpgrades[Random.Range(0, silverUpgrades.Length)]; }
            if (randomChance > 95 && 99 > randomChance) { upgrades[i] = goldUpgrades[Random.Range(0, goldUpgrades.Length)]; }
            //print(randomChance);
        }
        */
    }

    void CheckNoRepeat()
    {
       

        upgrades[0] = RandomizeUpgrades(0);
        //print("upgrades0");

        if (upgrades[1] == null && !upgrade1Set)
        {
            upgrades[1] = RandomizeUpgrades(1);
            if (upgrades[1] != upgrades[0])
            {
                upgrade1Set = true;
                //print("upgrades1");

            }
        }         

        if (upgrades[2] == null && !upgrade2Set)
        {
            upgrades[2] = RandomizeUpgrades(2);
            if (upgrades[2] != upgrades[1])
            {
                upgrade2Set = true;
                //print("upgrades2");
            }
        }

        if (upgrade1Set && upgrade2Set)
        {
            for (int i = 0; i < 3; i++)
            {
                upgrades[i] = Instantiate(upgrades[i], transform.position, transform.rotation) as GameObject;
            }

            upgradesRandomized = true;
            upgrade1Set = false;
            upgrade2Set = false;
            print("Upgrades Randomized = True");
        }


    }

    /*
    void UpgradesCreated()
    {
        for (int i = 0; i < 3; i++)
        {
            upgrades[i] = Instantiate(upgrades[i], transform.position, transform.rotation) as GameObject;
            //upgrades[i] = upgrades[i] as GameObject;
        }
    }*/

    void UpgradePlacement()
    {
        for (int i = 0; i < 3; i++)
        {
            upgrades[i].transform.position = Vector3.Lerp(upgrades[i].transform.position, upgradeStationSlots[i].transform.position, Time.deltaTime);
            upgrades[i].transform.rotation = Quaternion.Slerp(upgrades[i].transform.rotation, upgradeStationSlots[i].transform.rotation, Time.deltaTime);
            //print("upgrade moving");            
        }

        upgradeTitle.transform.position = Vector3.Lerp(upgradeTitle.transform.position, upgradeTitleSlot.transform.position, Time.deltaTime);
        upgradeTitle.transform.rotation = Quaternion.Slerp(upgradeTitle.transform.rotation, upgradeTitleSlot.transform.rotation, Time.deltaTime);

        upgradeInstructions.transform.position = Vector3.Lerp(upgradeInstructions.transform.position, upgradeInstructionsSlot.transform.position, Time.deltaTime);
        upgradeInstructions.transform.rotation = Quaternion.Slerp(upgradeInstructions.transform.rotation, upgradeInstructionsSlot.transform.rotation, Time.deltaTime);

        upgradeTimerBlock.transform.position = Vector3.Lerp(upgradeTimerBlock.transform.position, upgradeTimerBlockSlot.transform.position, Time.deltaTime);
        upgradeTimerBlock.transform.rotation = Quaternion.Slerp(upgradeTimerBlock.transform.rotation, upgradeTimerBlockSlot.transform.rotation, Time.deltaTime);

    }

    void UpgradeReturn()
    {
        upgradeBufferTimer -= Time.deltaTime;
        if (upgradeBufferTimer > 0)
        {
            foreach (GameObject item in upgrades)
            {
                item.transform.position = Vector3.Lerp(item.transform.position, new Vector3(0, -9, 0), Time.deltaTime);
            }

            upgradeTitle.transform.position = Vector3.Lerp(upgradeTitle.transform.position, originTextPos, Time.deltaTime);
            upgradeInstructions.transform.position = Vector3.Lerp(upgradeTitle.transform.position, originTextPos, Time.deltaTime);
            upgradeTimerBlock.SetActive(false);

            //upgradeTimerBlock.transform.position = Vector3.Lerp(upgradeTitle.transform.position, originTextPos, Time.deltaTime);
        }
        if (upgradeBufferTimer <= 0)
        {
            upgradeTitle.SetActive(false);
            upgradeInstructions.SetActive(false);
            upgradeTimerBlock.SetActive(false);
            upgradeSelected = false;
            upgradesRandomized = false;
            UpgradesNull();
        }
    }

    void UpgradesNull()
    {
        for (int i = 0; i < 3; i++)
        {
            //Destroy(upgrades[i].gameObject, 3f);
            upgrades[i] = null;
            //upgrades[i] = null;
           
        }
        upgradeStart = false;
        upgradeBufferTimer = 1.5f;
        upgradeDone = true;
    }

    void CheckRound()
    {
        ResetUpgradeClassProbability();

        switch (gameManager.roundCurrent)
        {
            case 1:
                upgradeClassProbability[0] = 100; // Blue / Common
                break;
            case 2:
                upgradeClassProbability[0] = 50; // Blue
                upgradeClassProbability[1] = 50; // Green
                break;
            case 3:
                upgradeClassProbability[0] = 20; // Light drones
                upgradeClassProbability[1] = 70; // Fast Drones
                upgradeClassProbability[2] = 10; // Fast Drones
                break;
            case 4:
                upgradeClassProbability[0] = 30; // Light drones
                upgradeClassProbability[1] = 70; // Fast Drones
                break;
            case 5:


                upgradeClassProbability[0] = 10; // Light drones
                upgradeClassProbability[1] = 50; // Fast Drones
                upgradeClassProbability[2] = 40; // Heavy Drones
                break;
        }
    }

    void ResetUpgradeClassProbability()
    {
        upgradeClassProbability[0] = 0; // Blue / Common Upgrades
        upgradeClassProbability[1] = 0; // Green / Uncommon Upgrades
        upgradeClassProbability[2] = 0; // Red / Rare Upgrades
        upgradeClassProbability[3] = 0; // Silver / Epic Upgrades
        upgradeClassProbability[4] = 0; // Gold / Legendary Upgrades
    }
}
