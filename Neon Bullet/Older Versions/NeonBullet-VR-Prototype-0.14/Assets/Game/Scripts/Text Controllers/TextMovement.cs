using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMovement : MonoBehaviour {

    public GameObject playerEye; //cameraEye object
    public GameObject textObject;
    //public TextMesh text;
    public float secondsAlive;
    public float moveZDirection;
    public bool flash;
    public bool panMove;
    public bool follow;
    //public bool lookAtPlayer;
    public float distanceFromPlayer;



    // Use this for initialization
    void Start()
    {
        playerEye = GameObject.FindGameObjectWithTag("Player");

        if(flash)
        {
            StartCoroutine(Flash());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        var playerDirection = playerEye.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = rotation;

        if (panMove)
        {
            transform.position = Vector3.Lerp(transform.position, transform.forward, Time.deltaTime * moveZDirection);
        }

        if (follow)
        {
            transform.position = Vector3.Lerp(transform.position, playerEye.transform.forward/* * distanceFromPlayer*/, Time.deltaTime * 2f);
        }
    }

    IEnumerator Flash()
    {
        textObject.SetActive(true);
        yield return new WaitForSeconds(secondsAlive);
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
