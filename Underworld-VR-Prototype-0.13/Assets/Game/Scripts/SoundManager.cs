using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource[] audioSources;
    private float[] audioSourcesPitchOriginal;

    public bool randomizePitch;
    public float randomizeAmount;

    // Use this for initialization
    private void Awake()
    {
        audioSourcesPitchOriginal = new float[audioSources.Length];

        if (randomizePitch)
        {
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].pitch += Random.Range(-randomizeAmount, randomizeAmount);
            }
        }
        
        
        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSourcesPitchOriginal[i] = audioSources[i].pitch;
        }



    }

    void Start () {




    }
	
	// Update is called once per frame
	void Update () {

        for(int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].pitch = Mathf.Lerp(audioSources[i].pitch, audioSourcesPitchOriginal[i] * Time.timeScale, Time.unscaledDeltaTime);
        }
    }
}
