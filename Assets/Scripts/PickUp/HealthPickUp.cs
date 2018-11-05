using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : GenericPickUpType<int> {
    public override void PickUpBehaviour<TPlayerType> (GenericPlayer<TPlayerType> player) {
        if (isActive) {
            //Does something when picked up
            player.HealthScript.ChangeHealth (PickUpItem);
            timeTillRespawn = CoolDownTime;
            visuals.SetActive (false);
            mcollider.enabled = false;
            isActive = false;
        }
    }
}