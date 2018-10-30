using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    [SerializeField] private List<GameObject> allPlayers = new List<GameObject>();
    [SerializeField] private List<GameObject> KnockedOutPlayers = new List<GameObject>();

    public DynamicCamera cameraScript { private get; set; }

    private void Awake()
    {
        instance = this;
    }

    public void KnockOut(GameObject Player)
    {
        if (!KnockedOutPlayers.Contains(Player))
        {
            KnockedOutPlayers.Add(Player);
            cameraScript.RemovePlayerFromList(Player);
            CheckWin();
        }
    }

    private void CheckWin()
    {
        if(KnockedOutPlayers.Count >= allPlayers.Count - 1)
        {
            print("win");
        }
    }

    public void AddPlayersToList (GameObject Player)
    {
        if (!allPlayers.Contains(Player))
            allPlayers.Add(Player);
    }
}
