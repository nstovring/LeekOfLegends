using UnityEngine;
using System.Collections;

public class EnemyScript : Stats {
    float time;
    float updateTime;
    //NavMeshAgent navMeshAgent;
    bool dead;
    // Use this for initialization
    public float t;
    private float startTime;
    private float journeyLength;
    public float offsetValue;

    void Start () {
        updateTime = 0.1f;
        time = 0;
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //navMeshAgent.SetDestination(Character.characterTransform.position);
        health = 1;
        dead = false;
        startTime = Time.time;
        journeyLength = Vector3.Distance(Character.characterTransform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            time += Time.deltaTime;
            if (time >= updateTime)
            {
                //navMeshAgent.SetDestination(Character.characterTransform.position);
                //GetComponent<Rigidbody>().AddForce(((Character.characterTransform.position - transform.position)/ Vector3.Distance(Character.characterTransform.position, transform.position)) * movementSpeed * Vector3.Distance(Character.characterTransform.position, transform.position));
                //GetComponent<Rigidbody>().AddForce((Character.characterTransform.position - transform.position) * movementSpeed);
                time = 0;
            }
            if(Vector3.Distance(Character.characterTransform.position, transform.position) > offsetValue)
            {
                StartNewJourney();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnDeath();
            }
        }
	}
    public void StartNewJourney()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = VectorSetHeight( Character.characterTransform.position - ((Character.characterTransform.position - transform.position) / Vector3.Distance(Character.characterTransform.position, transform.position))*offsetValue);
        float distCovered = (Time.time - startTime) * movementSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
    }
    public void OnDeath()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        //Destroy(navMeshAgent);
        GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().AddForce(new Vector3(15, 50));
        GetComponent<Rigidbody>().AddExplosionForce(50f, Character.characterTransform.position, 10);
        dead = true;
        Spawner.SpawnedEnemies.Remove(gameObject);
        Destroy(gameObject, 5F);
    }
    public void RecieveDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            OnDeath();
            
        }
    }
    Vector3 VectorSetHeight(Vector3 vector)
    {
        return new Vector3(vector.x, Spawner.spawnHeight, vector.z);
    }
}
