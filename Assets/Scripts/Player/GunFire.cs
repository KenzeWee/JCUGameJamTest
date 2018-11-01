using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {
    [SerializeField] private Transform firingPoint;

    [SerializeField] private GenericProjectile projectile;
    private GenericProjectile defaultProjectile;
    public GenericProjectile SetProjectile { set { projectile = value; } }

    [SerializeField] private GameObject player;

    public delegate void OnGunFired(float force);
    public event OnGunFired onGunFiredEvent;

    private IInput inputManager;

    // Use this for initialization
    void Start () {
        inputManager = player.GetComponent<IInput>();
        defaultProjectile = projectile;
	}
	
	// Update is called once per frame
	void Update () {
		if (inputManager.IsFire)
        {
            FireProjectile();
        }
	}

    void FireProjectile ()
    {
        GenericProjectile projFired = Instantiate(projectile.gameObject, firingPoint.position, firingPoint.rotation).GetComponent<GenericProjectile>();
        projFired.Fire();
        onGunFiredEvent(projFired.FireForce);
    }
}
