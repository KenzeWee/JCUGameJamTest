using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class MarioFireball : GenericProjectile {
	private Rigidbody2D rb;
	private Vector2 velocity;

	[SerializeField] private int maxBounce = 5;
	private int bounceCounter = 0;
	[SerializeField] private float explosionRadius = 2f;
	[SerializeField] private float explosionForce = 10f;
	[SerializeField] private bool drawDebugSphere = false;
	protected override void Awake () {
		base.Awake ();

		rb = GetComponent<Rigidbody2D> ();
		velocity = transform.right * FireForce;
	}

	public override void Fire () {
		rb.velocity = velocity;
		Destroy (gameObject, Lifetime);
	}

	protected override void collisionBehaviour () {
		if (bounceCounter >= maxBounce) {
			gameObject.Explode (explosionForce, transform.position, explosionRadius);
			Destroy (gameObject);
		}
	}
	
	protected override void OnCollisionEnter2D (Collision2D other) {
		base.OnCollisionEnter2D (other);

		bounceCounter++;
		if (other.gameObject.layer == 10) {
			gameObject.Explode (explosionForce, transform.position, explosionRadius);
			Destroy (gameObject);
		}
	}

	void OnDestroy () {
		gameObject.Explode (explosionForce, transform.position, explosionRadius);
	}

	private void OnDrawGizmos () {
		if (drawDebugSphere)
			Gizmos.DrawWireSphere (transform.position, explosionRadius);
	}
}