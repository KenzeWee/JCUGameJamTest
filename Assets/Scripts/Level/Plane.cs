using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : GenericLevel {
    [Header ("Plane Settings")]
    [SerializeField] private float planeArrivingTime = 3f;
    [SerializeField] private float planeIdleTime = 5f;
    [SerializeField] private float planeTravelTime = 5f;
    private GenericLevel levelToBreak;
    protected void Awake () {
        gameObject.layer = 13;

        Physics2D.IgnoreLayerCollision (13, 14);
    }

    public void RunPlaneEvent () {
        levelToBreak = GameManager.Instance.GetCurrentLevel;

        if (GameManager.Instance.IncrementLevelCounter ()) {
            transform.position = new Vector3 (-50, 5, 0);
            gameObject.SetActive (true);
            StartCoroutine (ExecutePlaneEvent ());
        }
    }

    private IEnumerator ExecutePlaneEvent () {
        GameManager.Instance.CurrentGameState = GameManager.GameState.PlaneArriving;

        // Spawn plane
        float timer = planeArrivingTime;
        float distanceDelta = 50 / planeArrivingTime;
        do {
            timer -= Time.deltaTime;
            transform.position += new Vector3 (distanceDelta * Time.deltaTime, 0, 0);
            yield return null;
        }
        while (transform.position.x <= 0);

        // Plane is now stopping at level
        GameManager.Instance.CurrentGameState = GameManager.GameState.PlaneIdle;
        levelToBreak.StartCoroutine (levelToBreak.LevelBreak ());
        yield return new WaitForSeconds (planeIdleTime);
        // Plane is now moving to next level
        GameManager.Instance.CurrentGameState = GameManager.GameState.PlaneLeaving;

        timer = planeTravelTime / 2;

        // Move NEXT level to the plane location/center of screen
        distanceDelta = 100 / planeTravelTime;
        do {
            timer -= Time.deltaTime;
            levelToBreak.transform.position -= new Vector3 (distanceDelta * Time.deltaTime, 0, 0);
            yield return null;
        }
        while (timer >= 0);

        // Disable former level and enable next level
        levelToBreak.gameObject.SetActive (false);
        GameManager.Instance.GetCurrentLevel.gameObject.SetActive (true);
        GameManager.Instance.GetCurrentLevel.transform.position = new Vector3 (50, 0, 0);

        // Move remaining distance
        timer = planeTravelTime / 2;
        do {
            timer -= Time.deltaTime;
            GameManager.Instance.GetCurrentLevel.transform.position -= new Vector3 (distanceDelta * Time.deltaTime, 0, 0);

            yield return null;
        }
        while (GameManager.Instance.GetCurrentLevel.transform.position.x >= 0);

        // Reached the next level, plane is moving away
        GameManager.Instance.CurrentGameState = GameManager.GameState.ReachedDestination;

        distanceDelta = 50 / planeTravelTime;
        // Move plane out of the screen
        do {
            timer -= Time.deltaTime;
            transform.position += new Vector3 (distanceDelta * Time.deltaTime, 0, 0);
            yield return null;
        }
        while (transform.position.x < 50);

        // At the next level
        GameManager.Instance.CurrentGameState = GameManager.GameState.InLevel;
        GameManager.Instance.ResetLevelFightTime ();

        gameObject.SetActive (false);

    }
}