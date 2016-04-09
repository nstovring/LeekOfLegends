using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

public class VisualEffects : MonoBehaviour {

    public List<Sprite> soundEffectSprites = new List<Sprite>(10);
    public Transform soundEffecTransform;
    private SpriteRenderer sRenderer;
    // Use this for initialization
    void Start () {
        sRenderer = soundEffecTransform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    public IEnumerator DisplaySoundEffectS()
    {
        GameObject effect = Instantiate(new GameObject("effect"),soundEffecTransform.position, Quaternion.identity) as GameObject;
        SpriteRenderer newRenderer =  effect.AddComponent<SpriteRenderer>();
        Rigidbody newRigidbody = effect.AddComponent<Rigidbody>();
        //effect.transform.localScale = Vector3.one*2;
        newRenderer.sprite = soundEffectSprites[Random.Range(0, soundEffectSprites.Count)];
        newRigidbody.useGravity = false;

        //Vector3 newForce =  Quaternion.Euler(0,0,45) * transform.up;
        Vector3 newForce = transform.up;

        newRigidbody.AddForce(newForce * 2, ForceMode.Impulse);
        float scaleModifier = 0;
        while (scaleModifier < 3)
        {
            scaleModifier += 0.55f;
            effect.transform.localScale = Vector3.one * scaleModifier;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);
        Destroy(effect);
        sRenderer.sprite = null;
        StopCoroutine("DisplaySoundEffects");
    }
}
