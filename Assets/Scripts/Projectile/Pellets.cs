using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellets : GenericProjectile {
    private Rigidbody2D rb;
    [SerializeField] private AudioSO explosionSound;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private bool drawDebugSphere = false;

    protected override void Awake () {
        base.Awake ();
        rb = GetComponent<Rigidbody2D> ();

        if (explosionSound)
            explosionSound = explosionSound.Initialize (gameObject);
    }

    protected override void Update () {
        base.Update ();
        if (explosionSound)
            explosionSound.Update ();
    }

    public override void Fire(GameObject playerWhoShot)
    {
        rb.AddForce (transform.right * FireForce, ForceMode2D.Impulse);
        Destroy (gameObject, Lifetime);
    }

    protected override void collisionBehaviour (Collision2D col) {
        gameObject.Explode (explosionForce, transform.position, explosionRadius);

        if (explosionSound)
            explosionSound.Play ();

        if (impactAnimation)
            impactAnimation.SetBool ("Impact", true);

        gameObject.GetComponent<Renderer> ().enabled = false;
        gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        Destroy (gameObject, 0.5f);
    }

    private void OnDrawGizmos () {
        if (drawDebugSphere)
            Gizmos.DrawWireSphere (transform.position, explosionRadius);
    }
}