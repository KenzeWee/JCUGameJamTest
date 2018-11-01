using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullets : GenericProjectile {
    [SerializeField] private List<GenericProjectile> pellets = new List<GenericProjectile>();
    public override void Fire()
    {
        foreach (GenericProjectile pellet in pellets)
        {
            pellet.Fire();
        }
    }

    protected override void collisionBehaviour()
    {
        //Pellets will handle, this is just the handler
    }
}
