using UnityEngine;
using System.Collections;

public class WithinRange : MonoBehaviour
{
    int dmg = 5;
    private AudioSource audio;
    private AudioClip clip ;
    private float startingPitch = 1;
    private float startringVolume = 0.7F;
    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        clip = audio.clip;
    }

    // Update is called once per frame
    void Update()
    {
        //sound script in use
        if (Input.GetKeyUp(KeyCode.Space))
        {

            float random = Random.Range(1F, 1.5F);
            float random2 = Random.Range(0.7F, 1F);
            
            audio.pitch = random;
            audio.volume = random2;

            audio.PlayOneShot(clip);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Input.GetKeyUp(KeyCode.Space))
        {
            float random = Random.Range(1F, 1.5F);
            float random2 = Random.Range(0.7F, 1F);

            audio.pitch = random;
            audio.volume = random2;

            audio.PlayOneShot(clip);

            GetComponent<AudioSource>().PlayOneShot(clip);
       
        }
    }


    
}//end class
