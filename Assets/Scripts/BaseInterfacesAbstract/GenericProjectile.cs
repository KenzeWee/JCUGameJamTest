using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public abstract class GenericProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float fireForce = 30f;
    public float FireForce { get { return fireForce; } }

    [SerializeField] protected int Damage = 1;

    protected Rigidbody2D projRB;

    public abstract void Fire();
    protected abstract void collisionBehaviour();

    protected virtual void Awake()
    {
        projRB = GetComponent<Rigidbody2D>();

        //set projectile to bullet layer
        //than ignore the gun layer 
        //so bullet doesnt blow up on gun
        gameObject.layer = 9;
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        collisionBehaviour();

        Health hp = col.gameObject.GetComponent<Health>();
        if (hp)
        {
            hp.TakeDamage(Damage);
        }
    }
}
