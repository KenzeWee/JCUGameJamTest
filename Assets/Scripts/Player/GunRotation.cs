using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] private Transform pivotingObj;

    //Seperated gun's center to the middle (pivot point)
    //rotate around player center
    //move gun only towards mouse not the whole pivot
    [SerializeField] private Transform rotatingObj;

    [SerializeField] private GunGroundCheck m_GroundCheck;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private IInput inputManager;

    // Use this for initialization
    void Start()
    {
        inputManager = pivotingObj.GetComponent<IInput>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Get mouse pos based on world space
        //global variable cause used throughout the script
        MouseLookat();
        FollowMouse();

        // If RMB is down and gun is touching floor
        if (inputManager.ChangePivot && m_GroundCheck.isGrounded)
            SwitchPivot();
    }

    private Vector3 desiredPos;

    private void MouseLookat()
    {
        //rotate based on pivot point(this script's gameobject)
        Vector2 diff = inputManager.CursorPos - (Vector2)transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rotatingObj.parent.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    private void FollowMouse()
    {
        //Get Distance between mouse and player
        float distance = Vector2.Distance(inputManager.CursorPos, pivotingObj.position); 
        //Debug.Log(distance + " " + (distance > minDistance && distance < maxDistance));

        //if distance is within bounds
        //move gun towards mouse with the max distance move clamped
        if ((distance > minDistance && distance < maxDistance) || (distance > maxDistance && Vector2.Distance(pivotingObj.position, rotatingObj.position) < maxDistance))
        {
            rotatingObj.transform.position = Vector2.MoveTowards(rotatingObj.transform.position, inputManager.CursorPos, Time.deltaTime * 2);
        }
    }

    private void SwitchPivot()
    {
        Transform temp = pivotingObj;
        pivotingObj = rotatingObj;
        rotatingObj = temp;
    }
}
