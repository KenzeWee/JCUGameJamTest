using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Used to refer to any playable character 
    ie human controlled avatars or AI Controlled
    Determined by the type of IInput implementation
*/

[RequireComponent (typeof (IDamagable))]
public abstract class GenericPlayer<T> : Entity where T : IInput {
    public IDamagable HealthScript { get; private set; }
    public T inputManager { get; private set; }
    public GunFire GunFire { get; private set; }
    public GunRotation GunRotation { get; private set; }

    [SerializeField] private PlayerVariable playerVariable;

    private Rigidbody2D rb;
    [SerializeField] private float knockbackFactor = 25f;

    /*----------Animations--------------*/
    [SerializeField] private Animator impactAnimation;

    protected virtual void Start () {
        //Set layer to player
        gameObject.layer = 10;
        Physics2D.IgnoreLayerCollision (8, 10);

        playerVariable.CurrentScore = 0;
    }

    void OnEnable () {
        if (HealthScript == null)
            HealthScript = GetComponent<IDamagable> ();

        if (inputManager == null)
            inputManager = GetComponent<T> ();

        if (GunFire == null)
            GunFire = GetComponentInChildren<GunFire> ();

        if (GunRotation == null)
            GunRotation = GetComponentInChildren<GunRotation> ();

        if (rb == null)
            rb = GetComponent<Rigidbody2D> ();

        if (GameManager.Instance == null) {
            FindObjectOfType<GameManager> ().AddPlayersToList (this);
        } else {
            GameManager.Instance.AddPlayersToList (this);
        }

        SuscribeToEvents ();
    }

    protected override void SuscribeToEvents () {
        GunFire.onGunFiredEvent += Knockback;
        HealthScript.onDieEvent += UnsuscribeToEvents;
        HealthScript.onDieEvent += KnockOutPlayer;
    }

    protected override void UnsuscribeToEvents () {
        GunFire.onGunFiredEvent -= Knockback;
        HealthScript.onDieEvent -= KnockOutPlayer;
        HealthScript.onDieEvent -= UnsuscribeToEvents;
    }

    private void KnockOutPlayer () {
        ++playerVariable.CurrentScore;
        GameManager.Instance.KnockOut (this);
    }

    protected virtual void OnTriggerEnter2D (Collider2D other) {
        IPickUp pickUp = other.gameObject.GetComponent<IPickUp> ();
        if (pickUp != null) {
            pickUp.PickUpBehaviour (this);
        }
    }

    protected virtual void OnCollisionEnter2D (Collision2D other) {
        //Debug.DrawRay (transform.position, Vector2.down, Color.green);
        Vector3 hitPoint = other.gameObject.transform.position;
        Collider2D col = other.contacts[0].otherCollider;
        if (Physics2D.Raycast (transform.position, Vector2.down, 0.5f)) {
            if (other.gameObject.tag == "platform" && col.gameObject == this.gameObject) {
                if (impactAnimation)
                    StartCoroutine (ImpactCoolDown (0.05f));
            }
        }
    }

    protected virtual void Knockback (float fireForce) {
        rb.AddForce (-GunFire.FiringPoint.right * (fireForce) * knockbackFactor, ForceMode2D.Impulse);
    }

    IEnumerator ImpactCoolDown (float cooldown) {
        impactAnimation.SetBool ("Impact", true);
        yield return new WaitForSeconds (cooldown);
        impactAnimation.SetBool ("Impact", false);
    }

    protected virtual void Update () {
        CheckPlayerPosition ();
    }

    void CheckPlayerPosition () {
        if (GameManager.Instance.IsGameRunning) {
            if (transform.position.y < GameManager.Instance.GetCurrentLevel.LowestHeight || transform.position.y > GameManager.Instance.GetCurrentLevel.HighestHeight || transform.position.x < GameManager.Instance.GetCurrentLevel.MinimumX || transform.position.x > GameManager.Instance.GetCurrentLevel.MaxmiumX) {
                if (GameManager.Instance.GetCurrentLevel.InfiniteScrolling) {
                    if (transform.position.y < GameManager.Instance.GetCurrentLevel.LowestHeight) {
                        transform.position = transform.position.With (y: GameManager.Instance.GetCurrentLevel.HighestHeight);
                    }

                    if (transform.position.y > GameManager.Instance.GetCurrentLevel.HighestHeight) {
                        transform.position = transform.position.With (y: GameManager.Instance.GetCurrentLevel.LowestHeight);
                    }

                    if (transform.position.x < GameManager.Instance.GetCurrentLevel.MinimumX) {
                        transform.position = transform.position.With (x: GameManager.Instance.GetCurrentLevel.MaxmiumX);
                    }

                    if (transform.position.x > GameManager.Instance.GetCurrentLevel.MaxmiumX) {
                        transform.position = transform.position.With (x: GameManager.Instance.GetCurrentLevel.MinimumX);
                    }
                } else {
                    HealthScript.ChangeHealth (-10000);
                }
            }
        }
    }
}