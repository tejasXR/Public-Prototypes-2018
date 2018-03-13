using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISmooth : MonoBehaviour
{

    public GameObject[] icons;
    public Vector3[] iconOriginalPos;

    private Vector3 hidePos;

    public UpgradeMenu upgradeMenu;
    public WeaponsMenu weaponsMenu;

    public bool move2;
    public bool move3;
    public bool move4;
    public bool move5;

    public bool isWeaponMenu;
    public bool isAttackMenu;
    public bool isDefenseMenu;

    public bool isIndiependantWeaponMenu;

    public bool isMainMenu;

    public float moveSpeed;
    public float percentToMove;

    // Use this for initialization
    private void Awake()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            iconOriginalPos[i] = icons[i].transform.localPosition;
        }

        hidePos = new Vector3(0f, 0f, .1f);

        foreach (GameObject icon in icons)
        {
            icon.transform.localPosition = hidePos;
        }

        //print(Vector3.Distance(icons[0].transform.localPosition, iconOriginalPos[0]));

    }

    void Start()
    {



    }

    private void OnEnable()
    {
        if (isIndiependantWeaponMenu || isMainMenu)
        {
            foreach (GameObject icon in icons)
            {
                icon.transform.localPosition = hidePos;
            }
        }

        



    }

    // Update is called once per frame
    void Update()
    {

        //StartCoroutine
        //ShowIcons();

        if (isWeaponMenu)
        {
            if (upgradeMenu.weaponUnlockActive)
            {
                ShowIcons();
            }
            else
            {
                HideIcons();
            }
        }


        if (isAttackMenu)
        {
            if (upgradeMenu.attackUpgradeActive)
            {
                ShowIcons();
                /*for(int i = 0; i < icons.Length; i++)
                {
                    icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 4f);
                }*/
            }
            else
            {
                HideIcons();
            }
        }


        if (isDefenseMenu)
        {
            if (upgradeMenu.defenseUpgradeActive)
            {
                ShowIcons();
            }
            else
            {
                HideIcons();
            }
        }

        if (isIndiependantWeaponMenu)
        {
            if (weaponsMenu.weaponsMenuOpen)
            {
                ShowIcons();
            } else
            {
                HideIcons();
            }
        }


        if (isMainMenu)
        {
            if (upgradeMenu.upgradeMenuOpen)
            {
                ShowMainMenu();
            }
            else
            {
                HideMainMenu();
            }
        }

    }

    void ShowIcons()
    {
        /*for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 4f);

            print("show");
        }*/


        /*for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 3f);
            yield return new WaitForSeconds(.1f);
        }*/

        
        icons[0].transform.localPosition = Vector3.Lerp(icons[0].transform.localPosition, iconOriginalPos[0], Time.unscaledDeltaTime * moveSpeed);

        if (move2)
        {
            icons[1].transform.localPosition = Vector3.Lerp(icons[1].transform.localPosition, iconOriginalPos[1], Time.unscaledDeltaTime * moveSpeed);
        }

        if (move3)
        {
            icons[2].transform.localPosition = Vector3.Lerp(icons[2].transform.localPosition, iconOriginalPos[2], Time.unscaledDeltaTime * moveSpeed);

        }

        if (move4)
        {
            icons[3].transform.localPosition = Vector3.Lerp(icons[3].transform.localPosition, iconOriginalPos[3], Time.unscaledDeltaTime * moveSpeed);

        }

        if (move5)
        {
            icons[4].transform.localPosition = Vector3.Lerp(icons[4].transform.localPosition, iconOriginalPos[4], Time.unscaledDeltaTime * moveSpeed);

        }


        if (Vector3.Distance(icons[0].transform.localPosition, iconOriginalPos[0]) < (1.5f - (percentToMove * 1.5f)))
        {
            move2 = true;
            if (Vector3.Distance(icons[1].transform.localPosition, iconOriginalPos[1]) < (1.5f - (percentToMove * 1.5f)))
            {
                move3 = true;

                if (Vector3.Distance(icons[2].transform.localPosition, iconOriginalPos[2]) < (1.5f - (percentToMove * 1.5f)))
                {
                    move4 = true;

                    if (Vector3.Distance(icons[3].transform.localPosition, iconOriginalPos[3]) < (1.5f - (percentToMove * 1.5f)))
                    {
                        move5 = true;

                    }
                }
            }
        }
        
    }


    void HideIcons()
    {
        /*for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, hidePos, Time.unscaledDeltaTime * 4f);
        }
        */
        //move2 = false;
        //move3 = false;
        //move4 = false;
        //move5 = false;

        icons[0].transform.localPosition = Vector3.Lerp(icons[0].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);

        if (!move2)
        {
            icons[1].transform.localPosition = Vector3.Lerp(icons[1].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);
        }

        if (!move3)
        {
            icons[2].transform.localPosition = Vector3.Lerp(icons[2].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);

        }

        if (!move4)
        {
            icons[3].transform.localPosition = Vector3.Lerp(icons[3].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);

        }

        if (!move5)
        {
            icons[4].transform.localPosition = Vector3.Lerp(icons[4].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);

        }


        if (Vector3.Distance(icons[0].transform.localPosition, iconOriginalPos[0]) < (1.5f - (percentToMove * 1.5f)))
        {
            move2 = false;
            if (Vector3.Distance(icons[1].transform.localPosition, iconOriginalPos[1]) < (1.5f - (percentToMove * 1.5f)))
            {
                move3 = false;

                if (Vector3.Distance(icons[2].transform.localPosition, iconOriginalPos[2]) < (1.5f - (percentToMove * 1.5f)))
                {
                    move4 = false;

                    if (Vector3.Distance(icons[3].transform.localPosition, iconOriginalPos[3]) < (1.5f - (percentToMove * 1.5f)))
                    {
                        move5 = false;

                    }
                }
            }
        }

    }

    void ShowMainMenu()
    {
        icons[0].transform.localPosition = Vector3.Lerp(icons[0].transform.localPosition, iconOriginalPos[0], Time.unscaledDeltaTime * moveSpeed);

        if (move2)
        {
            icons[1].transform.localPosition = Vector3.Lerp(icons[1].transform.localPosition, iconOriginalPos[1], Time.unscaledDeltaTime * moveSpeed);
        }

        if (move3)
        {
            icons[2].transform.localPosition = Vector3.Lerp(icons[2].transform.localPosition, iconOriginalPos[2], Time.unscaledDeltaTime * moveSpeed);

        }

        if (Vector3.Distance(icons[0].transform.localPosition, iconOriginalPos[0]) < (1.5f - (percentToMove * 1.5f)))
        {
            move2 = true;
            if (Vector3.Distance(icons[1].transform.localPosition, iconOriginalPos[1]) < (1.5f - (percentToMove * 1.5f)))
            {
                move3 = true;
            }
        }
    }

    void HideMainMenu()
    {
        icons[0].transform.localPosition = Vector3.Lerp(icons[0].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);

        if (!move2)
        {
            icons[1].transform.localPosition = Vector3.Lerp(icons[1].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);
        }

        if (!move3)
        {
            icons[2].transform.localPosition = Vector3.Lerp(icons[2].transform.localPosition, hidePos, Time.unscaledDeltaTime * moveSpeed);

        }

        if (Vector3.Distance(icons[0].transform.localPosition, iconOriginalPos[0]) < (1.5f - (percentToMove * 1.5f)))
        {
            move2 = false;
            if (Vector3.Distance(icons[1].transform.localPosition, iconOriginalPos[1]) < (1.5f - (percentToMove * 1.5f)))
            {
                move3 = false;
            }
        }
    }
}

