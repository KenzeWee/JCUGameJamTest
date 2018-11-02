using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float fireForce = 30f;
    public float FireForce { get { return fireForce; } }

    [SerializeField] private float cooldown;
    public float Cooldown { get { return cooldown; } }

    [SerializeField] protected int damage = 1;

    /*----------------Animations--------------*/
    [SerializeField] protected Animator impactAnimation;

    public abstract void Fire();
    protected abstract void collisionBehaviour();

    protected virtual void Awake()
    {
        //set projectile to bullet layer
        //than ignore the gun layer 
        //so bullet doesnt blow up on gun
        gameObject.layer = 9;
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        collisionBehaviour();

        if (impactAnimation)
            impactAnimation.SetBool(0, true);

        Health hp = col.gameObject.GetComponent<Health>();
        if (hp)
        {
            hp.ChangeHealth(-damage);
        }
    }
}
