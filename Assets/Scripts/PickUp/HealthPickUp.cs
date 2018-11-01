using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : GenericPickUpType<int>
{
    public override void PickUpBehaviour<TPlayerType>(GenericPlayer<TPlayerType> player)
    {
        //Does something when picked up
        player.HP.ChangeHealth(PickUpItem);
        gameObject.SetActive(false);
    }
}
