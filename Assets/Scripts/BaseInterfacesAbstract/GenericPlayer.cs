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
    protected IDamagable HP;
    protected T inputManager { get; set; }

    protected virtual void Start()
    {
        HP = GetComponent<IDamagable>();
        inputManager = GetComponent<T>();

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
}
