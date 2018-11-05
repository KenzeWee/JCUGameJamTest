using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerEnter : MonoBehaviour
{
    private bool isReady = false;

    [SerializeField] private List<GameObject> playerObj = new List<GameObject>();
    [SerializeField] private List<PlayerVariable> playerData = new List<PlayerVariable>();

    private void Start()
    {
        foreach (PlayerVariable player in playerData)
        {
            player.controlType = StaticFunctions.CONTROLLERTYPE.NULL;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputAssign();
        MainMenuManager.instance.StartGame();
    }

    private void InputAssign()
    {
        for (int i = 1; i < 5; i++)
        {
            if (Input.GetButtonDown("JoyStick_" + i + "_Fire"))
            {
                foreach (PlayerVariable player in playerData)
                {
                    if (player.controlType == StaticFunctions.CONTROLLERTYPE.NULL)
                    {
                        player.controlType = (StaticFunctions.CONTROLLERTYPE) i;
                    }
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            foreach (PlayerVariable player in playerData)
            {
                if (player.controlType == StaticFunctions.CONTROLLERTYPE.NULL)
                {
                    player.controlType = StaticFunctions.CONTROLLERTYPE.KEYBOARD;
                }
            }
        }
    }
}