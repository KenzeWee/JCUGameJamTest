using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {    
    [SerializeField] private int m_health;

    private void Start()
    {
        GameManager.instance.AddPlayersToList(gameObject);
    }

    public void TakeDamage()
    {
        --m_health;

        if(m_health <= 0)
        {
            GameManager.instance.KnockOut(gameObject);
            gameObject.SetActive(false);
        }
    }
}
