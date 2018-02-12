using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoardSmooth : MonoBehaviour {

    public Vector3 originalPos;
    //public GameObject player;

    // Use this for initialization
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        transform.localPosition = originalPos;
        transform.localPosition = Vector3.zero;

    }

    void Start () {
		
	}

    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;

    }



    // Update is called once per frame
    void Update () {

        //transform.LookAt(player.transform.position);

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, -.03f), Time.unscaledDeltaTime * 5f);
		
	}
}
