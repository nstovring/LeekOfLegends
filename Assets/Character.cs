using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
   
    public float range;
    public float speed;
    public GameObject attackRangeObject;

    public Collider attackRangeCollider;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start ()
	{
	    attackRangeCollider = attackRangeObject.transform.GetComponent<Collider>();
	    rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    Move();
	}

    public void Attack()
    {
        
    }

    public void Move()
    {
        float hFloat = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position +  new Vector3(Input.GetAxis("Horizontal")*speed,0, Input.GetAxis("Vertical")*speed);
        //rigidbody.position + = newPosition;s

        rigidbody.MovePosition(newPosition);
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
