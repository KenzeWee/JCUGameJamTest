using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : GenericLevel {
   protected override void Awake() {
       base.Awake();
       gameObject.layer = 13;
   }
}
