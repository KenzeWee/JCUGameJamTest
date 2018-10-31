using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IDamagable))]
[RequireComponent(typeof(IInput))]
public class Player : Entity {
    protected IDamagable health;
    protected IInput inputManager;

    void Start()
    {
        health = GetComponent<IDamagable>();
        inputManager = GetComponent<IInput>();

        //Set layer to player
        gameObject.layer = 10;

        health.onDieEvent += UnsuscribeToEvents;
        SuscribeToEvents();
    }

    protected override void SuscribeToEvents ()
    {
        GameManager.Instance.onGameEndEvent += inputManager.GameEnd;
    }

    protected override void UnsuscribeToEvents()
    {
        GameManager.Instance.onGameEndEvent -= inputManager.GameEnd;
        health.onDieEvent -= UnsuscribeToEvents;
    }
}
