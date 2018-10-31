using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected abstract void SuscribeToEvents();
    protected abstract void UnsuscribeToEvents();
}
