using UnityEngine;

public interface IInput {
    bool IsFire { get; }
    Vector2 CursorPos { get; }
    void GameEnd();
}
