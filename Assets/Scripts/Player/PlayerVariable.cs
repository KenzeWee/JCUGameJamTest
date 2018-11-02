using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerVariable : ScriptableObject {
    public int CurrentHP { get; set; }
    public int CurrentScore { get; set; }
}
