using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerVariable : ScriptableObject {
    [SerializeField]
    private string m_name;

    public string currentName { get { return m_name; }}

    public int CurrentHP { get; set; }
    public int CurrentScore { get; set; }
}
