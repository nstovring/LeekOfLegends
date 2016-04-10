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
    public AudioClip[] punchLines;
    public AudioClip[] deathSounds;
    AudioSource audio;
    float destroyTimer;

    void Start () {
        destroyTimer = 0;
        updateTime = 0.1f;
        time = 0;
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //navMeshAgent.SetDestination(Character.characterTransform.position);
        health = 1;
        dead = false;
        startTime = Time.time;
        journeyLength = Vector3.Distance(Character.characterTransform.position, transform.position);
        audio = GetComponent<AudioSource>();
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
                if (time >= updateTime)
                {
                    GoToPlayer(true);
                    time = 0;
                }
                else GoToPlayer(false);
            }
            if(transform.position.y < Character.characterTransform.position.y - 0.1 || transform.position.y > Character.characterTransform.position.y + 0.1)
            {
                destroyTimer += Time.deltaTime;
                if(destroyTimer >= 5)
                {
                    OnDeath();
                }
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
        ChangeSprite(enemyState[2]);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        rb.AddForce(transform.up*150);
        rb.AddExplosionForce(500f, Character.characterTransform.position, 10);
        dead = true;
        Spawner.SpawnedEnemies.Remove(gameObject);
        EventHandler.AddPoints(100);
        Death(gameObject);
        ChooseSound();
        Destroy(gameObject, 5F);
    }
    public delegate void onDeath(GameObject sender);
    public event onDeath Death;
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
    public void GoToPlayer(bool moveOtherWay)
    {
        Vector3 distVector = Character.characterTransform.position - transform.position;
        float angle;
        Quaternion rotation;

        if (distVector.magnitude > 10)
        {
            ChangeSprite(enemyState[0]);
        }else if (distVector.magnitude < 10 && !dead)
        {
            ChangeSprite(enemyState[1]);
        }

        if (distVector.x >= 0)
        {
            angle = Vector3.Angle(distVector, Vector3.right);
            if (distVector.z >= 0)
            {
                angle = -angle;
            }
            rotation = Quaternion.Euler(0, angle, 0);
            GetComponent<Rigidbody>().MovePosition(transform.position + rotation * Vector3.right * Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
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

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void GoToPlayer(Vector3 position, bool moveOtherWay)
    {
        Vector3 distVector = position - transform.position;
        float angle;
        Quaternion rotation;
        if (distVector.x >= 0)
        {
            angle = Vector3.Angle(distVector, Vector3.right);
            if (distVector.z >= 0)
            {
                angle = -angle;
            }
            rotation = Quaternion.Euler(0, angle, 0);
            GetComponent<Rigidbody>().MovePosition(transform.position + rotation * Vector3.right * Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
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

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void ChooseSound()
    {
        if (deathSounds.Length > 0)
        {
            float procChance = 10;
            float procRoll = Random.Range(0, 100);
            if (procRoll < procChance)
            {
                int chooseSong = (int)Random.Range(0, deathSounds.Length - 0.1f);
                audio.PlayOneShot(deathSounds[chooseSong]);
            }
        }
    }
    public Sprite[] enemyState = new Sprite[3];
    public SpriteRenderer myRenderer;
    public void ChangeSprite(Sprite state)
    {
        myRenderer.sprite = state;
    }
}
