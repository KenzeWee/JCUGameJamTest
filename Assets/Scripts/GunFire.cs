using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GenericProjectile projectile;
    [SerializeField] private float fireForce = 10f;

    [SerializeField] private GameObject player;
    private IInput inputManager;

    // Use this for initialization
    void Start () {
        inputManager = player.GetComponent<IInput>();
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
    }
}
