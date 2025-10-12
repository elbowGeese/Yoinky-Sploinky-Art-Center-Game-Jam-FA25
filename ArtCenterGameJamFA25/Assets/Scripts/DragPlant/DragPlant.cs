using UnityEngine;

[RequireComponent(typeof(UIHoverDetection))]
public class DragPlant : MonoBehaviour
{
    public enum DragState { IDLE, DRAGGING, RETURNING }
    public DragState state;
    public bool IsBeingDragged { get { return state == DragState.DRAGGING; } }

    public float draggingSpeed = 5f;
    public float returningSpeed = 10f;

    private PlantScript plant;
    private UIHoverDetection uiHover;
    private MousePosition mouseInput;
    private RectTransform rectTransform;
    private RectTransform dirtPlotTransform;
    private RectTransform uiDraggingLayer;

    void Start()
    {
        plant = GetComponent<PlantScript>();
        uiHover = GetComponent<UIHoverDetection>();
        rectTransform = GetComponent<RectTransform>();
        dirtPlotTransform = rectTransform.parent.GetComponent<RectTransform>();

        mouseInput = FindFirstObjectByType<MousePosition>();
        uiDraggingLayer = GameObject.FindWithTag("UIDraggingLayer").GetComponent<RectTransform>();
    }

    void Update()
    {
        switch (state)
        {
            case DragState.DRAGGING:
                DraggingUpdate(Time.deltaTime);
                break;
            case DragState.RETURNING:
                ReturningUpdate(Time.deltaTime);
                break;
            case DragState.IDLE:
                IdleUpdate();
                break;
            default:
                Debug.Log("No valid state.");
                break;
        }
    }

    private void DraggingUpdate(float frame)
    {
        if (mouseInput == null) { Debug.Log("NO MOUSE INPUT HELP!!!!!!!"); return; }

        if (!mouseInput.isMouseDown)
        {
            CheckForDrop();

            state = DragState.RETURNING;
            return;
        }

        if (!plant.isPaused) { plant.isPaused = true; }

        if (rectTransform.parent.GetComponent<RectTransform>() != uiDraggingLayer) { rectTransform.transform.SetParent(uiDraggingLayer); }

        // follow mouse
        Vector2 targetPosition = Vector2.Lerp(rectTransform.position, mouseInput.screenPosition, draggingSpeed * frame);
        rectTransform.position = targetPosition;
    }

    private void ReturningUpdate(float frame)
    {
        // lerp back to plot
        Vector2 targetPosition = Vector2.Lerp(rectTransform.position, dirtPlotTransform.position, returningSpeed * frame);
        rectTransform.position = targetPosition;

        if(Vector2.Distance(rectTransform.position, dirtPlotTransform.position) < 0.1f)
        {
            rectTransform.position = dirtPlotTransform.position;
            state = DragState.IDLE;
        }
    }

    private void IdleUpdate()
    {
        if (rectTransform.parent.GetComponent<RectTransform>() != dirtPlotTransform) { rectTransform.transform.SetParent(dirtPlotTransform); }

        if (plant.isPaused) { plant.isPaused = false; }

        if (uiHover.isMouseOver && mouseInput.onMouseDown)
        {
            state = DragState.DRAGGING;
            return;
        }

        // do nothing i guess
    }

    private void CheckForDrop()
    {
        // check for ticket
        if (plant.isGrown)
        {
            RectTransform ticket = GameObject.FindWithTag("Ticket").GetComponent<RectTransform>();
            if (ticket != null)
            {
                if (RectHelp.IsOverlapping(rectTransform, ticket))
                {
                    string plantName = plant.plantName;
                    if (ticket.GetComponent<OverarchingTicket>().TryToSubmit(plantName))
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        // check for trash
        RectTransform trash = GameObject.FindWithTag("Trash").GetComponent<RectTransform>();
        if (trash != null)
        {
            if (RectHelp.IsOverlapping(rectTransform, trash))
            {
                Destroy(gameObject);
            }
        }
    }
}
