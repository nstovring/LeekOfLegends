using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    public float range;
    public GameObject[] enemies;
    public static List<GameObject> SpawnedEnemies;

    public int maxSpawnAmount;
    [Range(0.1f,2f)]
    public float spawnInterval;
    bool runOnce;
    float time;
    float interval;
    int nEnemies;
    public float spawnRange;
    public static float spawnHeight;
    public GameObject spawnerPrefab;
    GameObject otherSpawner;
    public static Spawner spawner;
    bool other;
	// Use this for initialization
	void Start () {
        //spawner = this;
        Debug.Log("hello");
        runOnce = false;
        nEnemies = 0;
        spawnHeight = 0.57f;
        SpawnedEnemies = new List<GameObject>();
        //range = 10;
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
            StartSpawn(maxSpawnAmount, spawnInterval, false);
            if (!runOnce && !other)
            {
                //Debug.Log("spawning other");
                //otherSpawner = Instantiate(spawnerPrefab, transform.position - new Vector3(20, 0, 0), Quaternion.identity) as GameObject;
                //otherSpawner.GetComponent<Spawner>().other = true;
                //otherSpawner.transform.parent = transform;
                runOnce = true;
            }
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
        }
    }
    void StartSpawn(int wantedEnemies, float wantedInterval, bool check)
    {
        if (nEnemies < wantedEnemies && interval > wantedInterval)
        {
            int i = (int)Random.Range(0, enemies.Length - 0.1f);
            int j = (int)Random.Range(0, enemies.Length - 0.1f);
            Vector3 addedDist = checkIfWithinRange(transform.position);
            GameObject newEnemy1 = Instantiate(enemies[i], transform.position + addedDist, Quaternion.identity) as GameObject;

            Vector3 oppositePosition = new Vector3(transform.position.x *-1,transform.position.y,transform.position.z);

            GameObject newEnemy2 = Instantiate(enemies[j], oppositePosition + addedDist, Quaternion.identity) as GameObject;

            EnemyScript eScript1 = newEnemy1.GetComponent<EnemyScript>();
            EnemyScript eScript2 = newEnemy2.GetComponent<EnemyScript>();
            eScript1.Death += SoundEventHandler.soundEventHandler.whichMusic;
            eScript2.Death += SoundEventHandler.soundEventHandler.whichMusic;
            eScript1.dmg = enemyDamage;
            eScript2.dmg = enemyDamage;
            SpawnedEnemies.Add(newEnemy1);
            SpawnedEnemies.Add(newEnemy2);
            newEnemy1.GetComponent<Rigidbody>().AddForce(((Character.characterTransform.position - newEnemy1.transform.position)/Vector3.Distance(Character.characterTransform.position,newEnemy1.transform.position))* newEnemy1.GetComponent<EnemyScript>().movementSpeed);
            newEnemy2.GetComponent<Rigidbody>().AddForce(((Character.characterTransform.position - newEnemy2.transform.position) / Vector3.Distance(Character.characterTransform.position, newEnemy2.transform.position)) * newEnemy2.GetComponent<EnemyScript>().movementSpeed);

            interval = 0;
            nEnemies++; //Maybe say += 2?
        }
        if (nEnemies == wantedEnemies)
        {
            interval = 0;
            nEnemies = 0;
            if (!other)
            {
                moveSpawner(new Vector3(25, 0, 0));
            }
            else Destroy(gameObject);
        }
       // maxSpawnAmount += 2;
       // enemyDamage *= 2;
    }

    public int enemyDamage = 1;

    void moveSpawner(Vector3 movement)
    {
        transform.position += movement;
        runOnce = false;
        maxSpawnAmount += 2;
        enemyDamage *= 2;
    }
    Vector3 checkIfWithinRange(Vector3 position)
    {
        Vector3 addedDist = new Vector3(-1.5f, 0, 0) + new Vector3(Random.Range(0, 0.2f), 0, Random.Range(-spawnRange, spawnRange));
        foreach (GameObject g in SpawnedEnemies)
        {
            if(Vector3.Distance(g.transform.position, position + addedDist) < 0.1)
            {
                return checkIfWithinRange(position);
            }
        }
        return addedDist;
    }
}
