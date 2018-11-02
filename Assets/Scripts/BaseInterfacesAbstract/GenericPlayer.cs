using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Used to refer to any playable character 
    ie human controlled avatars or AI Controlled
    Determined by the type of IInput implementation
*/

[RequireComponent(typeof(IDamagable))]
public abstract class GenericPlayer<T> : Entity where T : IInput
{
    public IDamagable HealthScript { get; private set; }
    public T inputManager { get; private set; }
    public GunFire GunFire { get; private set; }
    public GunRotation GunRotation { get; private set; }
    [SerializeField] private PlayerVariable playerVariable;

    private Rigidbody2D rb;
    [SerializeField] private float knockbackFactor = 25f;

    /*----------Animations--------------*/
    [SerializeField] private Animator impactAnimation;

    protected virtual void Start()
    {
        HealthScript = GetComponent<IDamagable>();
        inputManager = GetComponent<T>();

        GunFire = GetComponentInChildren<GunFire>();
        GunRotation = GetComponentInChildren<GunRotation>();

        rb = GetComponent<Rigidbody2D>();

        //Set layer to player
        gameObject.layer = 10;
        Physics2D.IgnoreLayerCollision(8, 10);
        GameManager.Instance.AddPlayersToList(this);

        HealthScript.onDieEvent += UnsuscribeToEvents;
        HealthScript.onDieEvent += KnockOutPlayer;
        SuscribeToEvents();
    }

    protected override void SuscribeToEvents()
    {
        GunFire.onGunFiredEvent += Knockback;
    }

    protected override void UnsuscribeToEvents()
    {
        GunFire.onGunFiredEvent += Knockback;
        HealthScript.onDieEvent -= KnockOutPlayer;
        HealthScript.onDieEvent -= UnsuscribeToEvents;
    }

    private void KnockOutPlayer()
    {
        ++playerVariable.CurrentScore;
        GameManager.Instance.KnockOut(this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        IPickUp pickUp = other.gameObject.GetComponent<IPickUp>();
        if (pickUp != null)
        {
            pickUp.PickUpBehaviour(this);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        Vector3 hitPoint = other.gameObject.transform.position;
        Collider2D col = other.contacts[0].otherCollider;
        if (hit.collider)
        {
            if (other.gameObject.tag == "platform" && col.gameObject == this.gameObject)
            {
                Debug.Log(other.gameObject.name);
                StartCoroutine(ImpactCoolDown(0.05f));
            }
        }
    }

    protected virtual void Knockback(float fireForce)
    {
        rb.AddForce(-GunFire.FiringPoint.right * (fireForce) * knockbackFactor, ForceMode2D.Impulse);
    }

    IEnumerator ImpactCoolDown(float cooldown)
    {
        impactAnimation.SetBool("Impact", true);
        yield return new WaitForSeconds(cooldown);
        impactAnimation.SetBool("Impact", false);
    }
}
