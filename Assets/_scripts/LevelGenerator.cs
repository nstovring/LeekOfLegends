using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public GameObject levelChunck;
    public Sprite[] backGroundSprite= new Sprite[2];
	// Use this for initialization
	void Start ()
	{
	    lastSpawnPos = new Vector3(0,0,0);
	}

    public Vector3 lastSpawnPos;
    private Vector3 newSpawnPos;

	// Update is called once per frame
	void Update () {
	    if (transform.position.x - lastSpawnPos.x >= 25)
	    {
            newSpawnPos = new Vector3(lastSpawnPos.x + 25f, 0, 0);
            GameObject chunk = Instantiate(levelChunck, newSpawnPos, Quaternion.identity) as GameObject;
	        if (chunk)
	        {
	            int j = (int) Random.Range(0, 2);

                chunk.GetComponentInChildren<SpriteRenderer>().sprite = backGroundSprite[j];
	            lastSpawnPos = chunk.transform.position;
	        }
	    }
	}
}
