﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
   
    public float range;
    public float speed;
    private string opposition = "Enemy";
    public static Transform characterTransform;
    public GameObject attackRangeObject;
    public Collider attackRangeCollider;
    public int health = 100; 
    public AudioClip punch;
    public AudioClip landingClip;

    private AudioSource audio;
    private float startingPitch = 1;
    private float startringVolume = 0.7F;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
        audio = GetComponent<AudioSource>();
       
        characterTransform = transform;
	    attackRangeCollider = attackRangeObject.transform.GetComponent<Collider>();
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    Move();
        GetEnemyCollision();
	}

    public void Attack()
    {
        //attack sound
        float random = Random.Range(1F, 1.5F);
        float random2 = Random.Range(0.7F, 1F);
        audio.pitch = random;
        audio.volume = random2;
        audio.PlayOneShot(punch);
    }

    public int dmg = 5;
    private Vector3 colliderBoxPos;
    public Vector3 colliderBoxSize;
    public void GetEnemyCollision()
    {
        colliderBoxPos = transform.position + transform.forward* range;
        Collider[] hitColliders = Physics.OverlapBox(colliderBoxPos, colliderBoxSize/2);
        if (Input.GetButtonDown("Attack"))
        {
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.transform.tag == opposition)
                {
                    EnemyScript curEnemy = hitCollider.transform.GetComponent<EnemyScript>();
                    Debug.Log("Our hero is attacking");
                    Attack();
                    curEnemy.RecieveDamage(dmg);

                    //Check if enemy is alive
                    //if (!curEnemy.dead)
                    {
                        //if (enemies.Contains(curEnemy))
                        //{
                        //    enemies.Remove(curEnemy);
                        //}
                        //curTarget = null;
                        //return;
                    }
                    //if (!enemies.Contains(curEnemy))
                    //{
                    //    enemies.Add(curEnemy);
                    //    return;
                    //}
                }
            }
        } 
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;
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
            ReceiveDamage(1);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawCube(colliderBoxPos, colliderBoxSize);
    }
}