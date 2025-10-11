using UnityEngine;

public class BalanceDistance : MonoBehaviour
{
    public RectTransform sun, moon;
    public UIHoverDetection sunHover, moonHover;
    public float distanceMult = 1f;
    private float idealDistance = 450f;

    private RectTransform thisTransform;

    private MousePosition mousePos;

    private enum MovingState { NONE, SUN, MOON };
    private MovingState state;

    private void Start()
    {
        thisTransform = GetComponent<RectTransform>();
        mousePos = FindFirstObjectByType<MousePosition>();

        state = MovingState.NONE;
    }

    void Update()
    {
        idealDistance = ((thisTransform.localScale.x * Screen.width) / 2) * distanceMult;

        switch (state)
        {
            case MovingState.NONE:
                Debug.Log("NONE");

                if (mousePos.isMouseDown) // && mouse inventory is empty
                {
                    if (sunHover.isMouseOver)
                    {
                        state = MovingState.SUN;
                    }

                    if (moonHover.isMouseOver)
                    {
                        state = MovingState.MOON;
                    }
                }

                break;
            case MovingState.SUN:
                Debug.Log("SUN");

                LerpFromMouse(sun, moon);
                CheckRelease();

                break;
            case MovingState.MOON:
                Debug.Log("MOON");

                LerpFromMouse(moon, sun);
                CheckRelease();

                break;
            default:
                Debug.Log("Invalid state change.");
                break;
        }
    }

    private void CheckRelease()
    {
        if (!mousePos.isMouseDown)
        {
            state = MovingState.NONE;
        }
    }

    private void LerpFromMouse(RectTransform selected, RectTransform nonSelected)
    {
        // get mouse position
        Vector2 mousePos = this.mousePos.screenPosition;

        Vector2 thisPos = thisTransform.position;

        // find ideal distance as percentage between this pos and mouse pos
        float distThisAndMouse = Vector2.Distance(thisPos, mousePos);
        float targetDist = idealDistance / distThisAndMouse;

        // lerp selected between mouse and this 
        Vector2 targetPosition = Vector2.LerpUnclamped(thisPos, mousePos, targetDist);
        selected.position = targetPosition;

        // update non selected to be opposite side of selected
        Vector2 oppTargetPos = Vector2.LerpUnclamped(thisPos, mousePos, -targetDist);
        nonSelected.position = oppTargetPos;
    }
}
