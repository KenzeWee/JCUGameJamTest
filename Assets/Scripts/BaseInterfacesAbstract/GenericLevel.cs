using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLevel : MonoBehaviour {
	[SerializeField] private List<Transform> respawnPoints = new List<Transform> ();
	[SerializeField] private GameObject levelBreakGroup;
    [SerializeField] private float lowestHeight = -15f, highestHeight = 9, fallSpeed = 10;
    private List<GameObject> levelObjects = new List<GameObject>();


	protected virtual void Awake() {
		if (!levelBreakGroup) throw new ArgumentNullException ("Need to assign level break group");
		
		foreach (Transform child in levelBreakGroup.transform) {
			levelObjects.Add(child.gameObject);
		}

        StartCoroutine(LevelBreak());
	}	

	public List<GameObject> GetListOfLevelObjects () {
		return levelObjects;
	}

	public List<Transform> GetListOfRespawnPoints () {
		
		return respawnPoints;
		
	}

	public virtual IEnumerator LevelBreak () {

        //add level break stuff here

        foreach (GameObject j in levelObjects)
        {
            if (!j.GetComponent<Rigidbody2D>())
            {
                levelObjects.Remove(j);
            }
        }

        for (float i = lowestHeight; lowestHeight <= highestHeight; i+= Time.deltaTime * fallSpeed)
        {
            for (int j = levelObjects.Count; j > 0; --j)
            {
                if (levelObjects[j - 1].transform.position.y <= i)
                {
                    levelObjects[j - 1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    levelObjects.Remove(levelObjects[j - 1]);
                }
            }

            yield return null;
        }
    }
}