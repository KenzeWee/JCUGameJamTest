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
}
