﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    public delegate void OnPlayerKnockOut();
    public event OnPlayerKnockOut onPlayerKnockedOutEvent;

    public delegate void OnGameEnd();
    public event OnGameEnd onGameEndEvent;

    private List<Entity> ListOfPlayers = new List<Entity>();

    [SerializeField] private ParticleSystem fireworks;
    [SerializeField] private GameObject winScreen;
    public bool IsGameRunning { get; private set; }

    // LevelManager
    [SerializeField] private List<GameObject> gameLevels = new List<GameObject>();
    private float roundTimer = 30;
    private int currentLevelID = 0;

    private void Awake()
    {
        Instance = this;
        winScreen.SetActive(false);
        IsGameRunning = true;
    }

    private void Update()
    {
        if (!IsGameRunning)
        {
            roundTimer -= Time.deltaTime;
            if (roundTimer <= 0)
            {
                IsGameRunning = false;
                StartCoroutine(TransistionToNextLevel());
            }
        }
    }

    private IEnumerator TransistionToNextLevel()
    {
        // Disable former level and enable next level
        gameLevels[currentLevelID].SetActive(false);
        ++currentLevelID;
        gameLevels[currentLevelID].SetActive(true);

        // Transistion animation and stuff goes here
        yield return new WaitForSeconds(3);

        // Start the level again
        IsGameRunning = true;
        roundTimer = 30;
    }

    private void CheckWin()
    {
        if (ListOfPlayers.Count <= 1)
        {
            if (onGameEndEvent != null)
                onGameEndEvent();

            if (winScreen != null)
                winScreen.SetActive(true);

            if (ListOfPlayers.Count == 1)
            {
                fireworks.gameObject.transform.parent = ListOfPlayers[0].transform;
                fireworks.gameObject.transform.position = ListOfPlayers[0].transform.position;
                fireworks.Play();
            }
            //print("win");
            IsGameRunning = false;
        }
    }

    public void KnockOut<T> (GenericPlayer<T> Player) where T: IInput
    {
        if (ListOfPlayers.Contains(Player))
        {
            ListOfPlayers.Remove(Player);

            if (onPlayerKnockedOutEvent != null)
                onPlayerKnockedOutEvent();

            // CheckWin();
        }
    }

    public void AddPlayersToList<T> (GenericPlayer<T> Player) where T: IInput
    {
        if (!ListOfPlayers.Contains(Player))
            ListOfPlayers.Add(Player);
    }

    public List<Entity> GetListOfPlayers()
    {
        return ListOfPlayers;
    }
}
