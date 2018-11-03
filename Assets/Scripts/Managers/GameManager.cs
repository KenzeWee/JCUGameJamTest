using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private GenericLevel plane;
    [SerializeField] private float levelFightTime, PlaneArrivingTime, PlaneIdleTime, planeTravelTime;
    private float roundTimer;
    private int currentLevelID = 0;

    private GameState gameState = GameState.InLevel;
    public GameState GetGameState { get { return gameState; } }
    public enum GameState { InLevel, PlaneArriving, PlaneIdle, PlaneLeaving, ReachedDestination };
 [SerializeField] private List<GenericLevel> levels = new List<GenericLevel> ();
    public GenericLevel GetCurrentLevel { get { return levels[currentLevelID]; } }
    private void Awake () {
        Instance = this;
        IsGameRunning = true;
        roundTimer = levelFightTime;
        BGM = BGM.Initialize (gameObject);
        BGM.Play();
        //levels = levels.RandomizeList();
    }

    private void Update () {
        if (IsGameRunning) {
            roundTimer -= Time.deltaTime;
            BGM.Update();

            if (roundTimer <= 0) {
                if (currentLevelID == levels.Count) {
                    CheckWin ();
                    //Debug.Log ("Game End");
                } else if (gameState == GameState.InLevel && IsGameRunning) {
                    gameState = GameState.PlaneArriving;
                    ++currentLevelID;

                    if (currentLevelID == levels.Count) {
                        CheckWin ();
                    } else {
                        StartCoroutine (ExecutePlaneEvent ());
                    }
                }
            }
        }
    }

    private IEnumerator ExecutePlaneEvent () {
        // Spawn plane
        plane.transform.position = new Vector3 (-50, 5, 0);
        plane.gameObject.SetActive (true);
        float timer = PlaneArrivingTime;
        float distanceDelta = 50 / PlaneArrivingTime;
        do {
            timer -= Time.deltaTime;
            plane.transform.position += new Vector3 (distanceDelta * Time.deltaTime, 0, 0);
            yield return null;
        }
        while (plane.transform.position.x <= 0);

        // Plane is now stopping at level
        gameState = GameState.PlaneIdle;
        levels[currentLevelID - 1].StartCoroutine (levels[currentLevelID - 1].LevelBreak ());
        yield return new WaitForSeconds (PlaneIdleTime);
        // Plane is now moving to next level
        gameState = GameState.PlaneLeaving;

        timer = planeTravelTime / 2;

        // Move NEXT level to the plane location/center of screen
        distanceDelta = 100 / planeTravelTime;
        do {
            timer -= Time.deltaTime;
            levels[currentLevelID - 1].transform.position -= new Vector3 (distanceDelta * Time.deltaTime, 0, 0);
            yield return null;
        }
        while (timer >= 0);

        // Disable former level and enable next level
        levels[currentLevelID - 1].gameObject.SetActive (false);
        levels[currentLevelID].gameObject.SetActive (true);
        levels[currentLevelID].transform.position = new Vector3 (50, 0, 0);

        // Move remaining distance
        timer = planeTravelTime / 2;
        do {
            timer -= Time.deltaTime;
            levels[currentLevelID].transform.position -= new Vector3 (distanceDelta * Time.deltaTime, 0, 0);

            yield return null;
        }
        while (levels[currentLevelID].transform.position.x >= 0);

        // Reached the next level, plane is moving away
        gameState = GameState.ReachedDestination;

        distanceDelta = 50 / planeTravelTime;
        // Move plane out of the screen
        do {
            timer -= Time.deltaTime;
            plane.transform.position += new Vector3 (distanceDelta * Time.deltaTime, 0, 0);
            yield return null;
        }
        while (plane.transform.position.x < 50);
        plane.gameObject.SetActive (false);

        // At the next level
        gameState = GameState.InLevel;
        roundTimer = levelFightTime;
    }

    private void CheckWin () {
        if (onGameEndEvent != null)
            onGameEndEvent ();

        //show game end ui
        scoreBoard.GetComponent<GUI_Scoreboard> ().ShowScoreBoard ();

        //print("win");
        IsGameRunning = false;
    }

    public void KnockOut<T> (GenericPlayer<T> Player) where T : IInput {
        if (ListOfActivePlayers.Contains (Player)) {
            ListOfActivePlayers.Remove (Player);

            if (onListOfPlayerChangeEvent != null)
                onListOfPlayerChangeEvent ();

            // CheckWin();
        }
    }

    public void AddPlayersToList<T> (GenericPlayer<T> Player) where T : IInput {
        if (!ListOfPlayers.Contains (Player))
            ListOfPlayers.Add (Player);

        if (!ListOfActivePlayers.Contains (Player))
            ListOfActivePlayers.Add (Player);

        if (onListOfPlayerChangeEvent != null)
            onListOfPlayerChangeEvent ();
    }

    public List<Entity> GetListOfActivePlayers () {
        return ListOfActivePlayers;
    }

    public List<Entity> GetListOfAllPlayers () {
        return ListOfPlayers;
    }

    public void SpawnAllPlayer () {
        if (IsGameRunning) {
            for (int i = 0; i < 4; i++) {
                ListOfPlayers[i].gameObject.SetActive (true);
                ListOfPlayers[i].gameObject.transform.position = levels[currentLevelID - 1].GetListOfRespawnPoints () [i].position;
            }
        }
    }

    public void RunPlayerSpawnCoroutine (GameObject playerObj) {
        StartCoroutine (SpawnPlayerRandom (playerObj));
    }

    private IEnumerator SpawnPlayerRandom (GameObject playerObj) {
        //InLevel, PlaneArriving, PlaneIdle, PlaneLeaving, ReachedDestination 
        if (IsGameRunning) {
            yield return new WaitForSeconds (2);
            playerObj.SetActive (true);
            if (gameState == GameState.PlaneLeaving || gameState == GameState.PlaneIdle) {
                playerObj.transform.position = plane.GetListOfRespawnPoints () [Random.Range (0, plane.GetListOfRespawnPoints ().Count)].position;

            } else {
                if (currentLevelID == 0) {
                    playerObj.transform.position = levels[currentLevelID].GetListOfRespawnPoints () [Random.Range (0, levels[currentLevelID].GetListOfRespawnPoints ().Count)].position;
                } else {
                    playerObj.transform.position = levels[currentLevelID - 1].GetListOfRespawnPoints () [Random.Range (0, levels[currentLevelID - 1].GetListOfRespawnPoints ().Count)].position;
                }

            }
        }
    }
}