using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineMove : MonoBehaviour {

    public GameObject[] spineBones;
    public float raiseX = 0.2f;
    public float raiseY = 0.2f;
    public float raiseZ = 0.2f;

    public Vector3 originalPos;

    private int spineLength;

    public float spineTimer = 1f;

   


    // Use this for initialization
    void Start () {

        spineLength = spineBones.Length;

        originalPos = spineBones[0].transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < spineLength; i++)
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
		
	}
}
