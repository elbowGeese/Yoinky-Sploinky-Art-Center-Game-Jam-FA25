using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIHoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isMouseOver = false;

    private MouseInteractFeedback mouseFeedback;

    void Start()
    {
        mouseFeedback = FindFirstObjectByType<MouseInteractFeedback>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;

        if (mouseFeedback != null)
        {
            mouseFeedback.hovering = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;

        if (mouseFeedback != null)
        {
            mouseFeedback.hovering = false;
        }
    }
}
