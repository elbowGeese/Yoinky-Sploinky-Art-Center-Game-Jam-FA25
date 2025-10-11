using UnityEngine;

public class BalanceDistance : MonoBehaviour
{
    public RectTransform sun, moon;
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
                break;
            case MovingState.SUN:
                LerpFromMouse(sun, moon);
                break;
            case MovingState.MOON:
                LerpFromMouse(moon, sun);
                break;
            default:
                break;
        }
    }

    public void GrabMoon()
    {
        if(state == MovingState.NONE)
        {
            state = MovingState.MOON;
        }
    }

    public void GrabSun()
    {
        if (state == MovingState.NONE)
        {
            state = MovingState.SUN;
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
