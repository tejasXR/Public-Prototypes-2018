using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoices : MonoBehaviour {

    private EnemyParent enemyParent;

    public float enemyTalkTimer;
    public float enemyTalkTimerDuration;
    public bool enemyTalking;

    public AudioClip[] voices;
    public AudioSource enemyVoices;

    // Use this for initialization
    void Start () {
        enemyParent = GetComponent<EnemyParent>();

        enemyTalkTimer += Random.Range(-2f, 2f);
        enemyTalkTimerDuration = enemyTalkTimer;

    }

    // Update is called once per frame
    void Update () {

        enemyTalkTimerDuration -= Time.deltaTime;
        if(enemyTalkTimerDuration <= 0 && !enemyTalking)
        {
            enemyTalkTimerDuration = 0;
            EnemyTalk();
            enemyTalking = true;
        }

    }


    void EnemyTalk()
    {
        var randomInt = Random.Range(0, voices.Length);
        //var timer = voices[randomInt].length;

        //enemyTalking = true;

        //if (!enemyTalking)
        {
            //timer -= Time.deltaTime;
            enemyVoices.PlayOneShot(voices[randomInt]);
            

            enemyParent.EnemyTalkingGlow();

            //if (timer <= 0)
            {
                enemyTalkTimer += Random.Range(-2f, 2f);
                enemyTalkTimerDuration = enemyTalkTimer;
                enemyTalking = false;
            }
        }
    }
}
