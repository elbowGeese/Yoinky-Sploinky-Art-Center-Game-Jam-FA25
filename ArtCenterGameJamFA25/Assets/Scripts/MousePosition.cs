using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    public Vector2 screenPosition;

    public Vector3 worldPosition;


    // Update is called once per frame
    void Update()
    {
       //screenPosition = Input.mousePosition;  //this is old input system
     screenPosition =  Mouse.current.position.ReadValue();

       Vector3 screenPositionWithZ = new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane + 1f);

        worldPosition = Camera.main.ScreenToWorldPoint(screenPositionWithZ);
        

        transform.position = worldPosition;

    }

    public void OnMouseDown()
    {

    }

    public void OnMouseUp()
    {
        
    }
}
