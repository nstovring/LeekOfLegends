using UnityEngine;
using System.Collections;

public class SoundEventHandler : MonoBehaviour {

    public static SoundEventHandler soundEventHandler;
    bool isPlayingMusic;
    AudioClip[] prefab_punchline;
    public AudioClip[] general_punchlines;
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
        prefab_punchline = other.GetComponent<EnemyScript>().punchLines;
        float procChance = 10;
        float procRoll = Random.Range(0, 100);
        if(procRoll < procChance && !audio.isPlaying)
        {
            int chooseSong = (int)Random.Range(0, prefab_punchline.Length - 0.1f);
            playMusic(general_punchlines[chooseSong]);
        }

        procChance = 25;
        procRoll = Random.Range(0, 100);
        if (procRoll < procChance && !audio.isPlaying && prefab_punchline.Length != 0)
        {
            int chooseSong = (int)Random.Range(0, prefab_punchline.Length - 0.1f);
            playMusic(prefab_punchline[chooseSong]);
        }
    }
    public void playMusic(AudioClip music)
    {
        audio.PlayOneShot(music);
    }
}
