using UnityEngine;
using System.Collections;

public class SoundEventHandler : MonoBehaviour {

    public static SoundEventHandler soundEventHandler;
    bool isPlayingMusic;
    AudioClip[] music;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        soundEventHandler = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void whichMusic(GameObject other)
    {
        music = other.GetComponent<EnemyScript>().punchLines;
        float procChance = 25;
        float procRoll = Random.Range(0, 100);
        if(procRoll < procChance && !audio.isPlaying)
        {
            int chooseSong = (int)Random.Range(0, music.Length - 0.1f);
            playMusic(music[chooseSong]);
        }
    }
    public void playMusic(AudioClip music)
    {
        //audio.PlayOneShot(music);
    }
}
