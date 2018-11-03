using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerVariable : ScriptableObject {
    [SerializeField]
    private string m_name;

    public string currentName { get { return m_name; }}

    public int CurrentHP { get; set; }
    public int CurrentScore { get; set; }
    
    public enum CONTROLLERTYPE {NULL, KEYBOARD, CONTROLLER01, CONTROLLER02, CONTROLLER03, CONTROLLER04 };
    public CONTROLLERTYPE controlType = CONTROLLERTYPE.NULL;

    public InputManager.ControllerType getControl()
    {
        switch (controlType)
        {
            case CONTROLLERTYPE.CONTROLLER01:
                return InputManager.ControllerType.CONTROLLER01;
                break;
            case CONTROLLERTYPE.CONTROLLER02:
                return InputManager.ControllerType.CONTROLLER02;

                break;
            case CONTROLLERTYPE.CONTROLLER03:
                return InputManager.ControllerType.CONTROLLER03;

                break;
            case CONTROLLERTYPE.CONTROLLER04:
                return InputManager.ControllerType.CONTROLLER04;

                break;
            case CONTROLLERTYPE.KEYBOARD:
                return InputManager.ControllerType.KEYBOARD;

            default:
                
                return InputManager.ControllerType.KEYBOARD;
        }
}
}
