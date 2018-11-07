using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOfDeathPickUp : GenericPickUpType<TouchOfDeath> {
	public override void PickUpBehaviour<TPlayerType> (GenericPlayer<TPlayerType> player) {
		if (isActive) {
			player.gameObject.AddComponent<TouchOfDeath> ();
			timeTillRespawn = CoolDownTime;
			visuals.SetActive (false);
			isActive = false;
		}
	}
}