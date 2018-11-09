using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite defaultSprite;
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

    public delegate void OnGunFired (float force, Vector3 pos);
    public event OnGunFired onGunFiredEvent;

    private IInput inputManager;

    private float timeRemaining;

    // Use this for initialization
    void Start () {
        inputManager = player.GetComponent<IInput> ();
        defaultProjectile = projectile;
    }

    void OnEnable()
    {
        StopAllCoroutines();
        OnCooldown = false; 
    }

    void OnDisable()
    {
        animator.GetComponent<SpriteRenderer>().sprite = defaultSprite;
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
        projFired.Fire (gameObject.transform.parent.gameObject);

        if (projFired.FireSound)
            projFired.FireSound.Play ();

        onGunFiredEvent (projFired.FireForce, FiringPoint.position);
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