using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLevel : MonoBehaviour {
	[SerializeField] private List<Transform> respawnPoints = new List<Transform> ();

	public List<Transform> GetListOfRespawnPoints () {
		
		return respawnPoints;
		
	}
}