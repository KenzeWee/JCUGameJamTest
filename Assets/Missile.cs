using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : GenericProjectile
{
    [SerializeField] private GameObject target;
    private Rigidbody2D rb;
    private bool GoUp = true;
    private float i = 0;
    [SerializeField] private float speed;
    [SerializeField] private float rotation;
    private IInput inputManager;

    // Use this for initialization
    void Start()
    {
        GoUp = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GoUp == true)
        {
            StartCoroutine(FloatUp());
        }
        if (GoUp == false)
        {
            Vector2 distance = (Vector2)target.transform.position - rb.position;
            distance.Normalize();
            float rotateAmount = Vector3.Cross(distance, transform.up).z;
            rb.angularVelocity = -rotateAmount * rotation;
            rb.velocity = transform.up * speed;
        }
    }

    public override void Fire(GameObject playerWhoShot)
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 30, LayerMask.NameToLayer("player"));
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
        Gizmos.DrawWireSphere(transform.position, 30);
    }

    IEnumerator FloatUp()
    {
        rb.velocity = transform.up * 5;
        yield return new WaitForSeconds(0.5f);
        GoUp = false;
    }

}
