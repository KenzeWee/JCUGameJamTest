using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellets : GenericProjectile
{
    private Rigidbody2D rb;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private bool drawDebugSphere = false;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Fire()
    {
        rb.AddForce(transform.right * FireForce, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
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
