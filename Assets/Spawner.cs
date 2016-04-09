using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public float range;
    public GameObject[] enemies;
    public static List<GameObject> SpawnedEnemies;
    float time;
    float interval;
    int nEnemies;
    public float spawnRange;
    public static float spawnHeight;
	// Use this for initialization
	void Start () {
        nEnemies = 0;
        spawnHeight = 0.5f;
        SpawnedEnemies = new List<GameObject>();
        range = 10;
        time = 0;
        interval = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        interval += Time.deltaTime;
        CheckIfInRange();
	}

    void CheckIfInRange()
    {
        if (Vector3.Distance(Character.characterTransform.position,transform.position) <= range)
        {
            StartSpawn(32, 2f,false);
        }
    }
    void StartSpawn(int wantedTime, float wantedInterval)
    {
        if (time < wantedTime && interval > wantedInterval)
        {
            int i = (int)Random.Range(0, enemies.Length - 0.1f);
            GameObject newEnemy = Instantiate(enemies[i], transform.position + ((Character.characterTransform.position - transform.position) / (Character.characterTransform.position - transform.position).magnitude)*2, Quaternion.identity) as GameObject;
            SpawnedEnemies.Add(newEnemy);
            interval = 0;
        }
        else if(time > wantedTime)
        {
            int i = (int)Random.Range(0, enemies.Length - 0.1f);
            GameObject newEnemy = Instantiate(enemies[i], transform.position + ((Character.characterTransform.position - transform.position) / (Character.characterTransform.position - transform.position).magnitude)*2, Quaternion.identity) as GameObject;
            SpawnedEnemies.Add(newEnemy);
            interval = 0;
            time = 0;
            moveSpawner(new Vector3(20, 0, 0));
        }
    }
    void StartSpawn(int wantedEnemies, float wantedInterval, bool check)
    {
        if (nEnemies < wantedEnemies && interval > wantedInterval)
        {
            int i = (int)Random.Range(0, enemies.Length - 0.1f);
            GameObject newEnemy = Instantiate(enemies[i], transform.position + new Vector3(-1.5f, 0, 0) + new Vector3(Random.Range(0, 0.2f),0, Random.Range(-spawnRange, spawnRange)), Quaternion.identity) as GameObject;
            SpawnedEnemies.Add(newEnemy);
            newEnemy.GetComponent<Rigidbody>().AddForce(((Character.characterTransform.position - newEnemy.transform.position)/Vector3.Distance(Character.characterTransform.position,newEnemy.transform.position))* newEnemy.GetComponent<EnemyScript>().movementSpeed);
            interval = 0;
            nEnemies++;
        }
        if (nEnemies == wantedEnemies)
        {
            interval = 0;
            nEnemies = 0;
            moveSpawner(new Vector3(20, 0, 0));
        }
    }
    void moveSpawner(Vector3 movement)
    {
        transform.position += movement;
    }
}
