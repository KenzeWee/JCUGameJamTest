using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable {    
    [SerializeField] private int m_health;
    public int HP { get { return m_health; } }

    public event OnDie onDieEvent;

    public void ChangeHealth(int amount)
    {
        m_health += amount;

        if(m_health <= 0)
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
