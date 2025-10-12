using UnityEngine;

public class MouseInteractFeedback : MonoBehaviour
{
    private RectTransform rectTransform;
    private MousePosition mouseInput;

    public Animator anim;
    public bool hovering = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mouseInput = FindFirstObjectByType<MousePosition>();
    }

    void Update()
    {
        if (mouseInput != null)
        {
            rectTransform.position = mouseInput.screenPosition;
            anim.SetBool("hovering", hovering && !mouseInput.isMouseDown);
        }
    }
}
