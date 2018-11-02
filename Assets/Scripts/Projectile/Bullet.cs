using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericProjectile {
    [SerializeField] private AudioSO explosionSound;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private bool drawDebugSphere = false;
    private CameraShake shakeCam;
    private Rigidbody2D projRB;

    protected override void Awake () {
        base.Awake ();
        projRB = GetComponent<Rigidbody2D> ();
        shakeCam = FindObjectOfType<CameraShake> ();

        if (explosionSound)
            explosionSound = explosionSound.Initialize (gameObject);
    }

    protected override void Update () {
        base.Update ();

        if (explosionSound)
            explosionSound.Update ();
    }

    public override void Fire () {
        projRB.AddForce (transform.right * FireForce, ForceMode2D.Impulse);
        Destroy (gameObject, Lifetime);
    }

    protected override void collisionBehaviour (Collision2D col) {
        gameObject.Explode (explosionForce, transform.position, explosionRadius);
        shakeCam.Shake(10);

        if (explosionSound)
            explosionSound.Play ();

        gameObject.GetComponent<Renderer> ().enabled = false;
        gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
        Destroy (gameObject, 0.4f);
    }

    private void OnDrawGizmos () {
        if (drawDebugSphere)
            Gizmos.DrawWireSphere (transform.position, explosionRadius);
    }
}