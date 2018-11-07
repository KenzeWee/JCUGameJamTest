using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable {
    [SerializeField] private int maxHP = 10;
    [SerializeField] private PlayerVariable hpContainer;
    public int HP { get { return hpContainer.CurrentHP; } private set { hpContainer.CurrentHP = value; } }

    public GameObject deathExplosionPrefab;

    public event OnDie onDieEvent;

    private void OnEnable () {
        HP = maxHP;
    }

    public void ChangeHealth (int amount) {
        HP += amount;
        
        if (HP > maxHP)
        {
            HP = maxHP;
        }

        if (HP <= 0) {
            Die ();
        }
    }

    void Die () {
        if (onDieEvent != null)
            onDieEvent ();
        gameObject.SetActive (false);

        GameObject explosion = Instantiate (deathExplosionPrefab,transform.position,Quaternion.identity);
        Destroy(explosion,0.5f);

        Respawn ();
    }

    public void Respawn () {
        GameManager.Instance.RunPlayerSpawnCoroutine (gameObject);
    }
}