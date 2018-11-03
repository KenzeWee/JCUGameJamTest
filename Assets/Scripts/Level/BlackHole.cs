using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {
	[SerializeField] private float gravityPull = .78f;
	[SerializeField] private float killDistance = 12f;
	[SerializeField] private bool drawDebugCircle = false;
	private float gravityRadius = 1f;
	void Awake () {
		gravityRadius = GetComponent<CircleCollider2D> ().radius;
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.attachedRigidbody) {
			float gravityIntensity = Vector3.Distance (transform.position, other.transform.position) / gravityRadius;
			other.attachedRigidbody.AddForce ((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * gravityPull * Time.smoothDeltaTime, ForceMode2D.Impulse);
			CheckIfInBlackHoleVicinity (other);
		}
	}

	void CheckIfInBlackHoleVicinity (Collider2D other) {
		if (Vector3.Distance (transform.position, other.transform.position) <= killDistance) {
			IDamagable HP = other.GetComponent<IDamagable> ();
			
			if (HP != null)
				HP.ChangeHealth (-10000);
		}
	}

	private void OnDrawGizmos () {
		if (drawDebugCircle)
			Gizmos.DrawWireSphere (transform.position, killDistance);
	}
}