using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_TimerManager : MonoBehaviour {
    public Image TimerImage;

    private void Update()
    {
        UpdateTimerImage();
    }

    void UpdateTimerImage()
    {
        if (GameManager.Instance.roundTimer > 10)
        {
            TimerImage.color = Color.green;
        }
        else if (GameManager.Instance.roundTimer <= 10)
        {
            TimerImage.color = Color.red;
        }

        TimerImage.fillAmount = GameManager.Instance.roundTimer / 40;
    }
}
