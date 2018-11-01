using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInput
{
    public bool IsFire { get; private set; }
    public Vector2 CursorPos { get; private set; }

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameRunning)
            UpdateInput();
    }

    void UpdateInput()
    {
        IsFire = Input.GetButtonDown("Fire1");
        CursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
}
