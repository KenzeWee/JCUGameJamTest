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

    protected virtual void Start()
    {
        HP = GetComponent<IDamagable>();
        inputManager = GetComponent<T>();

        GunFire = GetComponentInChildren<GunFire>();
        GunRotation = GetComponentInChildren<GunRotation>();

        //Set layer to player
        gameObject.layer = 10;

        HP.onDieEvent += UnsuscribeToEvents;
        SuscribeToEvents();
    }

    protected override void SuscribeToEvents()
    {   }

    protected override void UnsuscribeToEvents()
    {
        HP.onDieEvent -= UnsuscribeToEvents;
    }

    protected virtual void OnTriggerEnter2D (Collider2D other)
    {
        IPickUp pickUp = other.gameObject.GetComponent<IPickUp>();
        if (pickUp != null)
        {
            pickUp.PickUpBehaviour(this);
        }
    }
}
