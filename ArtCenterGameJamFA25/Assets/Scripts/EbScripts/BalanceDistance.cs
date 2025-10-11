using UnityEngine;

public class BalanceDistance : MonoBehaviour
{
    public RectTransform sun, moon;
    public float idealDistance = 450f;

    private RectTransform thisTransform;

    void Update()
    {
        
    }

    private void LerpFromMouse(RectTransform selected, RectTransform nonSelected)
    {
        // get mouse position
        Vector2 mousePos = Vector2.zero;

        // find ideal distance as percentage between this pos and mouse pos
        float distThisAndMouse = Vector2.Distance(thisTransform.position, mousePos);
        float targetDist = idealDistance / distThisAndMouse;

        // lerp selected between mouse and this 
        Vector2 targetPosition = Vector2.Lerp(thisTransform.position, mousePos, targetDist);
        selected.position = targetPosition;

        // update non selected to be opposite side of selected
        Vector2 oppTargetPos = Vector2.Lerp(thisTransform.position, mousePos, -targetDist);
        nonSelected.position = oppTargetPos;
    }
}
