using UnityEngine;
using System.Collections;

public class WithinRange : MonoBehaviour
{
    int dmg = 5;
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && Input.GetKeyUp(KeyCode.Space))
        {
            AudioClip clip = GetComponent<AudioSource>().clip;
            GetComponent<AudioSource>().PlayOneShot(clip);
            other.transform.GetComponent<EnemyScript>().recieveDamage(dmg);
        }
    }

}//end class
