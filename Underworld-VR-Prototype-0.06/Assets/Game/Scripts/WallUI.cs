using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallUI : MonoBehaviour {

    private GameManager gameManager;

    public Transform UIStartPosition;
    public Transform UITargetPosition;
    public Transform timerStartTransform;
    public Transform timerTargetTransform;
    public GameObject timerObj;
    public TextMeshPro roundTitle;

    public GameObject wallUIWhole;

    public float moveSpeed;
    public float alpha;
    public float bufferTime; // buffer time before the timer ui drops down

	// Use this for initialization
	void Start () {
        alpha = 0;
        transform.position = UIStartPosition.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    private void OnEnable()
    {
        transform.position = UIStartPosition.position;
        timerObj.transform.position = timerStartTransform.position;

    }

    // Update is called once per frame
    void Update () {

        if (gameManager.roundActive)
        {
            alpha = Mathf.SmoothStep(alpha, 1, Time.deltaTime * 2.5f);
            
            transform.position = Vector3.Lerp(transform.position, UITargetPosition.position, Time.deltaTime * moveSpeed);

            bufferTime -= Time.deltaTime;
            if (bufferTime <= 0)
            {
                timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerTargetTransform.position, Time.deltaTime);
                bufferTime = 0;
            }
        } else //automatically fade out if the player is not in a round
        {
            timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerStartTransform.position, Time.deltaTime * 1.5f);
            alpha = Mathf.SmoothStep(alpha, 0, Time.deltaTime * 7f);

            if (alpha < .01)
            {
                wallUIWhole.SetActive(false);
            }
        }

        if (gameManager.redemptionActive)
        {
            wallUIWhole.SetActive(false);
        }

        roundTitle.alpha = alpha;

    }
}
