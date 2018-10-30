using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour {
    private Camera cam;

    //Find all players in scene (includes AI?)
    private List<GameObject> players;

    //Camera movement speed variables
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float zoomSpeed = 2f;

    private Vector3 targetPos;

    //prevent camera from moving/zooming in and out too much 
    [SerializeField] private float upperBoundary = 30.0f;
    [SerializeField] private float lowerBoundary = 5.0f;

    [SerializeField] private float maxOrth = 12;
    [SerializeField] private float minOrth = 6;

    [SerializeField] private float orthBuffer = 0.0f;

    private float largestDistance;

    private float orthRatio;
    //private float distRatio;

    private float orthSize;
    private float diff;
    private float orthDiff;

    private void Start()
    {
        cam = GetComponent<Camera>();
        players = StaticFunctions.FindGameObjectsByLayer(10);

        targetPos = transform.position;

        orthRatio = minOrth / maxOrth;
        //distRatio = lowerBoundary / upperBoundary;

        GameManager.instance.cameraScript = this;
    }

    private void Update()
    {
        CalcPos();
        CalcZoom();
    }

    /*-------------------------- Camera Pan and Zoom Calculations ------------------------------*/
    void CalcPos()
    {
        Vector3 centre = Vector3.zero;
        float count = 0f;

        for (int i = 0; i < players.Count; i++)
        {
            centre += players[i].transform.position;
            count++;
        }

        targetPos = centre / count;
        targetPos = targetPos.With(z: -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, panSpeed * Time.deltaTime);
    }

    void CalcZoom()
    {
        getMaxDistance();

        if (largestDistance > upperBoundary)
        {
            orthSize = maxOrth;
        }
        else if (largestDistance < lowerBoundary)
        {
            orthSize = minOrth;
        }
        else
        {
            diff = largestDistance - lowerBoundary;
            orthDiff = diff * orthRatio;
            orthSize = minOrth + orthDiff + orthBuffer;
        }

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, orthSize, zoomSpeed * Time.deltaTime);
    }

    void getMaxDistance()
    {
        largestDistance = 0.0f;

        for (int i = 0; i < players.Count; i++)
        {
            for (int b = 0; b < players.Count; b++)
            {
                float dist = Vector2.Distance(players[b].transform.position, players[i].transform.position);

                if (dist > largestDistance)
                {
                    largestDistance = dist;
                }
            }
        }
    }

    /* ------------------------- public functions ---------------------------*/
    public void RemovePlayerFromList (GameObject playerToRemove)
    {
        if (players.Contains(playerToRemove))
            players.Remove(playerToRemove);
    }

    public void AddPlayerToList (GameObject playerToAdd)
    {
        if (!players.Contains(playerToAdd))
            players.Add(playerToAdd);
    }
}
