using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTextCounter : MonoBehaviour {

    private Player playerController;
    public float bulletSmoothCount;
    private TextMeshPro bulletTextCounter;
    public float countSpeed = 85f;
    private bool adjust;

    public bool formatXX;
    //public bool formatXXX;

    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        bulletTextCounter = GetComponent<TextMeshPro>();
	}

    private void OnEnable()
    {
        bulletSmoothCount = 0;
    }

    // Update is called once per frame
    void Update () {

        if (adjust)
        {
            if (bulletSmoothCount <= playerController.playerBullets)
            {
                bulletSmoothCount += Time.unscaledDeltaTime * countSpeed;
            }
            else
            {
                bulletSmoothCount -= Time.unscaledDeltaTime * countSpeed;
            }
        }

        if (Mathf.Abs(bulletSmoothCount - playerController.playerBullets) > .5f)
        {
            adjust = true;
        } else
        {
            adjust = false;
        }
       

        

        //bulletSmoothCount = Mathf.RoundToInt(Mathf.Lerp(bulletSmoothCount, playerController.playerBullets, Time.deltaTime * countSpeed));

        if (formatXX)
        {
            bulletTextCounter.text = "" + Mathf.RoundToInt(bulletSmoothCount).ToString("00");
        } else
        {
            bulletTextCounter.text = "" + Mathf.RoundToInt(bulletSmoothCount).ToString("000");
        }


    }
}
