using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : GenericProjectile
{
    [SerializeField] private GameObject target;
    private Rigidbody2D rb;
    private bool OnCooldown = false;

    [SerializeField] private float speed;
    [SerializeField] private float rotation;

    private IInput inputManager;

    // Use this for initialization
    void Start()
    {
       
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 distance = (Vector2)target.transform.position - rb.position;
        distance.Normalize();
        float rotateAmount = Vector3.Cross(distance, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotation;
        rb.velocity = transform.up * speed;
    }

    public override void Fire(GameObject playerWhoShot)
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20, LayerMask.NameToLayer("player"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (playerWhoShot != colliders[i].transform.gameObject && colliders[i].transform.gameObject.layer == 10)
                {
                    target = colliders[i].transform.gameObject;
                    break;
                }
                else
                {
                    target = null;
                }
            }
        }
        Destroy(gameObject, Lifetime);
        return;
    }

    protected override void collisionBehaviour(Collision2D col)
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 20);
    }

    IEnumerator FireCoolDown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
    }

}
