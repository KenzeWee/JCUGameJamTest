using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerEnter : MonoBehaviour
{
    //Make sure to assign these in order
    //ie player 1 should be first in the list
    //player 2 should be second and so on
    [SerializeField] private List<GameObject> playerObj = new List<GameObject>();

    //make sure it follows the same order as the playerObj
    //ie player 1 should correspond to the index of player 1 in player obj
    [SerializeField] private List<PlayerVariable> playerData = new List<PlayerVariable>();

    IDictionary<StaticFunctions.CONTROLLERTYPE, bool> availableControllers = new Dictionary<StaticFunctions.CONTROLLERTYPE, bool>()
    {
        { StaticFunctions.CONTROLLERTYPE.CONTROLLER01, false },
        { StaticFunctions.CONTROLLERTYPE.CONTROLLER02, false },
        { StaticFunctions.CONTROLLERTYPE.CONTROLLER03, false },
        { StaticFunctions.CONTROLLERTYPE.CONTROLLER04, false },
        { StaticFunctions.CONTROLLERTYPE.KEYBOARD, false }
    };

    private bool acceptControllerInputs = true;

    private void Start()
    {
        foreach (PlayerVariable player in playerData)
        {
            player.controlType = StaticFunctions.CONTROLLERTYPE.NULL;
        }
    }

    void Update()
    {
        if (acceptControllerInputs)
        {
            if (InputAssign())
            {
                MainMenuManager.instance.StartGame();
            }
        }

        //Debug Code to check Dictionary Values
        //foreach (KeyValuePair<StaticFunctions.CONTROLLERTYPE, bool> kvp in availableControllers)
        //{
        //    Debug.Log("Controller: " + kvp.Key.ToString() + " Assigned?: " + kvp.Value);
        //}
    }

    private bool InputAssign()
    {
        //loop through the 5 possible controller configurations
        //and check dictionary of controllers to see if 
        //controller has been assigned before
        for (int i = 1; i < 6; i++)
        {
            if (Input.GetButtonDown("JoyStick_" + i + "_Fire"))
            {
                foreach (PlayerVariable player in playerData)
                {
                    //Check to see if dictionary has the input, then
                    //loop through each player one by one
                    //if a controller has not been assigned
                    //assign the controller to the player
                    bool result;
                    if (availableControllers.TryGetValue((StaticFunctions.CONTROLLERTYPE)i, out result))
                    {
                        if (!result)
                        {
                            if (player.controlType == StaticFunctions.CONTROLLERTYPE.NULL)
                            {
                                player.controlType = (StaticFunctions.CONTROLLERTYPE)i;
                                availableControllers[(StaticFunctions.CONTROLLERTYPE)i] = true;
                                playerObj[playerData.IndexOf(player)].SetActive(true);
                                return true;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Controller number " + i + " is either not configured in the Input Settings or has not been added to the dictionary in MenuPlayerEnter.");
                    }
                }
                return false;
            }
        }
        return false;
    }

    //Disable people joining in when the help screen is open
    public void ToggleAcceptControllerInput()
    {
        acceptControllerInputs = !acceptControllerInputs;
    }
}