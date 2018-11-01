using UnityEngine;

public interface IInput {
    bool IsFire { get; }
    bool ChangePivot { get; }
    Vector2 CursorPos { get; }
}
