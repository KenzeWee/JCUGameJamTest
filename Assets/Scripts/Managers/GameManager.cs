using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public List<GameObject> allPlayers = new List<GameObject>();
    public List<GameObject> KnockedOutPlayers = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void KnockOut(GameObject Player)
    {
        if (!KnockedOutPlayers.Contains(Player))
        {
            KnockedOutPlayers.Add(Player);
            CheckWin();
        }
    }

    public void CheckWin()
    {
        if(KnockedOutPlayers.Count >= allPlayers.Count - 1)
        {
            print("win");
        }
    }
}
