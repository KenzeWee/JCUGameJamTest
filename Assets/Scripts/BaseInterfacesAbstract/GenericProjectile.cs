using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericProjectile : MonoBehaviour, IProjectile {
    [SerializeField] private float fireForce = 30f;
    public float FireForce { get { return fireForce; } }

    [SerializeField] private float cooldown;
    public float Cooldown { get { return cooldown; } }

    [SerializeField] private float lifeTime = 5f;
    public float Lifetime { get { return lifeTime; } }

    [SerializeField] protected int damage = 1;
    [SerializeField] private AudioSO fireSound;
    public AudioSO FireSound { get { return fireSound; } }

    /*----------------Animations--------------*/
    [SerializeField] protected Animator impactAnimation;

    [Header ("Timer Cooldown Variables For PowerUps")]
    [SerializeField] private float duration = 5f;
    public float Duration { get { return duration; } }

    private float timeRemaining;

    public abstract void Fire ();
    protected abstract void collisionBehaviour (Collision2D col);

    protected virtual void Awake () {
        //set projectile to bullet layer
        //than ignore the gun layer 
        //so bullet doesnt blow up on gun
        gameObject.layer = 9;
        Physics2D.IgnoreLayerCollision (8, 9);
        Physics2D.IgnoreLayerCollision (9, 11);

        if (fireSound)
            fireSound = fireSound.Initialize (gameObject);
    }

    protected virtual void Update () {
        if (fireSound)
            fireSound.Update ();
    }

    protected virtual void OnCollisionEnter2D (Collision2D col) {
        collisionBehaviour (col);

        IDamagable hp = col.gameObject.GetComponent<IDamagable> ();
        if (hp != null) {
            hp.ChangeHealth (-damage);
        }
    }
}