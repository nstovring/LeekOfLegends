using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
   
    public float range;
    public float speed;
    private string opposition = "Enemy";
    public static Transform characterTransform;
    public int health = 100; 
    public AudioClip punch;
    public AudioClip landingClip;
    public AudioClip deathClip;
    public Animator animator;

    private AudioSource audio;
    private float startingPitch = 1;
    private float startringVolume = 0.7F;
    private int animSwitch = 1;
    private Rigidbody rb;
    private bool alive = true;

    public Text healthText;

    private VisualEffects vEffects;

    public static Character instance;

	// Use this for initialization
	void Start ()
	{
        audio = GetComponent<AudioSource>();
	    animator = GetComponentInChildren<Animator>();
	    vEffects = GetComponent<VisualEffects>();
        characterTransform = transform;
	    rb = GetComponent<Rigidbody>();
	    instance = this;
	}


	
	// Update is called once per frame
	void Update () {

	    if (alive)
	    {
            Move();
            GetEnemyCollision();
        }
       

       

    }

    public void Attack()
    {
        //attack sound
        float random = Random.Range(1F, 1.5F);
        float random2 = Random.Range(0.3F, 0.5F);
        audio.pitch = random;
        audio.volume = random2;
        audio.PlayOneShot(punch);
        audio.volume = 1;
    }

    public int dmg = 5;
    private Vector3 colliderBoxPos;
    public Vector3 colliderBoxSize;
    public void GetEnemyCollision()
    {
        colliderBoxPos = transform.position + transform.forward* range;
        Collider[] hitColliders = Physics.OverlapBox(colliderBoxPos, colliderBoxSize/2);

        BasicAttack(hitColliders);
        HeavyAttack(hitColliders);
        //if (Input.GetButtonDown("Attack"))
        //{
        //    foreach (var hitCollider in hitColliders)
        //    {
        //        if (hitCollider.transform.tag == opposition)
        //        {
        //            EnemyScript curEnemy = hitCollider.transform.GetComponent<EnemyScript>();
        //            Debug.Log("Our hero is attacking");
        //            Attack();
        //            curEnemy.RecieveDamage(dmg);
        //        }
        //    }
        //} 
    }

    public void BasicAttack(Collider[] hitColliders)
    {
        if (Input.GetButtonDown("Attack"))
        {
            //animation switch
            animSwitch *= -1;
            Debug.Log(animSwitch);
            animator.SetInteger("animState", animSwitch);

            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.tag == opposition)
                {
                    EnemyScript curEnemy = hitCollider.transform.GetComponent<EnemyScript>();
                    vEffects.StartCoroutine("DisplaySoundEffectS");
                    Attack();
                    curEnemy.RecieveDamage(dmg);
                }
            }
        }
    }

    private float heavyAttackDelay = 0.25f;
    public int heavyAttackModifier = 2;

    public void HeavyAttack(Collider[] hitColliders)
    {
        if (Input.GetButtonDown("HeavyAttack"))
        {
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.tag == opposition)
                {
                    EnemyScript curEnemy = hitCollider.transform.GetComponent<EnemyScript>();
                    Debug.Log("Our hero is attacking");
                    Attack();
                    curEnemy.RecieveDamage(dmg* heavyAttackModifier);
                }
            }
        }
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        alive = !alive;
        animator.SetInteger("animState", 3);
        audio.PlayOneShot(deathClip);
        Time.fixedDeltaTime = 0.1F;
        rb.isKinematic = false;
        rb.AddForce(transform.up * 20, ForceMode.Impulse);
        rb.AddForce(transform.forward*-1*3,ForceMode.Impulse);
        
    }

    public void Move()
    {
        float hFloat = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position +  new Vector3(Input.GetAxis("Horizontal")*speed,0, Input.GetAxis("Vertical")*speed);

        rb.MovePosition(newPosition);
        if (hFloat> 0.1f)
        {
            Quaternion turnRight = Quaternion.Euler(new Vector3(0, 90, 0));
            transform.rotation = turnRight;
        }

        if (hFloat < -0.1f)
        {
            Quaternion turnLeft = Quaternion.Euler(new Vector3(0, -90, 0));
            transform.rotation = turnLeft;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            EnemyScript eScript = other.transform.GetComponent<EnemyScript>();
            if (!eScript.dead)
            {
                ReceiveDamage(eScript.dmg);
            }
        }
    }

    public void OnDrawGizmos()
    {
       // Gizmos.DrawCube(colliderBoxPos, colliderBoxSize);
    }
}
