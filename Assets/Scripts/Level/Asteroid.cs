using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : GenericLevel {
	//Add functionality for this level here
	[SerializeField] private Transform blackHole;
	[SerializeField] private List<Transform> asteroidSpawnAreas;
	[SerializeField] private List<GenericProjectile> asteroidsToSpawn;
	private float cooldownTimer = 0f;

	void Start () {
		List<Entity> players = GameManager.Instance.GetListOfAllPlayers ();
		foreach (Entity player in players) {
			player.gameObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		}

		FindObjectOfType<DynamicCamera> ().SetStaticCamera = true;
		StartNewTimer ();
	}

	void FixedUpdate () {
		if (Timer () && GameManager.Instance.IsGameRunning) {
			SpawnAsteroid ();
			StartNewTimer ();
		}
	}

	void SpawnAsteroid () {
		Transform randomSpawnArea = asteroidSpawnAreas.RandomObject ();
		GameObject asteroidSpawn = asteroidsToSpawn.RandomObject ().gameObject;

		GenericProjectile spawnedAsteroid = Instantiate (asteroidSpawn, randomSpawnArea.position, randomSpawnArea.rotation).GetComponent<GenericProjectile> ();
		spawnedAsteroid.Fire ();
	}

	void StartNewTimer () {
		cooldownTimer = Random.Range (0.5f, 2f);
	}

	bool Timer () {
		cooldownTimer -= Time.deltaTime;
		return cooldownTimer <= 0;
	}
}