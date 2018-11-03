using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager instance;
    [SerializeField] private AudioSO mainMenuMusic;
    [SerializeField] private GameObject HelpScreen;

    public TextMeshProUGUI timer;

    private float timeLeft = 10;

    private bool isCountdown = false;

    private void Awake () {
        instance = this;
        mainMenuMusic = mainMenuMusic.Initialize (gameObject);
        mainMenuMusic.Play();
        //timer.gameObject.SetActive(false);
    }

    public void StartGame () {
        timer.gameObject.SetActive (true);
        mainMenuMusic.Stop();
        isCountdown = true;
    }

    private void Update () {
        mainMenuMusic.Update();
        if (isCountdown) {
            timeLeft -= Time.deltaTime;
            timer.text = "Starting in " + timeLeft.ToString ("f0");
            if (timeLeft <= 0) {
                SceneManager.LoadScene (1);
            }
        }
    }

    public void Help (bool On) {
        HelpScreen.SetActive (On);
    }

    public void Exit () {
        Application.Quit ();
    }
}