using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoGUI : MonoBehaviour {
    public Image GUI_PlayerHealth;

    [SerializeField]
    private PlayerVariable m_player;

    [SerializeField]
    private TMPro.TextMeshProUGUI m_ScoreCountText;

    private void Update()
    {
        UpdateHealth();
        UpdateDeath();
    }

    void UpdateHealth()
    {
        GUI_PlayerHealth.fillAmount = ((float)m_player.CurrentHP / 10f) ;
    }

    void UpdateDeath()
    {
        m_ScoreCountText.text = "Death - " + m_player.CurrentScore;
    }
}
