using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private GameObject player;

    //Seperated gun's center to the middle (pivot point)
    //rotate around player center
    //move gun only towards mouse not the whole pivot
    [SerializeField] private GameObject gun;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private IInput inputManager;

    // Use this for initialization
    void Start()
    {
        inputManager = player.GetComponent<IInput>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Get mouse pos based on world space
        //global variable cause used throughout the script
        MouseLookat();
        FollowMouse();
    }

    private void MouseLookat()
    {
        //rotate based on pivot point (this script's gameobject)
        Vector2 diff = inputManager.LookDirection - (Vector2)transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    private void FollowMouse()
    {
        //Get Distance between mouse and player
        float distance = Vector2.Distance(inputManager.LookDirection, player.transform.position); 
        //Debug.Log(distance + " " + (distance > minDistance && distance < maxDistance));

        //if distance is within bounds
        //move gun towards mouse with the max distance move clamped
        if (distance > minDistance && distance < maxDistance)
        {
            gun.transform.position = Vector2.MoveTowards(gun.transform.position, inputManager.LookDirection, maxDistance-minDistance);
        }
    }
}
