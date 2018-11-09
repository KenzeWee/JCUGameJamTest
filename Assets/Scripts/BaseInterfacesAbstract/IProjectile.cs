using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile {
    float FireForce { get; }
    void Fire(GameObject playerWhoShot);
}
