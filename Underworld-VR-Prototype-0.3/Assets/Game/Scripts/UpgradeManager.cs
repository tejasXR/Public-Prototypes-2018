using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public GameManager gameManager;

    public GameObject upgrade1;
    public GameObject upgrade2;
    public GameObject upgrade3;

    public Transform upgradeStation1;
    public Transform upgradeStation2;
    public Transform upgradeStation3;

    public bool upgradeSelected = false;

    private float upgradeBufferTimer = 1.5f; //the buffer time between after the player upgrades and the wave starts
   

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //upgrade1.SetActive(false);
        //upgrade2.SetActive(false);
        //upgrade3.SetActive(false);

        //UpgradeReturn();
    }

    // Update is called once per frame
    void Update () {

        if (gameManager.upgradeActive)
        {
            //print("UpgradeManager");

            UpgradePlacement();
            
        }

        if (upgradeSelected)
        {
            UpgradeReturn();
            //gameManager.StartNewWave();
            //upgradeSelected = false;
        }
		
	}

    void UpgradePlacement()
    {
        upgrade1.transform.position = Vector3.Lerp(upgrade1.transform.position, upgradeStation1.transform.position, Time.deltaTime * 2f);
        upgrade1.transform.rotation = Quaternion.Slerp(upgrade1.transform.rotation, upgradeStation1.transform.rotation, Time.deltaTime);

        upgrade2.transform.position = Vector3.Lerp(upgrade2.transform.position, upgradeStation2.transform.position, Time.deltaTime * 2f);
        upgrade2.transform.rotation = Quaternion.Slerp(upgrade2.transform.rotation, upgradeStation2.transform.rotation, Time.deltaTime);

        upgrade3.transform.position = Vector3.Lerp(upgrade3.transform.position, upgradeStation3.transform.position, Time.deltaTime * 2f);
        upgrade3.transform.rotation = Quaternion.Slerp(upgrade3.transform.rotation, upgradeStation3.transform.rotation, Time.deltaTime);
    }

    void UpgradeReturn()
    {
        upgrade1.transform.position = Vector3.Lerp(upgrade1.transform.position, new Vector3(0, -15, 0), Time.deltaTime * 2f);
        upgrade2.transform.position = Vector3.Lerp(upgrade2.transform.position, new Vector3(0, -15, 0), Time.deltaTime * 2f);
        upgrade3.transform.position = Vector3.Lerp(upgrade3.transform.position, new Vector3(0, -15, 0), Time.deltaTime * 2f);

        upgradeBufferTimer -= Time.deltaTime;
        if (upgradeBufferTimer <= 0)
        {
            gameManager.upgradeActive = false;
            upgradeSelected = false;
        }


    }
}
