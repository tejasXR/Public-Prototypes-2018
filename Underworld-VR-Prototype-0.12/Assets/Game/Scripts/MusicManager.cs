using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] music;
    private AudioSource musicPlayer;

    public GameManager gameManager;
    public TimeManager timeManager;

    public bool playingMusic;
    public int musicTrack;

    public float musicVolume;
    public float musicPitch;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        musicPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameManager.gameStart && !playingMusic)
        {
            StartCoroutine(PlayMusic());
            playingMusic = true;
        }

        if (timeManager.slowDown)
        {
            musicPitch = .8f;
        }
        else
        {
            musicPitch = 1f;
        }

        if (gameManager.upgradeActive)
        {
            musicVolume = .1f;
        } else
        {
            musicVolume = .25f;
        }


        musicPlayer.volume = Mathf.Lerp(musicPlayer.volume, musicVolume, Time.deltaTime * 1f);
        //musicPlayer.pitch = Mathf.Lerp(musicPlayer.pitch, musicPitch, Time.deltaTime * 5f);


    }


    IEnumerator PlayMusic()
    {
        musicPlayer.clip = music[musicTrack];
        musicPlayer.PlayOneShot(musicPlayer.clip);
        yield return new WaitForSeconds(musicPlayer.clip.length);
        yield return new WaitForSeconds(1f);
        musicTrack++;
        if (musicTrack > music.Length)
        {
            musicTrack = 0;
        }

        playingMusic = false;
            
    }
}
