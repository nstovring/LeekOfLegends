﻿using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour
{
    public GameObject levelChunck;

	// Use this for initialization
	void Start ()
	{
	    lastSpawnPos = new Vector3(0,0,0);
	}

    public Vector3 lastSpawnPos;
    private Vector3 newSpawnPos;

	// Update is called once per frame
	void Update () {
	    if (Vector3.Distance(transform.position, lastSpawnPos) >= 25)
	    {
	        lastSpawnPos = transform.position;
            newSpawnPos = new Vector3(lastSpawnPos.x + 25f, 0, 0);
            Instantiate(levelChunck, newSpawnPos, Quaternion.identity);
	    }
	}
}
