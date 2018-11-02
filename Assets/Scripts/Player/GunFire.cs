using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {
    [SerializeField] private Animator animator;
    private bool OnCooldown = false;

    [SerializeField] private Transform firingPoint;
    public Transform FiringPoint { get { return firingPoint; } }

    [SerializeField] private GenericProjectile projectile;
    private GenericProjectile defaultProjectile;
    public GenericProjectile SetProjectile {
        set 
        {
            projectile = value;
            timeRemaining = projectile.Duration;
        }
    }

    [SerializeField] private GameObject player;

    public delegate void OnGunFired (float force);
    public event OnGunFired onGunFiredEvent;

    private IInput inputManager;

    private float timeRemaining;

    // Use this for initialization
    void Start () {
        inputManager = player.GetComponent<IInput> ();
        defaultProjectile = projectile;
    }

    // Update is called once per frame
    void Update () {
        if (inputManager.IsFire && !OnCooldown) {
            FireProjectile ();
            StartCoroutine (FireCoolDown (projectile.Cooldown));
        }

        if (timeRemaining > 0 && projectile != defaultProjectile) {
            if (Timer()) {
                projectile = defaultProjectile;
            }
        }
    }

    void FireProjectile () {
        GenericProjectile projFired = Instantiate (projectile.gameObject, firingPoint.position, firingPoint.rotation).GetComponent<GenericProjectile> ();
        projFired.Fire ();
        projFired.FireSound.Play ();
        onGunFiredEvent (projFired.FireForce);
    }

    IEnumerator FireCoolDown (float cooldown) {
        OnCooldown = true;
        animator.SetBool ("IsFiring", true);
        yield return new WaitForSeconds (cooldown);
        OnCooldown = false;
        animator.SetBool ("IsFiring", false);
    }

    bool Timer () {
        timeRemaining -= Time.deltaTime;
        return (timeRemaining <= 0);
    }
}