using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour {
    private Camera cam;

    //Find all players in scene (includes AI?)
    private List<GameObject> objectsToTrack;

    //Camera movement speed variables
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float zoomSpeed = 2f;

    [SerializeField] private Transform movementAxis;
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
    [SerializeField] private GameObject movingPlatform;
    private bool isStaticCamera = false;
    public bool SetStaticCamera { set { isStaticCamera = value; } }
    private bool TrackCloud = false;

    private void Start () {
        cam = GetComponent<Camera> ();

        targetPos = transform.position;

        orthRatio = minOrth / maxOrth;
        //distRatio = lowerBoundary / upperBoundary;

        GameManager.Instance.onListOfPlayerChangeEvent += UpdateListOfPlayers;
    }

    private void Update () {
        if (objectsToTrack == null)
            UpdateListOfPlayers ();

        switch (GameManager.Instance.GetGameState) {
            case GameManager.GameState.PlaneArriving: case GameManager.GameState.PlaneIdle: case GameManager.GameState.PlaneLeaving:
                TrackCloud = true;
                break;
            case GameManager.GameState.InLevel: case GameManager.GameState.ReachedDestination:
                TrackCloud = false;
                break;
        }
        
        CheckForMovingPlatform ();

        if (!isStaticCamera) {
            CalcPos ();
            CalcZoom ();
        } else {
            movementAxis.position = Vector3.Lerp (movementAxis.position, Vector3.zero.With (z: -10), panSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 18, zoomSpeed * Time.deltaTime);
        }
    }

    /*-------------------------- Camera Pan and Zoom Calculations ------------------------------*/
    void CalcPos () {
        Vector3 centre = Vector3.zero;
        float count = 0f;

        for (int i = 0; i < objectsToTrack.Count; i++) {
            centre += objectsToTrack[i].transform.position;
            count++;
        }

        if (count != 0) {
            targetPos = centre / count;
            targetPos = targetPos.With (z: -10);
            movementAxis.position = Vector3.Lerp (transform.position, targetPos, panSpeed * Time.deltaTime);
        }
    }

    void CalcZoom () {
        getMaxDistance ();

        if (largestDistance > upperBoundary) {
            orthSize = maxOrth;
        } else if (largestDistance < lowerBoundary) {
            orthSize = minOrth;
        } else {
            diff = largestDistance - lowerBoundary;
            orthDiff = diff * orthRatio;
            orthSize = minOrth + orthDiff + orthBuffer;
            if (orthSize > maxOrth) {
                orthSize = maxOrth;
            }
        }

        cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, orthSize, zoomSpeed * Time.deltaTime);
    }

    void getMaxDistance () {
        largestDistance = 0.0f;

        for (int i = 0; i < objectsToTrack.Count; i++) {
            for (int b = 0; b < objectsToTrack.Count; b++) {
                float dist = Vector2.Distance (objectsToTrack[b].transform.position, objectsToTrack[i].transform.position);

                if (dist > largestDistance) {
                    largestDistance = dist;
                }
            }
        }
    }

    void CheckForMovingPlatform () {
        if (objectsToTrack != null) {
            if (TrackCloud) {
                if (movingPlatform.activeSelf && !objectsToTrack.Contains (movingPlatform)) {
                    objectsToTrack.Add (movingPlatform);
                }

                if (!movingPlatform.activeSelf && objectsToTrack.Contains (movingPlatform)) {
                    objectsToTrack.Remove (movingPlatform);
                }
            } else if (objectsToTrack.Contains (movingPlatform)) {
                objectsToTrack.Remove (movingPlatform);
            }
        }
    }

    void UpdateListOfPlayers () {
        List<Entity> temp = GameManager.Instance.GetListOfActivePlayers ();

        if (objectsToTrack != null)
            objectsToTrack.Clear ();

        objectsToTrack = new List<GameObject> ();

        foreach (Entity player in temp) {
            objectsToTrack.Add (player.gameObject);
        }
    }
}