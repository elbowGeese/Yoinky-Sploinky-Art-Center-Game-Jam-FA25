using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeedSlotSpawner : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Prefab & Layers")]
    public SeedInstance seedPrefab;  
    public RectTransform dragLayer;  

    [Header("Visual")]
    public Color seedColor = Color.white;   
    public Sprite flowerSprite;           
    public bool spawnFromSlotCenter = true;

    Canvas canvas;
    RectTransform slotRect;
    SeedInstance currentSeed;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        slotRect = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentSeed != null || seedPrefab == null || dragLayer == null) return;

     
        currentSeed = Instantiate(seedPrefab, dragLayer);
        if (currentSeed.image) currentSeed.image.color = seedColor;

      
        currentSeed.Init(slotRect, dragLayer, canvas);

      
        currentSeed.flowerSprite = flowerSprite;

        if (spawnFromSlotCenter)
            currentSeed.GetComponent<RectTransform>().anchoredPosition =
                WorldToAnchored(dragLayer, slotRect.position, canvas);

        currentSeed.HandleBeginDrag(eventData, dragLayer);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentSeed == null) return;

     
        PitSlot nearest = null;
        float best = float.MaxValue;

        var pits = Object.FindObjectsByType<PitSlot>(FindObjectsSortMode.None);
        foreach (var pit in pits)
        {
            Vector2 pitScreen = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, pit.rectTransform.position);
            float d = (pitScreen - eventData.position).sqrMagnitude;
            if (d < best) { best = d; nearest = pit; }
        }

        currentSeed.currentHoverPit = nearest;
        currentSeed.HandleDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentSeed == null) return;
        currentSeed.HandleEndDrag(eventData);
        currentSeed = null;
    }

  
    Vector2 WorldToAnchored(RectTransform parent, Vector3 worldPos, Canvas canvas)
    {
        Vector2 screen = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent, screen,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out var local);
        return local;
    }
}
