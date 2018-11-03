using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
	[SerializeField] public float gravityPull = .78f;
	private float gravityRadius = 1f;
	void Awake () {
		gravityRadius = GetComponent<CircleCollider2D>().radius;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.attachedRigidbody) {
			float gravityIntensity = Vector3.Distance (transform.position, other.transform.position) / gravityRadius;
			other.attachedRigidbody.AddForce ((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * gravityPull * Time.smoothDeltaTime, ForceMode2D.Impulse);
			//Debug.DrawRay (other.transform.position, transform.position - other.transform.position);
		}
	}
}