using UnityEngine;

public interface IInput {
    bool IsFire { get; }
    Vector2 LookDirection { get; }
    void GameEnd();
}
