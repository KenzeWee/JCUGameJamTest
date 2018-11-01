using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePickUp : GenericPickUpType<GenericProjectile> {

    public override void PickUpBehaviour<TPlayerType>(GenericPlayer<TPlayerType> player)
    {
        //Does something when picked up
        player.GunFire.SetProjectile = PickUpItem;
        gameObject.SetActive(false);
    }
}
