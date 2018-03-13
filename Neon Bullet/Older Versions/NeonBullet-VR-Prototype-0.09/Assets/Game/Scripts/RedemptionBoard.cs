using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionBoard : MonoBehaviour {

    private GameManager gameManager;

    public GameObject[] triangleStadiums;
    public GameObject triangleCenter;
    public bool countDown;


    // Use this for initialization
    void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}

    private void OnEnable()
    {
        triangleCenter.SetActive(false);

        for (int i = 0; i < triangleStadiums.Length; i++)
        {
            triangleStadiums[i].SetActive(false);
        }


        StartCoroutine(RedemptionBoardEnable());
    }

    // Update is called once per frame
    void Update () {

        

        if (!countDown && gameManager.redemptionActive)
        {
            StartCoroutine(RedemptionBoardCountDown());
            countDown = true;
        }

		
	}

    IEnumerator RedemptionBoardEnable()
    {

        for(int i = 0; i <triangleStadiums.Length; i++)
        {
            triangleStadiums[i].SetActive(true);
            yield return new WaitForSeconds(.05f);
        }

        triangleCenter.SetActive(false);
        yield return new WaitForSeconds(.01f);
        triangleCenter.SetActive(true);
        yield return new WaitForSeconds(.01f);
        triangleCenter.SetActive(false);
        yield return new WaitForSeconds(.05f);
        triangleCenter.SetActive(true);
        yield return new WaitForSeconds(.05f);
        triangleCenter.SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleCenter.SetActive(true);
        yield return new WaitForSeconds(.1f);


    }

    IEnumerator RedemptionBoardCountDown()
    {
        for (int i = 9; i > -1; i--)
        {
            triangleStadiums[i].SetActive(false);
            yield return new WaitForSeconds(gameManager.redemptionMeterMax / 10);
        }

        triangleCenter.SetActive(false);
    }
}
