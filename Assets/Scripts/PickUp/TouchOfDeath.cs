using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchOfDeath : MonoBehaviour {
	[SerializeField] private float durationOfPowerUp = 5f;
	private float timer;

	private void Awake () {
		timer = durationOfPowerUp;
	}

	void FixedUpdate()
	{
		if (checkDuration()) {
			Destroy(this);
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.layer == 10) {
			other.gameObject.GetComponent<IDamagable>().ChangeHealth(-10000);
		}
	}


	bool checkDuration() {
		timer -= Time.deltaTime;
		return (timer <= 0);
	}
}
