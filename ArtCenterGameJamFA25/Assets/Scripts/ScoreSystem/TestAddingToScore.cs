using UnityEngine;
using UnityEngine.InputSystem;

public class TestAddingToScore : MonoBehaviour
{
    public Score score;

    void Update()
    {
        bool spaceKeyPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        if (spaceKeyPressed)
        {
            score.AddScore(120);
        }
    }
}
