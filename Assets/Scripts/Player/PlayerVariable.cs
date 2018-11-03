using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerVariable : ScriptableObject {
    public string currentName { get; set; }

    public int CurrentHP { get; set; }
    public int CurrentScore { get; set; }
}
