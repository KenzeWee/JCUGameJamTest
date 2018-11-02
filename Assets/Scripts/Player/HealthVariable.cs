using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class HealthVariable : ScriptableObject {
    public int CurrentHP { get; set; }
}
