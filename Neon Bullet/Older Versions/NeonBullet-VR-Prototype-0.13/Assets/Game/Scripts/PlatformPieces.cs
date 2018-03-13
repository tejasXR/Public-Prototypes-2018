using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPieces : MonoBehaviour {

    public GameObject[] pieces;
    public Vector3[] originalPos;
    public Vector3[] targetPos;
    public GameManager gameManager;

    public bool moving;
    public bool flash;

    public bool go;


    // Use this for initialization
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void OnEnable()
    {
        moving = true;
    }

    void Start () {
        for(int i = 0; i < 3; i++)
        {
            originalPos[i] = pieces[i].transform.localPosition;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (moving)
        {
            //for(int i = 0; i < 3; i++)
            {
                //pieces[0].transform.position = targetPos[0];
                //pieces[1].transform.position = targetPos[1];
                //pieces[2].transform.position = targetPos[2];
            }

            for (int i = 0; i < 3; i++)
            {
                pieces[i].transform.localPosition = Vector3.Lerp(pieces[i].transform.localPosition, targetPos[i], Time.deltaTime * 2f);
                pieces[i].transform.localRotation = Quaternion.Lerp(pieces[i].transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 2f);
            }
        }

		
	}
}
