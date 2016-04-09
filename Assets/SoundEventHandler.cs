using UnityEngine;
using System.Collections;

public class SoundEventHandler : MonoBehaviour {

    public static SoundEventHandler soundEventHandler;
    bool isPlayingMusic;
    AudioClip[] prefab_punchline;
    public AudioClip[] general_punchlines;
    AudioSource audio;
    float time;
	// Use this for initialization
	void Start () {
        time = 0;
        isPlayingMusic = false;
        audio = GetComponent<AudioSource>();
        soundEventHandler = this;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time >= 2)
        {
            isPlayingMusic = false;
        }
	}
    public void whichMusic(GameObject other)
    {
        prefab_punchline = other.GetComponent<EnemyScript>().punchLines;
        float procChance = 10;
        float procRoll = Random.Range(0, 100);
        if(procRoll < procChance && !isPlayingMusic)
        {
            int chooseSong = (int)Random.Range(0, prefab_punchline.Length - 0.1f);
            playMusic(general_punchlines[chooseSong]);
            Debug.Log("NOW PLAYING GENERAL SOUND");
            isPlayingMusic = true;
        }

        procChance = 25;
        procRoll = Random.Range(0, 100);
        if (procRoll < procChance && !isPlayingMusic && prefab_punchline.Length != 0)
        {
            int chooseSong = (int)Random.Range(0, prefab_punchline.Length - 0.1f);
            playMusic(prefab_punchline[chooseSong]);
            Debug.Log("NOW PLAYING PREFAB SOUND");
            isPlayingMusic = true;
        }
    }
    public void playMusic(AudioClip music)
    {
        audio.clip = music;
        audio.PlayOneShot(music, 1);
    }
}
