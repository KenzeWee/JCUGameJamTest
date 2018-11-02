using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [SerializeField] private GameObject HelpScreen;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
