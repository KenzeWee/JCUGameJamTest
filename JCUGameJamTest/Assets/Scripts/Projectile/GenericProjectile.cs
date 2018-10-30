using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public abstract class GenericProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float fireForce = 30f;
    public float FireForce { get { return fireForce; } }

    protected Rigidbody2D projRB;

    public abstract void Fire();
    protected abstract void collisionBehaviour();

    protected virtual void Awake()
    {
        projRB = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        collisionBehaviour();
    }
}
