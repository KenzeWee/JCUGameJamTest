using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Used to refer to any playable character 
    ie human controlled avatars or AI Controlled
    Determined by the type of IInput implementation
*/

[RequireComponent(typeof(IDamagable))]
public abstract class GenericPlayer<T> : Entity where T : IInput
{
    public IDamagable HP { get; private set; }
    public T inputManager { get; private set; }
    public GunFire GunFire { get; private set; }
    public GunRotation GunRotation { get; private set; }

    private Rigidbody2D rb;
    [SerializeField] private float knockbackFactor = 25f;

    protected virtual void Start()
    {
        HP = GetComponent<IDamagable>();
        inputManager = GetComponent<T>();

        GunFire = GetComponentInChildren<GunFire>();
        GunRotation = GetComponentInChildren<GunRotation>();

        rb = GetComponent<Rigidbody2D>();

        //Set layer to player
        gameObject.layer = 10;
        GameManager.Instance.AddPlayersToList(this);

        HP.onDieEvent += UnsuscribeToEvents;
        HP.onDieEvent += KnockOutPlayer;
        SuscribeToEvents();
    }

    protected override void SuscribeToEvents()
    {
        GunFire.onGunFiredEvent += Knockback;
    }

    protected override void UnsuscribeToEvents()
    {
        GunFire.onGunFiredEvent += Knockback;
        HP.onDieEvent -= KnockOutPlayer;
        HP.onDieEvent -= UnsuscribeToEvents;
    }

    private void KnockOutPlayer()
    {
        GameManager.Instance.KnockOut(this);
    }

    protected virtual void OnTriggerEnter2D (Collider2D other)
    {
        IPickUp pickUp = other.gameObject.GetComponent<IPickUp>();
        if (pickUp != null)
        {
            pickUp.PickUpBehaviour(this);
        }
    }

    protected virtual void Knockback (float fireForce)
    {
        rb.AddForce(-GunFire.FiringPoint.right * (fireForce) * knockbackFactor, ForceMode2D.Impulse);
    }
}
