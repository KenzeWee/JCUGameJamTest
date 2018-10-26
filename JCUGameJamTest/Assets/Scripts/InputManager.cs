using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private bool pressedmouse1;
    public bool isMouseClicked { get { return pressedmouse1; } }

    // Update is called once per frame
    void Update()
    {
        pressedmouse1 = Input.GetButtonDown("Fire1");
    }
}
