using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPickUpType<T> : MonoBehaviour, IPickUp {
    //can be type of projectile to give
    //could be amount of health to return 
    //etc etc
    [Header ("Respawn Variables")]
    [SerializeField] protected bool respawnable = true;
    [SerializeField] private float cooldownTimer = 5f;
    protected float CoolDownTime { get { return cooldownTimer; } }
    protected float timeTillRespawn;
    protected bool isActive = true;

    [SerializeField] private T pickUpItem;
    public T PickUpItem { get { return pickUpItem; } }

    [SerializeField] protected GameObject visuals;
    protected Collider2D mcollider;

    //What this pickup does when its picked up
    //e.g. replenish health
    public abstract void PickUpBehaviour<TPlayerType> (GenericPlayer<TPlayerType> player) where TPlayerType : IInput;

    protected virtual void Awake() {
        mcollider = GetComponent<Collider2D>();
    }
    protected virtual void Respawn () {
        timeTillRespawn -= Time.deltaTime;
        if (timeTillRespawn <= 0) {
            visuals.SetActive (true);
            mcollider.enabled = true;
            isActive = true;
        }
    }

    protected virtual void Update () {
        if (respawnable && !isActive) {
            Respawn ();
        }
    }
}