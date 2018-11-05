using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] private AudioSO BGM;
    public delegate void OnListOfPlayerChange ();
    public event OnListOfPlayerChange onListOfPlayerChangeEvent;

    public delegate void OnGameEnd ();
    public event OnGameEnd onGameEndEvent;

    private List<Entity> ListOfPlayers = new List<Entity> ();
    private List<Entity> ListOfActivePlayers = new List<Entity> ();

    [SerializeField] private GameObject scoreBoard;
    public bool IsGameRunning { get; private set; }

    // LevelManager
    [SerializeField] private Plane plane;
    [SerializeField] private float levelFightTime = 40;
    private float roundTimer;
    public int currentLevelID { get; private set; }

    public GameState CurrentGameState { get; set; }
    public enum GameState { InLevel, PlaneArriving, PlaneIdle, PlaneLeaving, ReachedDestination };
 [SerializeField] private List<GenericLevel> levels = new List<GenericLevel> ();
 public GenericLevel GetCurrentLevel {
 get {
 if (!(currentLevelID >= levels.Count))
 return levels[currentLevelID];

 return levels[currentLevelID - 1];
        }
    }
    private void Awake () {
        Instance = this;
        IsGameRunning = true;
        roundTimer = levelFightTime;
        CurrentGameState = GameState.InLevel;
        currentLevelID = 0;

        BGM = BGM.Initialize (gameObject);
        BGM.Play ();
        //levels = levels.RandomizeList();
    }

    private void Update () {
        if (IsGameRunning) {
            roundTimer -= Time.deltaTime;
            BGM.Update ();
            if (roundTimer <= 0) {
                if (currentLevelID == levels.Count) {
                    CheckWin ();
                    //Debug.Log ("Game End");
                } else if (CurrentGameState == GameState.InLevel) {
                    CurrentGameState = GameState.PlaneArriving;

                    if (currentLevelID == levels.Count) {
                        CheckWin ();
                    } else {
                        plane.RunPlaneEvent ();
                    }
                }
            }
        }

        //Restart Level
        //Need more permenant solution
        if (Input.GetKeyDown (KeyCode.R)) {
            SceneManager.LoadScene (0);
        }
    }

    private void CheckWin () {
        if (onGameEndEvent != null)
            onGameEndEvent ();

        //show game end ui
        scoreBoard.GetComponent<GUI_Scoreboard> ().ShowScoreBoard ();

        //print("win");
        IsGameRunning = false;
        BGM.Stop ();
    }

    public bool IncrementLevelCounter () {
        currentLevelID++;
        if (currentLevelID >= levels.Count)
            return false;

        return true;
    }

    public void ResetLevelFightTime () {
        roundTimer = levelFightTime;
    }

    /*------------------------------------ Player Stuff ---------------------------*/
    public void AddPlayersToList<T> (GenericPlayer<T> Player) where T : IInput {
        if (!ListOfPlayers.Contains (Player))
            ListOfPlayers.Add (Player);

        if (!ListOfActivePlayers.Contains (Player))
            ListOfActivePlayers.Add (Player);

        if (onListOfPlayerChangeEvent != null)
            onListOfPlayerChangeEvent ();
    }

    public void KnockOut<T> (GenericPlayer<T> Player) where T : IInput {
        if (ListOfActivePlayers.Contains (Player)) {
            ListOfActivePlayers.Remove (Player);

            if (onListOfPlayerChangeEvent != null)
                onListOfPlayerChangeEvent ();
        }
    }

    public List<Entity> GetListOfActivePlayers () {
        return ListOfActivePlayers;
    }

    public List<Entity> GetListOfAllPlayers () {
        return ListOfPlayers;
    }

    public void RunPlayerSpawnCoroutine (GameObject playerObj) {
        StartCoroutine (SpawnPlayerRandom (playerObj));
    }

    private IEnumerator SpawnPlayerRandom (GameObject playerObj) {
        if (IsGameRunning) {
            yield return new WaitForSeconds (2);
            playerObj.SetActive (true);
            if (CurrentGameState != GameState.InLevel && CurrentGameState != GameState.ReachedDestination) {
                playerObj.transform.position = plane.GetListOfRespawnPoints () [Random.Range (0, plane.GetListOfRespawnPoints ().Count)].position;
            } else {
                playerObj.transform.position = levels[currentLevelID].GetListOfRespawnPoints () [Random.Range (0, levels[currentLevelID].GetListOfRespawnPoints ().Count)].position;
            }
        }
    }
}