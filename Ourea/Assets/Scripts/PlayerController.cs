using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private void Update()
    {
        ExtraMovement();
    }

    private void ExtraMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

        }
    }
}