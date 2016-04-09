using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public GameObject levelChunck;
    public Sprite[] backGroundSprite= new Sprite[2];
    public List<Sprite> backgroundObjects = new List<Sprite>(11); 
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
	            int j = Random.Range(0, 2);
                chunk.GetComponentInChildren<SpriteRenderer>().sprite = backGroundSprite[j];
	            lastSpawnPos = chunk.transform.position;
	            if (backgroundObjects != null)
	            {
	                AddBackgroundObjects(lastSpawnPos);
	            }
	        }
	    }
	}

    void AddBackgroundObjects(Vector3 centerAreaVector3)
    {
        int amountOfObjects = Random.Range(1, 4);
        for (int i = 0; i < amountOfObjects; i++)
        {
            Debug.Log("Spawning Extra stuff");
            Vector3 objPosition = new Vector3(centerAreaVector3.x + Random.Range(-25, 25), 0, centerAreaVector3.z + Random.Range(-7, 7));
            Sprite sprite = backgroundObjects[Random.Range(0, 11)];
            GameObject clone = Instantiate(new GameObject("backGrounDObject"), objPosition, Quaternion.identity) as GameObject;
            clone.AddComponent<SpriteRenderer>().sprite = sprite;
            //clone.transform.parent = transform;
        }
    }
}
