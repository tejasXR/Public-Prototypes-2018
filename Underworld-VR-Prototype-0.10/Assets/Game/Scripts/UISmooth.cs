using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISmooth : MonoBehaviour {

    public GameObject[] icons;
    public Vector3[] iconOriginalPos;

    private Vector3 hidePos;

    public UpgradeMenu upgradeMenu;

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

        hidePos = new Vector3(0f, 0f, -.05f);

    }

    void Start () {

        
		
	}

    private void OnEnable()
    {
        /*foreach(GameObject icon in icons)
        {
            icon.transform.localPosition = Vector3.zero;
        }*/

        

    }

    // Update is called once per frame
    void Update () {

        if (upgradeMenu.attackUpgradeActive && isAttackMenu)
        {
            ShowIcons();
            /*for(int i = 0; i < icons.Length; i++)
            {
                icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 2f);
            }*/
        } else
        {
            for (int i = 0; i < icons.Length; i++)
            {
                icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, hidePos, Time.unscaledDeltaTime * 2f);
            }
        }
		
	}

    IEnumerator ShowIcons()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].transform.localPosition = Vector3.Lerp(icons[i].transform.localPosition, iconOriginalPos[i], Time.unscaledDeltaTime * 3f);
            yield return new WaitForSeconds(.1f);
        }
    }
}
