using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProjectile : GenericProjectile {
	private Rigidbody2D rb;
	public override void Fire () {
		rb = GetComponent<Rigidbody2D> ();
		rb.AddForce (transform.right * FireForce, ForceMode2D.Impulse);
	}

	protected override void collisionBehaviour (Collision2D col) {

	}

	protected override void Awake () {
		base.Awake ();

		gameObject.layer = 12;
		Physics2D.IgnoreLayerCollision(12, 13);
        Destroy(gameObject, 10f);
	}

	protected override void Update () {
		base.Update ();
		CheckIfAstroidOutOfScreen ();
	}

	void CheckIfAstroidOutOfScreen () {
		if (GameManager.Instance.IsGameRunning) {
			if (transform.position.y > GameManager.Instance.GetCurrentLevel.HighestHeight || transform.position.y < GameManager.Instance.GetCurrentLevel.LowestHeight || transform.position.x > GameManager.Instance.GetCurrentLevel.MaxmiumX || transform.position.x < GameManager.Instance.GetCurrentLevel.MinimumX) {
				Destroy (gameObject);
			}
		}
	}
}