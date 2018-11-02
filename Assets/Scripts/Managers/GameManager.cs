using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
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
    [SerializeField] private GameManager plane;
    [SerializeField] private float levelFightTime, PlaneArrivingTime, PlaneIdleTime, planeTravelTime;
    private float roundTimer;
    private int currentLevelID = 0;

    private GameState gameState = GameState.InLevel;
    enum GameState { InLevel, PlaneArriving, PlaneIdle, PlaneLeaving, ReachedDestination };
    [SerializeField] private List<GenericLevel> levels = new List<GenericLevel>();

    private void Awake()
    {
        Instance = this;
        winScreen.SetActive(false);
        IsGameRunning = true;
        roundTimer = levelFightTime;
        gameLevels = gameLevels.RandomizeList();
    }

    private void Update()
    {
        if (!IsGameRunning)
        {
            roundTimer -= Time.deltaTime;

            if (gameState == GameState.InLevel && roundTimer <= 0)
            {
                gameState = GameState.PlaneArriving;
                StartCoroutine(ExecutePlaneEvent());
            }
        }
    }

    private IEnumerator ExecutePlaneEvent()
    {
        // Spawn plane
        float timer = PlaneArrivingTime;

        // Move plane to level
        do
        {
            timer -= Time.deltaTime;

            yield return null;
        }
        while (timer <= 0);

        // Plane is now stopping at level
        gameState = GameState.PlaneIdle;
        yield return new WaitForSeconds(PlaneIdleTime);

        // Plane is now moving to next level
        gameState = GameState.PlaneLeaving;
        timer = planeTravelTime/2;

        // Move NEXT level to the plane location/center of screen
        do
        {
            timer -= Time.deltaTime;

            

            yield return null;
        }
        while (timer <= 0);

        // Disable former level and enable next level
        gameLevels[currentLevelID].SetActive(false);
        ++currentLevelID;
        gameLevels[currentLevelID].SetActive(true);

        // Move remaining distance
        timer = planeTravelTime / 2;
        do
        {
            timer -= Time.deltaTime;


            yield return null;
        }
        while (timer <= 0);

        // Reached the next level, plane is moving away
        gameState = GameState.ReachedDestination;

        // Move plane out of the screen
        do
        {
            timer -= Time.deltaTime;

            yield return null;
        }
        while (plane.transform.position.x < 25);

        // At the next level
        gameState = GameState.InLevel;
        roundTimer = levelFightTime;
    }

    private IEnumerator TransistionToNextLevel()
    {
        // Transistion animation and stuff goes here
        yield return new WaitForSeconds(3);

        // Disable former level and enable next level
        gameLevels[currentLevelID].SetActive(false);
        ++currentLevelID;
        gameLevels[currentLevelID].SetActive(true);

        // Start the level again
        IsGameRunning = true;
        roundTimer = 40;
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

    public void KnockOut<T>(GenericPlayer<T> Player) where T : IInput
    {
        if (ListOfPlayers.Contains(Player))
        {
            ListOfPlayers.Remove(Player);

            if (onPlayerKnockedOutEvent != null)
                onPlayerKnockedOutEvent();

            // CheckWin();
        }
    }

    public void AddPlayersToList<T>(GenericPlayer<T> Player) where T : IInput
    {
        if (!ListOfPlayers.Contains(Player))
            ListOfPlayers.Add(Player);
    }

    public List<Entity> GetListOfPlayers()
    {
        return ListOfPlayers;
    }
}
