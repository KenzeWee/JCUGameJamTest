using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLevel : MonoBehaviour {
	[SerializeField] private List<Transform> respawnPoints = new List<Transform> ();
	[SerializeField] private List<GameObject> levelObjects = new list<GameObject>();

	private virtual void Awake() {
		foreach (Transform child in transform) {
			levelObjects.Add(child.gameObject);
		}
	}	

	public List<GameObject> GetListOfLevelObjects () {
		return levelObjects;
	}

	public List<Transform> GetListOfRespawnPoints () {
		
		return respawnPoints;
		
	}
}