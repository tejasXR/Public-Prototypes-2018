using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletTextCounter : MonoBehaviour {

    private Player playerController;
    private float bulletSmoothCount;
    private TextMeshPro bulletTextCounter;
    public float countSpeed;
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
        bulletSmoothCount = Mathf.RoundToInt(Mathf.Lerp(bulletSmoothCount, playerController.playerBullets, Time.deltaTime * countSpeed));

        bulletTextCounter.text = "" + bulletSmoothCount.ToString();


    }
}
