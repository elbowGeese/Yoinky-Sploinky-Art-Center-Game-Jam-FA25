using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    public Vector2 screenPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //screenPosition = Input.mousePosition;  //this is old input system
      screenPosition =  Mouse.current.position.ReadValue();
      

    }
}
