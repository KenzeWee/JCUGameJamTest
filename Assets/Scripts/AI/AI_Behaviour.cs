using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Behaviour : MonoBehaviour, IInput {
    private enum AISTATE { IDLE, CHASING, ATTACK, DIE };
    [SerializeField] private AISTATE currenState = AISTATE.IDLE;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float engageRange = 4;
 
    [SerializeField]
    private float fireCooldown = 1;
    private float currentFireCooldown = 0;

    public bool IsFire { get; private set; }
    public Vector2 LookDirection { get; private set; }

	// Update is called once per frame
	void Update () {
        //Isfire

        //LookDirection 
        RunState();
        RunFireCD();
    }

    private void RunState()
    {
        switch (currenState) {
            case AISTATE.IDLE:

                break;

            case AISTATE.CHASING:
                MoveTo();
                Chase();
                break;

            case AISTATE.ATTACK:
                Attack();
                break;

            case AISTATE.DIE:

                break;
        }
    }

    private void RunFireCD()
    {
        //if curr fire cooldown is more than 0, cannot fire anything.
        if(currentFireCooldown > 0)
        {
            currentFireCooldown -= Time.deltaTime;
        }
    }

    void Aim()
    {
        Vector2 direction = transform.position + (target.position + Vector3.up * Vector3.Magnitude(transform.position - target.position) * 0.1f - transform.position).normalized;

        LookDirection = direction;
    }

    void MoveTo()
    {
        Vector2 direction = transform.position;

        if (target.position.x > transform.position.x)
        {
            direction += Vector2.down - Vector2.right;
        }else if(target.position.x < transform.position.x)
        {
            direction += Vector2.down + Vector2.right;
        }
        Fire();

        LookDirection = direction;
    }

    void Chase()
    {
        int layer = LayerMask.NameToLayer("GunColliderLayer");
        layer = ~layer;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (target.position - transform.position).normalized, target.position - transform.position,layer);

        if (checkEngageRange(hit.collider.transform))
        {
            currenState = AISTATE.ATTACK;
        }
    }

    private void Attack()
    {
        int layer = LayerMask.NameToLayer("GunColliderLayer");
        layer = ~layer;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (target.position - transform.position).normalized, target.position - transform.position, layer);

        if (checkEngageRange(hit.collider.transform))
        {
            Aim();
            Fire();
        }
        else
        {
            currenState = AISTATE.CHASING;
        }
    }

    bool checkEngageRange(Transform targetTrans) {
        if (targetTrans.GetComponent<Health>() && Vector3.Magnitude(transform.position - target.position) < engageRange)
        {
            return true;
        }
        return false;
    }
    
    private void Fire()
    {
        if(currentFireCooldown <= 0)
        {
            IsFire = true;
            currentFireCooldown = fireCooldown;
            StartCoroutine(ToggleFire());
        }
    }

    private IEnumerator ToggleFire()
    {
        yield return new WaitForSeconds(0);
        IsFire = false;
    }
}
