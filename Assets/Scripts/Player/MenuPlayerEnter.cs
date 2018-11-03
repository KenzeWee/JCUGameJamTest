using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerEnter : MonoBehaviour {
    private bool isReady = false;

    public GameObject playerObj;
    public PlayerVariable player;
    

    private void Start()
    {
        player.controlType = PlayerVariable.CONTROLLERTYPE.NULL;
    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<InputManager>().IsFire)
        {
            isReady = true;
            playerObj.SetActive(true);

            switch (GetComponent<InputManager>().ControlType)
            {
                case InputManager.ControllerType.CONTROLLER01:
                    player.controlType = PlayerVariable.CONTROLLERTYPE.CONTROLLER01;
                    break;
                case InputManager.ControllerType.CONTROLLER02:
                    player.controlType = PlayerVariable.CONTROLLERTYPE.CONTROLLER02;

                    break;
                case InputManager.ControllerType.CONTROLLER03:
                    player.controlType = PlayerVariable.CONTROLLERTYPE.CONTROLLER03;

                    break;
                case InputManager.ControllerType.CONTROLLER04:
                    player.controlType = PlayerVariable.CONTROLLERTYPE.CONTROLLER04;

                    break;
                case InputManager.ControllerType.KEYBOARD:
                    player.controlType = PlayerVariable.CONTROLLERTYPE.KEYBOARD;
                    break;
            }

            MainMenuManager.instance.StartGame();
        }
	}
}
