using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitle : MonoBehaviour {

    private GameManager gameManager;

    public float delay;

    public GameObject[] titles;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach(GameObject title in titles)
        {
            title.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (gameManager.gameStart)
        {
            delay -= Time.deltaTime;
        }

        if (delay <= 0)
        {
            StartCoroutine(ShowTitle());
        }

	}

    IEnumerator ShowTitle()
    {
        titles[0].SetActive(true);
        yield return new WaitForSeconds(.1f);

        titles[1].SetActive(true);
        yield return new WaitForSeconds(.1f);

        titles[2].SetActive(true);
        yield return new WaitForSeconds(.1f);



        //this.gameObject.SetActive(false);

       
    }
}
