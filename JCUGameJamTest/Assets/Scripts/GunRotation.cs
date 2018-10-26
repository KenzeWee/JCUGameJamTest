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

    private Camera mainCamera;
    private float rot_z;

    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private GameObject firingPoint;
    [SerializeField] private GameObject Bullet;
   
    private InputManager inputManager;
    private Vector2 mousePos;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        inputManager = player.GetComponent<InputManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Get mouse pos based on world space
        //global variable cause used throughout the script
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        MouseLookat();
        FollowMouse();
        if (inputManager.isMouseClicked)
        {
            fireBullet();
        }
    }

    private void MouseLookat()
    {
        //rotate based on pivot point (this script's gameobject)
        Vector2 diff = GetDifference();
        diff.Normalize();
        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    private void FollowMouse()
    {
        //Get Distance between mouse and player
        float distance = Vector2.Distance(mousePos, player.transform.position); 
        //Debug.Log(distance + " " + (distance > minDistance && distance < maxDistance));

        //if distance is within bounds
        //move gun towards mouse with the max distance move clamped
        if (distance > minDistance && distance < maxDistance)
        {
            gun.transform.position = Vector2.MoveTowards(gun.transform.position, mousePos, maxDistance-minDistance);
        }

        //Vector2 mousePos = Input.mousePosition;
        //Vector2 origin = player.transform.position;
        //float distanceBetween = Vector2.Distance(mousePos, origin);
        //Debug.Log(distanceBetween);
        //if (430 > distanceBetween && distanceBetween > 398)
        //{
        //    this.transform.position = mainCamera.ScreenToWorldPoint(mousePos);
        //}
    }

    private Vector2 GetDifference()
    {
        Vector2 difference = mousePos - (Vector2) transform.position;
        return difference;
    }

    void fireBullet()
    {
       GameObject spawnBullet = Instantiate(Bullet, firingPoint.transform.position, firingPoint.transform.rotation);
       spawnBullet.GetComponent<Rigidbody2D>().AddForce(spawnBullet.transform.forward * 1000f);
    }


}
