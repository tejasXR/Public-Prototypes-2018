using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    //public AudioClip[] music;
    //private AudioSource musicPlayer;

    public GameManager gameManager;
    public TimeManager timeManager;

    public bool playingBeginningMusic;
    public bool playingActiveMusic;

    //public bool playingMusic;
    //public int musicTrack;

    public AudioSource beginningMusicPlayer;
    public AudioSource activeMusicPlayer;
    public AudioLowPassFilter activeMusicLowPass;

    public float lowPass;

    public float activeMusicVolume;
    public float beginningMusicVolume;

    //public float musicPitch;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        //musicPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!gameManager.gameStart)
        {
            //beginningMusicPlayer.Play();
            //playingBeginningMusic = true;
            beginningMusicVolume = .55f;
            activeMusicVolume = 0;
        }

        if (gameManager.gameStart)
        {
            if (!playingActiveMusic)
            {
                activeMusicPlayer.Play();
                playingActiveMusic = true;
            }
            beginningMusicVolume = 0f;
            //lowPass = 5000;
            activeMusicVolume = .95f;

        }

        /*if (gameManager.gameStart && !playingMusic)
        {
            StartCoroutine(PlayMusic());
            playingMusic = true;
        }*/

        if (timeManager.slowDown)
        {
            //activeMusicLowPass.enabled = true;
            lowPass = 1000;
        }
        else
        {
            lowPass = 9000;
            //activeMusicLowPass.enabled = false;

        }

        if (gameManager.gameOver)
        {
            beginningMusicVolume = 1;
            activeMusicVolume = 0;
            lowPass = 10;
        }

        /*if (gameManager.upgradeActive)
        {
            activeMusicVolume = .1f;
        } else
        {
            activeMusicVolume = .1f;
        }*/

        activeMusicLowPass.cutoffFrequency = Mathf.Lerp(activeMusicLowPass.cutoffFrequency, lowPass, Time.unscaledDeltaTime);
        //musicPlayer.volume = Mathf.Lerp(musicPlayer.volume, musicVolume, Time.deltaTime * 1f);
        //musicPlayer.pitch = Mathf.Lerp(musicPlayer.pitch, musicPitch, Time.deltaTime * 5f);
        activeMusicPlayer.volume = Mathf.Lerp(activeMusicPlayer.volume, activeMusicVolume, Time.deltaTime * .25f);
        beginningMusicPlayer.volume = Mathf.Lerp(beginningMusicPlayer.volume, beginningMusicVolume, Time.deltaTime * .25f);



    }


    /*IEnumerator PlayMusic()
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
            
    }*/
}
