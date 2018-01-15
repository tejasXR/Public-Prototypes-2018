using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnBulletsText : MonoBehaviour {

    public GameObject player;
    public GameObject textObject;
    public TextMesh text;
    

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Flash());
    }

    // Update is called once per frame
    void Update () {
        var playerDirection = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = rotation;

        transform.position = Vector3.Lerp(transform.position, transform.forward,  Time.deltaTime * 0.02f);
    }

    IEnumerator Flash()
    {
        textObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        textObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        textObject.SetActive(false);
        yield return new WaitForSeconds(.05f);

        Destroy(this.gameObject);

    }

}
