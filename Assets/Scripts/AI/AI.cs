using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAIInput))]
[RequireComponent(typeof(IDamagable))]
public class AI : Entity {
    protected IDamagable health;
    protected IAIInput inputManager;

    void Start()
    {
        health = GetComponent<IDamagable>();
        inputManager = GetComponent<IAIInput>();

        //Set layer to player
        gameObject.layer = 10;

        health.onDieEvent += UnsuscribeToEvents;
        SuscribeToEvents();
    }

    protected override void SuscribeToEvents()
    {
        GameManager.Instance.onGameEndEvent += inputManager.GameEnd;
        GameManager.Instance.onPlayerKnockedOutEvent += inputManager.PlayerKnockOutCheck;
    }

    protected override void UnsuscribeToEvents()
    {
        GameManager.Instance.onGameEndEvent -= inputManager.GameEnd;
        GameManager.Instance.onPlayerKnockedOutEvent -= inputManager.PlayerKnockOutCheck;
        health.onDieEvent -= UnsuscribeToEvents;
    }
}
