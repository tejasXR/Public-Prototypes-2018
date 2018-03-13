using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    private GameManager gameManager;
    private GameObject playerController;

    public GameObject platformPieces;

    public float scaleCurrent;
    public float scaleOriginal;

    public bool moving;
    public bool scaling;
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    public float maxSpeed;

    public float distanceTotal;
    public float distanceCurrent;

    //private Renderer rend;
    //private Renderer platformPiecesRend;



    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("PlayerController");

        scaleOriginal = transform.localScale.x;
        scaleCurrent = transform.localScale.x;
        distanceTotal = Vector3.Distance(transform.position, Vector3.zero);

        //rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        distanceCurrent = Vector3.Distance(transform.position, Vector3.zero);
        float distancePercent = (distanceTotal - distanceCurrent) / distanceTotal;
        //print(distancePercent);

        /*foreach(GameObject piece in platformPieces.GetComponent<PlatformPieces>().pieces)
        {
            piece.GetComponent<Renderer>().material = rend.material;
        }*/

        if (moving)
        {
           if (distancePercent < .05f)
            {
                maxSpeed = Mathf.Lerp(maxSpeed, 7f, Time.deltaTime);
            } else if (distancePercent > .1f && distancePercent < .9f)
            {
                maxSpeed = Mathf.Lerp(maxSpeed, 10f, Time.deltaTime);
            } else if (distancePercent > .9f)
            {
                maxSpeed = Mathf.Lerp(maxSpeed, 5f, Time.deltaTime);
                //print("slowing down");
            }
           
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), ref velocity, smoothTime, maxSpeed);
            playerController.transform.position = Vector3.SmoothDamp(playerController.transform.position, Vector3.zero, ref velocity, smoothTime, maxSpeed);

            if (distanceCurrent < .01f)
            {
                moving = false;
            }
        }

        if (scaling)
        {
            // Scaling based on distance
            //var distancePercent = (distanceTotal - distanceCurrent) / distanceTotal;
            //scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal + ((1 - scaleOriginal) * distancePercent), Time.deltaTime * 2f);
            //scaleCurrent = Mathf.Lerp(scaleCurrent, 1, Time.deltaTime);
            // Simple scaling i.e., scale when at the top of the stadium
            scaleCurrent = Mathf.Lerp(scaleCurrent, .5f, Time.deltaTime * 3f);
            transform.localScale = new Vector3(scaleCurrent, scaleCurrent, scaleCurrent);
            //platformPieces.SetActive(true);
            //platformPieces.GetComponent<PlatformPieces>().go = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 60f, 0f)), Time.deltaTime * 3f);


            if (((.5f - scaleCurrent) < .005f))
            {
                //transform.localScale = new Vector3(1, 1, 1);
                //transform.rotation = Quaternion.Euler(Vector3.zero);
                //platformPieces.SetActive(false);
                scaling = false;
                platformPieces.SetActive(true);
                platformPieces.GetComponent<PlatformPieces>().go = true;
            }

        }
		
	}
}
