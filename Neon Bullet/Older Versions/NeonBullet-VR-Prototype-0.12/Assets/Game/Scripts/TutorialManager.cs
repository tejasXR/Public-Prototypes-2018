using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public bool inTutorial;
    public bool tutorialStart;

    public int tutorialInt;
    public int tutorialBoardCounter = -1;
    public TutorialStartUI tutorialStartUI;
    public GameObject[] tutorialBoards;

    public PlayerShield tutorialShield;
    public GameObject tutorialShieldObj;
    public GameObject tutorialPistol;
    public UpgradeMenu upgradeMenu;
    public WeaponsMenu weaponMenu;
    public EnemyParent tutorialDrone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(tutorialStartUI.tutorialStart && !tutorialStart)
        {
            inTutorial = true;
            //tutorialBoardInt++;
            tutorialStart = true;
            tutorialShieldObj.SetActive(true);
            tutorialInt = 0;
            tutorialBoardCounter = -1;
        }

        if (tutorialStart)
        {
            TutorialStart();
        }

        if (!tutorialStart && upgradeMenu.upgradeMenuOpen && !upgradeMenu.upgradeSelected)
        {
            tutorialShieldObj.SetActive(false);
        } else if(tutorialStart && !upgradeMenu.upgradeMenuOpen && !upgradeMenu.upgradeSelected)
        {
            tutorialShieldObj.SetActive(true);
        }


        //if (tu)




    }


    void TutorialStart()
    {
        // Sets active the board the the tutorial is on
        if (tutorialBoardCounter != tutorialInt)
        {
            for (int i = 0; i < tutorialBoards.Length; i++)
            {
                tutorialBoards[i].SetActive(false);
            }

            tutorialBoards[tutorialInt].SetActive(true);
            tutorialBoardCounter = tutorialInt;
        }

        

        if (tutorialInt == 0)
        {
            if (tutorialShield.shieldHealth < 9)
            {
                tutorialInt++;
            }
        }

        if (tutorialInt == 1)
        {
            if (upgradeMenu.upgradeMenuActive)
            {
                tutorialInt++;
            }
        }

        if (tutorialInt == 2)
        {
            if (upgradeMenu.weaponUnlockActive)
            {
                tutorialInt++;
            }
        }

        if (tutorialInt == 3)
        {
            if (upgradeMenu.upgradeSelected)
            {
                tutorialInt++;
            }
        }

        if (tutorialInt == 4)
        {
            if (upgradeMenu.upgradeDone)
            {
                tutorialInt++;
            }
        }

        if (tutorialInt == 5)
        {
            if (weaponMenu.weaponsMenuOpen)
            {
                tutorialInt++;
            }
        }

        if (tutorialInt == 6)
        {
            /*if (weaponMenu.weaponSelected)
            {
                tutorialInt++;
            }*/

            if (tutorialDrone.enemyHealth <= 0)
            {
                tutorialInt++;
            }
        }


        if (tutorialInt == 7)
        {
            tutorialStart = false;
            /*if (tutorialDrone.enemyHealth <= 0)
            {
                tutorialInt++;
            }*/
        }
    }

    public void TutorialReset()
    {
        inTutorial = false;
        //tutorialBoardInt++;
        tutorialStart = false;
        tutorialShieldObj.SetActive(false);

        for (int i = 0; i < tutorialBoards.Length; i++)
        {
            tutorialBoards[i].SetActive(false);
        }
    }
}
