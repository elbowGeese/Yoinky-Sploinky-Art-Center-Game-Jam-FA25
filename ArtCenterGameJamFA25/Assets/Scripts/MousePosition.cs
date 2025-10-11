using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    public Vector2 screenPosition;

    public bool isMouseDown;

    // Update is called once per frame
    void Update()
    {
       //screenPosition = Input.mousePosition;  //this is old input system
      screenPosition =  Mouse.current.position.ReadValue();

        isMouseDown = Mouse.current.leftButton.isPressed;

    }



}
