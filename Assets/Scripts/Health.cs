using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int m_health;

    public void TakeDamage()
    {
        --m_health;

        if(m_health <= 0)
        {
            //gameObject.SetActive(false);
        }
    }
}
