using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLevel : MonoBehaviour {
    [SerializeField] private List<Transform> respawnPoints = new List<Transform> ();
    [SerializeField] private float lowestHeight = -15f, highestHeight = 9, fallSpeed = 10, minX = -20, maxX = 20;
    [SerializeField] private List<Rigidbody2D> levelObjects = new List<Rigidbody2D> ();
    [SerializeField] private bool infiniteScrolling = false;
    public bool InfiniteScrolling { get { return infiniteScrolling; } }
    public float LowestHeight { get { return lowestHeight; } }
    public float HighestHeight { get { return highestHeight; } }
    public float MinimumX { get { return minX; } }
    public float MaxmiumX { get { return maxX; } }

    public List<Rigidbody2D> GetListOfLevelObjects () {
        return levelObjects;
    }

    public List<Transform> GetListOfRespawnPoints () {
        return respawnPoints;
    }

    public virtual IEnumerator LevelBreak () {

        //add level break stuff here
        for (float i = lowestHeight; lowestHeight <= highestHeight; i += Time.deltaTime * fallSpeed) {
            for (int j = levelObjects.Count; j > 0; --j) {
                if (levelObjects[j - 1] != null) {
                    if (levelObjects[j - 1].transform.position.y <= i) {
                        levelObjects[j - 1].bodyType = RigidbodyType2D.Dynamic;
                        levelObjects.Remove (levelObjects[j - 1]);
                    }
                }
            }
            yield return null;
        }
    }
}