using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedemptionKillsText : MonoBehaviour {

    public TextMeshPro killNumText;
    private GameManager gameManager;
    public GameObject playerEye;
    public Vector3 moveDirection;
    public Vector3 targetPos;
    public float moveSpeed;
    public GameObject textObj;

    // Use this for initialization
    void Start () {
        playerEye = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        killNumText.text = gameManager.enemiesDestroyed.ToString();

        StartCoroutine(SpawnMove());

    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(playerEye.transform.position);
        //transform.position = Vector3.Lerp(transform.position, transform.forward, Time.deltaTime * .01f);

        //transform.position = Vector3.Lerp(transform.position, playerEye.transform.position, Time.deltaTime * .01f);
        //transform.Translate(0, 0, 1f * Time.deltaTime);
        targetPos.z += .1f * Time.deltaTime;
        //textSolid.transform.localPosition = Vector3.Lerp(textSolid.transform.localPosition, targetPos, Time.deltaTime * movespeed);


        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.5f, 1.5f, 1.5f), Time.deltaTime * .25f);

    }

    IEnumerator SpawnMove()
    {
        moveDirection = playerEye.transform.position - transform.position;

        //moveDirection += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(0f, 1f));

        Ray ray = new Ray(transform.position, moveDirection);

        targetPos = ray.GetPoint(.5f);

        //yield return new WaitForSeconds(.25f);

        //textOutline1.transform.position = ray.GetPoint(0f);
        //textOutline1.SetActive(true);
        //yield return new WaitForSeconds(.1f);
        //textOutline1.SetActive(false);
        //yield return new WaitForSeconds(.01f);
        //textOutline2.transform.position = ray.GetPoint(.3f);
        //textOutline2.SetActive(true);
        //yield return new WaitForSeconds(.1f);
        //textOutline2.SetActive(false);
        //textSolid.transform.position = ray.GetPoint(.6f);
        textObj.SetActive(true);
        yield return new WaitForSeconds(2f);

        textObj.SetActive(false);
        yield return new WaitForSeconds(.1f);
        textObj.SetActive(true);
        yield return new WaitForSeconds(.1f);

        textObj.SetActive(false);
        yield return new WaitForSeconds(.1f);
        textObj.SetActive(true);
        yield return new WaitForSeconds(.1f);

        textObj.SetActive(false);


        //textOutline1.SetActive(false);
        //yield return new WaitForSeconds(.1f);
        //textOutline2.SetActive(false);
        //yield return new WaitForSeconds(.1f);

        //textSolid.SetActive(false);

        Destroy(this.gameObject);



    }
}
