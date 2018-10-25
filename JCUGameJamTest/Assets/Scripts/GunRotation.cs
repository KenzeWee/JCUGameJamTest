using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Camera mainCamera;
    private float rot_z;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        MouseLookat();
        FollowMouse();
    }

    private void MouseLookat()
    {
        Vector2 diff = GetDifference();
        diff.Normalize();
        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    private void FollowMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 origin = player.transform.position;
        float distanceBetween = Vector2.Distance(mousePos, origin);
        Debug.Log(distanceBetween);
        if (430 > distanceBetween && distanceBetween > 398)
        {
            this.transform.position = mainCamera.ScreenToWorldPoint(mousePos);
        }
    }

    private Vector2 GetDifference()
    {
        Vector2 difference = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        return difference;
    }


}
