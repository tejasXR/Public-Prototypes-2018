using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISmooth : MonoBehaviour
{

    public GameObject[] icons;
    public Vector3[] iconOriginalPos;

    private Vector3 hidePos;

    public UpgradeMenu upgradeMenu;

    public bool move2;
    public bool move3;
    public bool move4;
    public bool move5;

    public bool isWeaponMenu;
    public bool isAttackMenu;
    public bool isDefenseMenu;

    // Use this for initialization
    private void Awake()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            iconOriginalPos[i] = icons[i].transform.localPosition;
        }

        hidePos = new Vector3(0f, 0f, .05f);

    }

    void Start()
    {



    }

    private void OnEnable()
    {
        /*foreach(GameObject icon in icons)
        {
            icon.transform.localPosition = Vector3.zero;
        }*/



    }

    // Update is called once per frame
    void Update()
    {

        //StartCoroutine
        ShowIcons();
        if (upgradeMenu.attackUpgradeActive && isAttackMenu)
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

    void ShowIcons()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 4f);
        }


        /*for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 3f);
            yield return new WaitForSeconds(.1f);
        }*/

        /*
        icons[0].transform.localPosition = Vector3.Lerp(icons[0].transform.localPosition, iconOriginalPos[0], Time.unscaledDeltaTime * 4f);

        if (move2)
        {
            icons[1].transform.localPosition = Vector3.Lerp(icons[1].transform.localPosition, iconOriginalPos[1], Time.unscaledDeltaTime * 4f);
            print("called1");
        }

        if (move3)
        {
            icons[2].transform.localPosition = Vector3.Lerp(icons[2].transform.localPosition, iconOriginalPos[2], Time.unscaledDeltaTime * 4f);

        }

        if (move4)
        {
            icons[3].transform.localPosition = Vector3.Lerp(icons[3].transform.localPosition, iconOriginalPos[3], Time.unscaledDeltaTime * 4f);

        }

        if (move5)
        {
            icons[4].transform.localPosition = Vector3.Lerp(icons[4].transform.localPosition, iconOriginalPos[4], Time.unscaledDeltaTime * 4f);

        }

        move2 = true;

        if (Vector3.Distance(icons[0].transform.localPosition, iconOriginalPos[0]) < .5f)
        {
            //move2 = true;
            if (Vector3.Distance(icons[1].transform.localPosition, iconOriginalPos[1]) < .5f)
            {
                move3 = true;

                if (Vector3.Distance(icons[2].transform.localPosition, iconOriginalPos[2]) < .5f)
                {
                    move4 = true;

                    if (Vector3.Distance(icons[3].transform.localPosition, iconOriginalPos[3]) < .5f)
                    {
                        move5 = true;

                    }
                }
            }
        }
        */
    }


    void HideIcons()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, hidePos, Time.unscaledDeltaTime * 4f);
        }
    }
}
