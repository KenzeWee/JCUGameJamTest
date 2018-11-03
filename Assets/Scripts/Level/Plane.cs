using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : GenericLevel {
   protected void Awake() {
       gameObject.layer = 13;
   }
}
