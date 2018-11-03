using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
	[SerializeField] private float gravityPull = .78f;
	[SerializeField] private float killDistance = 0.5f;
	[SerializeField] private bool drawDebugCircle = false;
	private float gravityRadius = 1f;
	void Awake () {
		gravityRadius = GetComponent<CircleCollider2D> ().radius;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (Vector3.Distance (other.transform.position, transform.position) <= killDistance) {
			IDamagable HP = other.GetComponent<IDamagable> ();
			if (HP != null)
				HP.ChangeHealth (-10000);
		}

		if (other.attachedRigidbody) {
			float gravityIntensity = Vector3.Distance (transform.position, other.transform.position) / gravityRadius;
			other.attachedRigidbody.AddForce ((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * gravityPull * Time.smoothDeltaTime, ForceMode2D.Impulse);
			//Debug.DrawRay (other.transform.position, transform.position - other.transform.position);
		}
	}

	private void OnDrawGizmos () {
		if (drawDebugCircle)
			Gizmos.DrawWireSphere (transform.position, killDistance);
	}
}