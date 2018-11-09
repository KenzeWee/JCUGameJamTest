using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullets : GenericProjectile {
    [SerializeField] private List<GenericProjectile> pellets = new List<GenericProjectile>();
    public override void Fire (GameObject playerWhoShot)
    {
        foreach (GenericProjectile pellet in pellets)
        {
            pellet.Fire(playerWhoShot);
        }
    }

    protected override void collisionBehaviour(Collision2D col)
    {
        //Pellets will handle, this is just the handler
    }
}
