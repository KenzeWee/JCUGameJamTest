using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInput {

    public PlayerVariable player;
    public StaticFunctions.CONTROLLERTYPE controlType;

    public bool IsFire { get; private set; }
    public Vector2 CursorPos { get; private set; }

    private Camera mainCam;

    private void Start () {
        mainCam = Camera.main;
        if (controlType == StaticFunctions.CONTROLLERTYPE.NULL){
            controlType = player.controlType;
        }
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.Instance == null || GameManager.Instance.IsGameRunning)
            UpdateInput ();
    }

    void UpdateInput () {
        Vector2 worldPos = Vector2.zero;

        switch (controlType) {
            case StaticFunctions.CONTROLLERTYPE.KEYBOARD:
                IsFire = Input.GetMouseButtonDown (0);
                CursorPos = mainCam.ScreenToWorldPoint (Input.mousePosition);
                break;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER01:
                IsFire = Input.GetButtonDown ("JoyStick_1_Fire");

                worldPos = new Vector2 (transform.position.x + Input.GetAxis ("JoyStick_1_Horizontal"),
                    transform.position.y - Input.GetAxis ("JoyStick_1_Vertical"));

                CursorPos = worldPos;
                break;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER02:
                IsFire = Input.GetButtonDown ("JoyStick_2_Fire");

                worldPos = new Vector2 (transform.position.x + Input.GetAxis ("JoyStick_2_Horizontal"),
                    transform.position.y - Input.GetAxis ("JoyStick_2_Vertical"));

                CursorPos = worldPos;
                break;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER03:
                IsFire = Input.GetButtonDown ("JoyStick_3_Fire");

                worldPos = new Vector2 (transform.position.x + Input.GetAxis ("JoyStick_3_Horizontal"),
                    transform.position.y - Input.GetAxis ("JoyStick_3_Vertical"));

                CursorPos = worldPos;
                break;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER04:
                IsFire = Input.GetButtonDown ("JoyStick_4_Fire");

                worldPos = new Vector2 (transform.position.x + Input.GetAxis ("JoyStick_4_Horizontal"),
                    transform.position.y - Input.GetAxis ("JoyStick_4_Vertical"));

                CursorPos = worldPos;
                break;
        }
    }
}