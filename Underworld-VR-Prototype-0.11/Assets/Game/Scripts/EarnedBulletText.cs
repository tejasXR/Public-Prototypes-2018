using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EarnedBulletText : MonoBehaviour {

    public GameObject playerEye;

    public GameObject textOutline1;
    public GameObject textOutline2;
    public GameObject textSolid;
    public TextMeshPro[] texts;
    public float bulletNumber;

    public Vector3 moveDirection;


	// Use this for initialization
	void Start () {
        playerEye = GameObject.FindGameObjectWithTag("Player");

        foreach(TextMeshPro text in texts)
        {
            text.text = "" + bulletNumber.ToString();
        }

        textOutline1.SetActive(false);
        textSolid.SetActive(false);
        StartCoroutine(EarnedBulletSpawn());
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector3.Lerp(transform.position, moveDirection, Time.deltaTime);
        //Vector3.MoveTowards(transform.position, playerController.transform.position, Time.deltaTime * 2f); 
	}

    IEnumerator EarnedBulletSpawn()
    {
        moveDirection = playerEye.transform.position - transform.position;

        moveDirection += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(0f, 1f));

        Ray ray = new Ray(transform.position, moveDirection);

        yield return new WaitForSeconds(.5f);

        textOutline1.transform.position = transform.position;
        textOutline1.SetActive(true);
        yield return new WaitForSeconds(.1f);


        /*textOutline2.transform.position = ray.GetPoint(.5f);
        textOutline2.SetActive(true);
        yield return new WaitForSeconds(.1f);*/


        textSolid.transform.position = ray.GetPoint(1f);
        textSolid.SetActive(true);
        yield return new WaitForSeconds(1.25f);




        textOutline1.SetActive(false);
        //yield return new WaitForSeconds(.1f);
        //textOutline2.SetActive(false);
        yield return new WaitForSeconds(.1f);

        textSolid.SetActive(false);

        Destroy(this.gameObject);



    }
}
