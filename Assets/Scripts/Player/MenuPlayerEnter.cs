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
        if (InputAssign())
        {
            MainMenuManager.instance.StartGame();
        }
    }

    private bool InputAssign()
    {
        //loop through the 4 possible joystick configurations
        //and check for keyboard
        for (int i = 1; i < 5; i++)
        {
            if (Input.GetButtonDown("JoyStick_" + i + "_Fire"))
            {
                foreach (PlayerVariable player in playerData)
                {
                    //loop through each player one by one
                    //if not assigned, assign the player
                    if (player.controlType == StaticFunctions.CONTROLLERTYPE.NULL)
                    {
                        player.controlType = (StaticFunctions.CONTROLLERTYPE)i;
                        playerObj[playerData.IndexOf(player)].SetActive(true);
                        return true;
                    }
                }
                return false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            for (int j = 0; j < playerData.Count; j++)
            {
                if (playerData[j].controlType == StaticFunctions.CONTROLLERTYPE.NULL)
                {
                    playerData[j].controlType = StaticFunctions.CONTROLLERTYPE.KEYBOARD;
                    playerObj[j].SetActive(true);
                    return true;
                }
            }
            return false;
        }

        return false;
    }
}