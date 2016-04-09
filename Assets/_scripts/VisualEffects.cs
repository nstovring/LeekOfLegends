using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class VisualEffects : MonoBehaviour {

    public List<Sprite> soundEffectSprites = new List<Sprite>(10);
    public Transform soundEffecTransform;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    public IEnumerator DisplaySoundEffectS()
    {
        SpriteRenderer sRenderer = soundEffecTransform.GetComponent<SpriteRenderer>();
        sRenderer.sprite = soundEffectSprites[Random.Range(0, soundEffectSprites.Count)];
        yield return new WaitForSeconds(0.5f);
        sRenderer.sprite = null;
        StopCoroutine("DisplaySoundEffects");
    }
}
