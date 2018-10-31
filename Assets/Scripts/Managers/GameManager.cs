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


    private void Awake()
    {
        Instance = this;
    }

    private void CheckWin()
    {
        if (ListOfPlayers.Count <= 1)
        {
            if (onGameEndEvent != null)
                onGameEndEvent();

            print("win");
        }
    }

    public void KnockOut(Entity Player)
    {
        if (ListOfPlayers.Contains(Player))
        {
            ListOfPlayers.Remove(Player);

            if (onPlayerKnockedOutEvent != null)
                onPlayerKnockedOutEvent();

            CheckWin();
        }
    }

    public void AddPlayersToList (Entity Player)
    {
        if (!ListOfPlayers.Contains(Player))
            ListOfPlayers.Add(Player);
    }

    public List<Entity> GetListOfPlayers()
    {
        return ListOfPlayers;
    }
}
