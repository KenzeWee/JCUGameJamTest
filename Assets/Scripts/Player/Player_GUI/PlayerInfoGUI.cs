using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoGUI : MonoBehaviour {
    public Image GUI_PlayerHealth;

    [SerializeField]
    private PlayerVariable m_player;

    private void Update()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        GUI_PlayerHealth.fillAmount = ((float)m_player.CurrentHP / 10f) ;
        print(((float)m_player.CurrentHP / 10f) );
    }
}
