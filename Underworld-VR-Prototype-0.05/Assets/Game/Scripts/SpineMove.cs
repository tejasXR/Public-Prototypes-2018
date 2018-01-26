using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineMove : MonoBehaviour {

    //public GameObject[] spineBones;
    public float raiseX = 0.2f;
    public float raiseY = 0.2f;
    public float raiseZ = 0.2f;

    public Vector3 originalPos;
    public Vector3 targetPosition;

    private int spineLength;

    public float spineTimer;
    private float spineTimerDuration;

    public float delayTimer;

    public float moveSpeed;

    public bool isUp;
    public bool isDown;

    private float xPos;
    private float yPos;
    private float zPos;


    // Use this for initialization
    void Start () {

        //spineLength = spineBones.Length;
        //originalPos = spineBones[0].transform.position;

        originalPos = transform.localPosition;
        spineTimerDuration = spineTimer;
        //targetPosition = new Vector3(transform.localPosition.x + raiseX, transform.localPosition.y + raiseY, transform.localPosition.z + raiseZ) + transform.position;
        

        xPos = transform.localPosition.x + raiseX;
        yPos = transform.localPosition.y + raiseY;
        zPos = transform.localPosition.z + raiseZ;

        targetPosition = new Vector3(xPos, yPos, zPos);

    }

    // Update is called once per frame
    void Update () {

        //transform.localPosition = Vector3.Lerp(transform.localPosition,  new Vector3(transform.localPosition.x, testPos, transform.localPosition.z), Time.deltaTime);

        if (delayTimer >= 0)
        {
            delayTimer -= Time.deltaTime;
        }

        if (delayTimer < 0)
        {
            if (!isUp)
            {
                // Start moving is the spine is not in the target position
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);

                // Becuase all spines are in original position, count down the delay timer to cause a spine rolling effect

                spineTimer -= Time.deltaTime;
                if (spineTimer <= 0)
                {
                    spineTimer = spineTimerDuration;
                    isDown = false;
                    isUp = true;
                }
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, Time.deltaTime * moveSpeed);

                spineTimer -= Time.deltaTime;
                if (spineTimer <= 0)
                {
                    spineTimer = spineTimerDuration;
                    isDown = true;
                    isUp = false;
                }
            }
        }       

        /*
        for (int i = 0; i < spineLength; i++)
        {
            int i = 0;
            spineTimer -= Time.deltaTime;

            
            spineBones[i].transform.position = Vector3.Lerp(spineBones[i].transform.position, new Vector3(spineBones[i].transform.position.x + raiseX, 
                spineBones[i].transform.position.y + raiseY, spineBones[i].transform.position.z + raiseZ), Time.deltaTime);

            if (spineTimer <= 0)
            {
                spineBones[i].transform.position = Vector3.Lerp(spineBones[i].transform.position, originalPos, Time.deltaTime);
            }
        }

        transform.position = Vector3.Lerp(transform.position)
        */
	}
}
