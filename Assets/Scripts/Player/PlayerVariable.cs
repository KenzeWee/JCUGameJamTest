using UnityEngine;

[CreateAssetMenu (menuName = "PlayerHealth")]
public class PlayerVariable : ScriptableObject {
    [SerializeField]
    private string m_name;

    public string currentName { get { return m_name; } }

    public int CurrentHP { get; set; }
    public int CurrentScore { get; set; }
    public StaticFunctions.CONTROLLERTYPE controlType = StaticFunctions.CONTROLLERTYPE.NULL;

    public StaticFunctions.CONTROLLERTYPE getControl () {
        switch (controlType) {
            case StaticFunctions.CONTROLLERTYPE.CONTROLLER01:
                return StaticFunctions.CONTROLLERTYPE.CONTROLLER01;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER02:
                return StaticFunctions.CONTROLLERTYPE.CONTROLLER02;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER03:
                return StaticFunctions.CONTROLLERTYPE.CONTROLLER03;

            case StaticFunctions.CONTROLLERTYPE.CONTROLLER04:
                return StaticFunctions.CONTROLLERTYPE.CONTROLLER04;

            case StaticFunctions.CONTROLLERTYPE.KEYBOARD:
                return StaticFunctions.CONTROLLERTYPE.KEYBOARD;

            default:
                return StaticFunctions.CONTROLLERTYPE.KEYBOARD;
        }
    }
}