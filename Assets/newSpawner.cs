using UnityEngine;
using System.Collections;
using System;

public class newSpawner : MonoBehaviour {
    private int nEnemies;
    private float interval;
    GameObject[] enemies;
    float spawnInterval;
    private int maxSpawnAmount;
    Spawner spawner;
    float spawnRange;
    float range;
    // Use this for initialization
    void Start () {
        spawner = Spawner.spawner;
        maxSpawnAmount = spawner.maxSpawnAmount;
        spawnInterval = spawner.spawnInterval;
        range = spawner.range;
        spawnRange = spawner.spawnRange;
	}
	
	// Update is called once per frame
	void Update () {
        maxSpawnAmount = spawner.maxSpawnAmount;
    }
    void CheckIfInRange()
    {

        if (Vector3.Distance(Character.characterTransform.position, transform.position) <= range)
        {
            StartSpawn(maxSpawnAmount, spawnInterval, false);
        }
    }

    private void StartSpawn(object maxSpawnAmount, object spawnInterval, bool v)
    {
        throw new NotImplementedException();
    }

    void StartSpawn(int wantedEnemies, float wantedInterval, bool check)
    {
        if (nEnemies < wantedEnemies && interval > wantedInterval)
        {
            int i = (int)UnityEngine.Random.Range(0, enemies.Length - 0.1f);
            Vector3 addedDist = checkIfWithinRange(transform.position);
            GameObject newEnemy = Instantiate(enemies[i], transform.position + addedDist, Quaternion.identity) as GameObject;
            newEnemy.GetComponent<EnemyScript>().Death += SoundEventHandler.soundEventHandler.whichMusic;
            Spawner.SpawnedEnemies.Add(newEnemy);
            newEnemy.GetComponent<Rigidbody>().AddForce(((Character.characterTransform.position - newEnemy.transform.position) / Vector3.Distance(Character.characterTransform.position, newEnemy.transform.position)) * newEnemy.GetComponent<EnemyScript>().movementSpeed);
            interval = 0;
            nEnemies++;
        }
        if (nEnemies == wantedEnemies)
        {
            Destroy(gameObject);
        }
    }
    Vector3 checkIfWithinRange(Vector3 position)
    {
        Vector3 addedDist = new Vector3(1.5f, 0, 0) + new Vector3(UnityEngine.Random.Range(0, 0.2f), 0, UnityEngine.Random.Range(-spawnRange, spawnRange));
        foreach (GameObject g in Spawner.SpawnedEnemies)
        {
            if (Vector3.Distance(g.transform.position, position + addedDist) < 0.1)
            {
                return checkIfWithinRange(position);
            }
        }
        return addedDist;
    }
}
