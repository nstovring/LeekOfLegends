using UnityEngine;
using System.Collections;

public class EnemyScript : Stats {
    float time;
    float updateTime;
    //NavMeshAgent navMeshAgent;
    public bool dead;
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
                //ToPlayer();
                //StartNewJourney();
                GoToPlayer();
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
        Vector3 endPos = VectorSetHeight( Character.characterTransform.position- ((Character.characterTransform.position - transform.position) / Vector3.Distance(Character.characterTransform.position, transform.position))*offsetValue);
        journeyLength = Vector3.Distance(Character.characterTransform.position, transform.position);
        float distCovered = (Time.time - startTime) * movementSpeed * Time.deltaTime;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
    }
    public void ToPlayer()
    {
        //Vector3 Destination = VectorSetHeight((Character.characterTransform.position - transform.position) - ((Character.characterTransform.position - transform.position) / Vector3.Distance(Character.characterTransform.position, transform.position)) * offsetValue);
        Vector3 Destination = VectorSetHeight(Character.characterTransform.position - transform.position);
        GetComponent<Rigidbody>().MovePosition(transform.position +((Destination/Destination.magnitude)* Time.deltaTime* movementSpeed));
        //Debug.Log(((Character.characterTransform.position - transform.position) / Vector3.Distance(Character.characterTransform.position, transform.position)).magnitude);
    }
    public void OnDeath()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        //Destroy(navMeshAgent);
        GetComponent<Rigidbody>().useGravity = true;

        //GetComponent<Rigidbody>().AddForce(new Vector3(15, 50));
        GetComponent<Rigidbody>().AddExplosionForce(500f, Character.characterTransform.position, 10);
        dead = true;
        Spawner.SpawnedEnemies.Remove(gameObject);
        EventHandler.AddPoints(100);
        Destroy(gameObject, 5F);
    }
    public void RecieveDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            OnDeath();
        }
        StartCoroutine(FlickerColor());
    }

    IEnumerator FlickerColor()
    {
        int i = 0;
        SpriteRenderer sRenderer = GetComponentInChildren<SpriteRenderer>();
        while (i < 20)
        {
            float colorValue = Mathf.PingPong(Time.time, 255);
            sRenderer.color = new Color(colorValue, 0, 0);
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        sRenderer.color = Color.white;
    }

    Vector3 VectorSetHeight(Vector3 vector)
    {
        return new Vector3(vector.x, Spawner.spawnHeight, vector.z);
    }
    public void GoToPlayer()
    {
        Vector3 distVector = Character.characterTransform.position - transform.position;
        float angle;
        Quaternion rotation;
        if(distVector.x >= 0)
        {
            angle = Vector3.Angle(distVector, Vector3.right);
            if(distVector.z >= 0)
            {
                angle = -angle;
            }
            rotation = Quaternion.Euler(0, angle, 0);
            GetComponent<Rigidbody>().MovePosition(transform.position + rotation * Vector3.right * Time.deltaTime * movementSpeed);
        }
        else if (distVector.x < 0)
        {
            angle = Vector3.Angle(distVector, Vector3.left);
            if (distVector.z <= 0)
            {
                angle = -angle;
            }
            rotation = Quaternion.Euler(0, angle, 0);
            GetComponent<Rigidbody>().MovePosition(transform.position + rotation * Vector3.left * Time.deltaTime * movementSpeed);
        }
    }
}
