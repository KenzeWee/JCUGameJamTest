using System.Collections;
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

    [SerializeField] private GameObject winScreen;
    public bool IsGameRunning { get; private set; }

    private void Awake()
    {
        Instance = this;
        winScreen.SetActive(false);
        IsGameRunning = true;
    }

    private void CheckWin()
    {
        if (ListOfPlayers.Count <= 1)
        {
            if (onGameEndEvent != null)
                onGameEndEvent();

            if (winScreen != null)
                winScreen.SetActive(true);

            print("win");
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

            CheckWin();
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
