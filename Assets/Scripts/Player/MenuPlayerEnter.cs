using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerEnter : MonoBehaviour {
    private bool isReady = false;

    public GameObject playerObj;
    public PlayerVariable player;

    private void Start () {
        player.controlType = StaticFunctions.CONTROLLERTYPE.NULL;
    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<InputManager> ().IsFire) {
            isReady = true;
            playerObj.SetActive (true);

            switch (GetComponent<InputManager> ().controlType) {
                case StaticFunctions.CONTROLLERTYPE.CONTROLLER01:
                    player.controlType = StaticFunctions.CONTROLLERTYPE.CONTROLLER01;
                    break;
                case StaticFunctions.CONTROLLERTYPE.CONTROLLER02:
                    player.controlType = StaticFunctions.CONTROLLERTYPE.CONTROLLER02;

                    break;
                case StaticFunctions.CONTROLLERTYPE.CONTROLLER03:
                    player.controlType = StaticFunctions.CONTROLLERTYPE.CONTROLLER03;

                    break;
                case StaticFunctions.CONTROLLERTYPE.CONTROLLER04:
                    player.controlType = StaticFunctions.CONTROLLERTYPE.CONTROLLER04;

                    break;
                case StaticFunctions.CONTROLLERTYPE.KEYBOARD:
                    player.controlType = StaticFunctions.CONTROLLERTYPE.KEYBOARD;
                    break;
            }

            MainMenuManager.instance.StartGame ();
        }
    }
}