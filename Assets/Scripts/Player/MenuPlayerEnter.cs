using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerEnter : MonoBehaviour
{
    private bool isReady = false;

    [SerializeField] private List<GameObject> playerObj = new List<GameObject>();
    [SerializeField] private List<PlayerVariable> playerData = new List<PlayerVariable>();

    List<StaticFunctions.CONTROLLERTYPE> assignedControllers = new List<StaticFunctions.CONTROLLERTYPE>();

    private bool acceptControllerInputs = true;

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
        if (acceptControllerInputs)
        {
            if (InputAssign())
            {
                MainMenuManager.instance.StartGame();
            }
        }
    }

    private bool InputAssign()
    {
        //loop through the 4 possible joystick configurations
        //and check for keyboard
        for (int i = 1; i < 6; i++)
        {
            if (Input.GetButtonDown("JoyStick_" + i + "_Fire"))
            {
                foreach (PlayerVariable player in playerData)
                {
                    //loop through each player one by one
                    //if not assigned, assign the player
                    if (!assignedControllers.Contains((StaticFunctions.CONTROLLERTYPE)i))
                    {
                        if (player.controlType == StaticFunctions.CONTROLLERTYPE.NULL)
                        {
                            player.controlType = (StaticFunctions.CONTROLLERTYPE)i;
                            assignedControllers.Add((StaticFunctions.CONTROLLERTYPE)i);
                            playerObj[playerData.IndexOf(player)].SetActive(true);
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        return false;
    }

    public void ToggleAcceptControllerInput()
    {
        acceptControllerInputs = !acceptControllerInputs;
    }
}