using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : GenericProjectile {

    private GameObject target;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float rotation;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 distance = (Vector2)target.transform.position - rb.position;
        distance.Normalize();
        float rotateAmount = Vector3.Cross(distance, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotation;
        rb.velocity = transform.up * speed;
	}

    public override void Fire()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, Lifetime);
        return;
    }

    protected override void collisionBehaviour(Collision2D col)
    {
        Destroy(gameObject);
    }
}
