using System.Collections;
using UnityEngine;

public class SeedInstance : MonoBehaviour
{
    private bool isDragging;
    private Vector3 dragOffset;
    private Camera cam;
    private PitSlot placedPit;

    private Vector3 originPos;
    private Transform returnTarget;

    public void Init(Vector3 origin, Transform returnTarget)
    {
        this.originPos = origin;
        this.returnTarget = returnTarget;
    }

    void Awake()
    {
        cam = Camera.main;
    }

    public void BeginDrag(Vector3 worldMousePos)
    {
        isDragging = true;
        dragOffset = transform.position - worldMousePos;
    }

    public void UpdateDragging(Vector3 worldMousePos)
    {
        if (!isDragging) return;
        Vector3 target = worldMousePos + dragOffset;
        target.z = 0f;
        transform.position = target;
    }

    public void EndDrag(bool placed)
    {
        isDragging = false;

        if (!placed && placedPit == null)
        {
            StartCoroutine(ReturnAndDestroy(0.2f));
        }
    }

    public void SetPlaced(PitSlot pit)
    {
        placedPit = pit;
        isDragging = false;
    }

    private IEnumerator ReturnAndDestroy(float duration)
    {
        Vector3 start = transform.position;
        Vector3 end = returnTarget ? returnTarget.position : originPos;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
