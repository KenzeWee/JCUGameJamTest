using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericProjectile {

    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionForce = 10f;

    [SerializeField] private bool drawDebugSphere = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Fire()
    {
        projRB.AddForce(transform.right * FireForce, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    protected override void collisionBehaviour()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (drawDebugSphere)
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
