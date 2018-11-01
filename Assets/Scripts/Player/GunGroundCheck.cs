using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGroundCheck : MonoBehaviour {

    public bool isGrounded { get; private set;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;

    }
}
