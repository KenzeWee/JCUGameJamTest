using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable {
    [SerializeField] private int maxHP = 10;
    public int HP { get { return maxHP; } }
    [SerializeField] private HealthVariable hpContainer;

    public event OnDie onDieEvent;

    private void Start()
    {
        hpContainer.CurrentHP = HP;
    }

    public void ChangeHealth(int amount)
    {
        hpContainer.CurrentHP += amount;

        if(hpContainer.CurrentHP <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (transform.position.y < -15 || transform.position.y > 13)
        {
            Die();
        }
    }

    void Die()
    {
        if (onDieEvent != null)
            onDieEvent();
        gameObject.SetActive(false);
    }
}
