using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamagable {    
    [SerializeField] private int m_health;
    public int HP { get { return m_health; } }

    public event OnDie onDieEvent;

    private void Start()
    {
        GameManager.Instance.AddPlayersToList(gameObject.GetComponent<Entity>());
    }

    public void TakeDamage(int amount)
    {
        m_health -= amount;

        if(m_health <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (transform.position.y < -15)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.KnockOut(gameObject.GetComponent<Entity>());

        if (onDieEvent != null)
            onDieEvent();
        gameObject.SetActive(false);
    }
}
