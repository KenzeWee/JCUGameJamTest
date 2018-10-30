using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput {
    bool IsFire { get; }
    Vector2 LookDirection { get; }
}
