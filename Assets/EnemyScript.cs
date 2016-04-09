using UnityEngine;
using System.Collections;

public class EnemyScript : Stats {
    float time;
    float updateTime;
    //NavMeshAgent navMeshAgent;
    bool dead;
    // Use this for initialization
    void Start () {
        updateTime = 10;
        time = 0;
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //navMeshAgent.SetDestination(Character.characterTransform.position);
        health = 1;
        dead = false;
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
                GetComponent<Rigidbody>().AddForce((Character.characterTransform.position - transform.position)/1000);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnDeath();
            }
        }
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
    }
    public void RecieveDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            OnDeath();
            Destroy(this, 5f);
        }
    }
}
