using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager instance;

    [SerializeField] private GameObject HelpScreen;

    public TextMeshProUGUI timer;

    private float timeLeft = 10;

    private void Awake()
    {
        instance = this;
        timer.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        timer.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (timer.gameObject.activeInHierarchy)
        {
            timeLeft -= Time.deltaTime;
            timer.text = "Starting in " + timeLeft.ToString("f0");
            if(timeLeft <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void Help(bool On)
    {
        HelpScreen.SetActive(On);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
