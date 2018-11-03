using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoGUI : MonoBehaviour {
    public Image GUI_PlayerHealth;

    [SerializeField]
    private GameObject m_playerObject;

    [SerializeField]
    private PlayerVariable m_player;

    [SerializeField]
    private TMPro.TextMeshProUGUI m_ScoreCountText;

    [SerializeField]
    private CanvasGroup CG;

    private void Start () {
        if (m_player.controlType == StaticFunctions.CONTROLLERTYPE.NULL) {
            gameObject.SetActive (false);
            m_playerObject.SetActive (false);
        } else {
            m_playerObject.SetActive (true);
        }

        CG = gameObject.AddComponent(typeof(CanvasGroup)) as CanvasGroup;
    }

    private void Update () {
        UpdateHealth ();
        UpdateDeath ();
    }

    void UpdateHealth () {
        GUI_PlayerHealth.fillAmount = ((float) m_player.CurrentHP / 10f);
        if (m_player.CurrentHP <= 0)
        {
            CG.alpha = 0.2f;
        }
        else
        {
            CG.alpha = 1f;
        }
    }

    void UpdateDeath () {
        m_ScoreCountText.text = "Death - " + m_player.CurrentScore;
    }
}