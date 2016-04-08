using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
   
    public float range;
    public float speed;
    public GameObject attackRangeObject;

    public Collider attackRangeCollider;
    private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
	    attackRangeCollider = attackRangeObject.transform.GetComponent<Collider>();
	    rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    Move();
	}

    public void Attack()
    {
        
    }


    public void GetEnemyCollision()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(0.7f, 1, 0.7f));
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == opposition)
            {
                Stats curEnemy = hitCollider.transform.GetComponent<Stats>();
                //Check if enemy is alive
                if (!curEnemy.isAlive)
                {
                    if (enemies.Contains(curEnemy))
                    {
                        enemies.Remove(curEnemy);
                    }
                    //curTarget = null;
                    return;
                }
                if (!enemies.Contains(curEnemy))
                {
                    enemies.Add(curEnemy);
                    return;
                }
            }
        }
    }

    public void Move()
    {
        float hFloat = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position +  new Vector3(Input.GetAxis("Horizontal")*speed,0, Input.GetAxis("Vertical")*speed);
        //rigidbody.position + = newPosition;s

        rb.MovePosition(newPosition);
        //transform.position = newPosition;
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
}
