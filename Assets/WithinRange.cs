using UnityEngine;
using System.Collections;

public class WithinRange : MonoBehaviour
{
    int dmg = 5;
    private AudioSource audio;
    public AudioClip punch ;
    public AudioClip landingClip;
    private float startingPitch = 1;
    private float startringVolume = 0.7F;
    private float time = 1;
 
    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        //punch = audio.clip;
    }

    // Update is called once per frame
    private void Update()
    {

      

        if (Input.GetKeyUp(KeyCode.L))
        {
            AudioSource.PlayClipAtPoint(landingClip, transform.position);
        }


            //punching sound
            if (Input.GetKeyUp(KeyCode.Space))
        {

            float random = Random.Range(1F, 1.5F);
            float random2 = Random.Range(0.7F, 1F);

            audio.pitch = random;
            audio.volume = random2;

            audio.PlayOneShot(punch);
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

            audio.PlayOneShot(punch);

            GetComponent<AudioSource>().PlayOneShot(punch);
       
        }
    }


    
}//end class
