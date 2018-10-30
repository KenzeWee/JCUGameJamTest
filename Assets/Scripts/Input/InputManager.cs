using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInput {

    public bool IsFire { get; private set; }
    public Vector2 LookDirection { get; private set; }

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        IsFire = Input.GetButtonDown("Fire1");
        LookDirection = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
