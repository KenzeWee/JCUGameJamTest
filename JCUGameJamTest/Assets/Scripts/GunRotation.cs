using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour
{

    public float offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition) - transform.position;
        float rotationz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationz + offset);
    }
}
