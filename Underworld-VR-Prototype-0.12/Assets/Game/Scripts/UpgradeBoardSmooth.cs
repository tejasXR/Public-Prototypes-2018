using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBoardSmooth : MonoBehaviour {

    //public GameObject player;
    private Vector3 originalPos;
    public float negativeZ;

    // Use this for initialization
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //transform.localPosition = Vector3.zero;
        originalPos = transform.localPosition;


    }

    void Start () {
        //transform.localPosition = originalPos;
    }

    private void OnEnable()
    {
        transform.localPosition = originalPos;

    }



    // Update is called once per frame
    void Update () {

        //transform.LookAt(player.transform.position);

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, -negativeZ), Time.unscaledDeltaTime * 5f);
		
	}
}
