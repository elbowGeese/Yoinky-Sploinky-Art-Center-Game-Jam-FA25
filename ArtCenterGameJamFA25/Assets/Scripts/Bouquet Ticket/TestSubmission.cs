using UnityEngine;
using UnityEngine.InputSystem;

public class TestSubmission : MonoBehaviour
{
    public OverarchingTicket ticketRef;

    public string plantName;

    // Update is called once per frame
    void Update()
    {
        bool spaceKeyPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        if (spaceKeyPressed)
        {
           ticketRef.TryToSubmit(plantName);
        }
    }
}
