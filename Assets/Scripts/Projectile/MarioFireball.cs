using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class MarioFireball : GenericProjectile {
	private Rigidbody2D rb;
	private Vector2 velocity;
	[SerializeField] private AudioSO explosionSound;
	[SerializeField] private int maxBounce = 5;
	private int bounceCounter = 0;
	[SerializeField] private float explosionRadius = 2f;
	[SerializeField] private float explosionForce = 10f;
	[SerializeField] private bool drawDebugSphere = false;

	protected override void Awake () {
		base.Awake ();

		rb = GetComponent<Rigidbody2D> ();
		velocity = transform.right * FireForce;

		transform.rotation = Quaternion.Euler(Vector3.zero);
		
		if (explosionSound)
			explosionSound = explosionSound.Initialize (gameObject);
	}

	protected override void Update () {
		if (bounceCounter >= maxBounce) {
			gameObject.Explode (explosionForce, transform.position, explosionRadius);

			if (explosionSound)
				explosionSound.Play ();

			if (impactAnimation)
				impactAnimation.SetBool ("Impact", true);

			gameObject.GetComponent<Renderer> ().enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			Destroy (gameObject, 0.5f);
		}
	}

	public override void Fire () {
		rb.velocity = velocity;
		Destroy (gameObject, Lifetime);
	}

	protected override void collisionBehaviour (Collision2D col) {
		bounceCounter++;
		if (col.gameObject.layer == 10) {
			gameObject.Explode (explosionForce, transform.position, explosionRadius);

			if (explosionSound)
				explosionSound.Play ();

			if (impactAnimation)
				impactAnimation.SetBool ("Impact", true);

			gameObject.GetComponent<Renderer> ().enabled = false;
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;

			Destroy (gameObject, 0.5f);
		}
	}

	private void OnDrawGizmos () {
		if (drawDebugSphere)
			Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}