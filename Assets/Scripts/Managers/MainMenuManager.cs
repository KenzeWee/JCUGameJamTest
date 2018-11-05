using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public static MainMenuManager instance;
    [SerializeField] private AudioSO mainMenuMusic;
    [SerializeField] private AudioSO buttonPress;
    [SerializeField] private AudioSO beeping;
    [SerializeField] private GameObject HelpScreen;
    [SerializeField] private float timeLeft = 5;

    public TextMeshProUGUI timer;
    private bool isCountdown = false;

    private void Awake () {
        instance = this;
        mainMenuMusic = mainMenuMusic.Initialize (gameObject);
        mainMenuMusic.Play ();

        buttonPress = buttonPress.Initialize (gameObject);

        beeping = beeping.Initialize (gameObject);
        //timer.gameObject.SetActive(false);
    }

    public void StartGame () {
        timer.gameObject.SetActive (true);
        isCountdown = true;
    }

    private void Update () {
        mainMenuMusic.Update ();
        buttonPress.Update ();
        beeping.Update ();

        if (isCountdown) {
            timeLeft -= Time.deltaTime;
            timer.text = "Starting in " + timeLeft.ToString ("f0");

            if (timeLeft <= 0) {
                mainMenuMusic.Stop ();
                SceneManager.LoadScene (1);
            }
        }
    }

    public void Exit () {
        Application.Quit ();
    }

    public void PlayButtonPressSound () {
        buttonPress.Play ();
    }
}