using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericProjectile {
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionForce = 10f;

    [SerializeField] private bool drawDebugSphere = false;
    private Rigidbody2D projRB;

    protected override void Awake()
    {
        base.Awake();
        projRB = GetComponent<Rigidbody2D>();
    }

    public override void Fire()
    {
        projRB.AddForce(transform.right * FireForce, ForceMode2D.Impulse);
        Destroy(gameObject, Lifetime);
    }

    protected override void collisionBehaviour()
    {
        gameObject.Explode(explosionForce, transform.position, explosionRadius);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (drawDebugSphere)
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
