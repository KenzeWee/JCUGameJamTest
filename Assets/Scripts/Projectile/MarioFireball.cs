using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MarioFireball : GenericProjectile {

	protected Rigidbody2D rb;

	protected override void Awake()
	{
		base.Awake();
		rb = GetComponent<Rigidbody2D>();	
	}

    public override void Fire()
    {
		rb.AddForce(transform.right * FireForce, ForceMode2D.Impulse);
    }

    protected override void collisionBehaviour()
    {
        
    }
}
