using UnityEngine;
using System.Collections;

public class EnemyScript : Stats {
    float time;
    float updateTime;
    NavMeshAgent navMeshAgent;
    bool dead;
    // Use this for initialization
    void Start () {
        updateTime = 1;
        time = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(HeroScript.hero.position);
        health = 10;
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
                navMeshAgent.SetDestination(HeroScript.hero.position);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                onDeath();
            }
        }
	}
    public void onDeath()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(navMeshAgent);
        GetComponent<Rigidbody>().useGravity = true;
        //GetComponent<Rigidbody>().AddForce(new Vector3(15, 50));
        GetComponent<Rigidbody>().AddExplosionForce(50f, HeroScript.hero.position, 10);
        dead = true;
    }
    public void recieveDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            onDeath();
            Destroy(this, 5f);
        }
    }
}
