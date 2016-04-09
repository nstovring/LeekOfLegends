using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public GameObject levelChunck;

	// Use this for initialization
	void Start ()
	{
	    lastSpawnPos = transform.position;
	}

    private Vector3 lastSpawnPos;
    private Vector3 newSpawnPos;

	// Update is called once per frame
	void Update () {
	    if (Vector3.Distance(transform.position, lastSpawnPos) >= 25)
	    {
	        lastSpawnPos = transform.position;
	        Instantiate(levelChunck, lastSpawnPos, Quaternion.Identity);
	    }
	}
}
