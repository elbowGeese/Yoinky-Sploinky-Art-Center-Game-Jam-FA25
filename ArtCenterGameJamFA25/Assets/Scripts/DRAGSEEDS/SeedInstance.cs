using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SeedInstance : MonoBehaviour
{
    [Header("Snap / Magnet")]
    public float snapRadiusScreenPx; 
    public float magnetStrength;  
    public float snapTime = 0.12f;  
    public float returnTime = 0.18f;  

    [Header("Flower")]
    public Sprite flowerSprite;              
    [Range(0.1f, 3f)]
    public float flowerScaleMultiplier = 1.2f;

    [Header("Flower Prefab")]
    public GameObject flowerPrefab;

    [HideInInspector] public RectTransform rectTransform;
    //[HideInInspector] public Image image;
    [HideInInspector] public Canvas parentCanvas;


    RectTransform dragLayer;           
    RectTransform slotRect;          
    Vector2 slotAnchoredInDrag;   

   
    [HideInInspector] public PitSlot currentHoverPit;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        //image = GetComponent<Image>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    
    public void Init(RectTransform fromSlot, RectTransform dragLayer, Canvas canvas)
    {
        this.slotRect = fromSlot;
        this.dragLayer = dragLayer;
        this.parentCanvas = canvas;

      
        slotAnchoredInDrag = WorldToAnchored(dragLayer, slotRect.position);
    }

 
    public void HandleBeginDrag(PointerEventData eventData, RectTransform dragLayer)
    {
        transform.SetParent(dragLayer, worldPositionStays: false);
        UpdatePosition(eventData.position);
    }

   
    public void HandleDrag(PointerEventData eventData)
    {
        Vector2 mouseAnchored = ScreenToAnchored(dragLayer, eventData.position);
        Vector2 target = mouseAnchored;

        if (currentHoverPit != null)
        {
            Vector2 pitScreen = RectTransformUtility.WorldToScreenPoint(parentCanvas.worldCamera, currentHoverPit.rectTransform.position);
            float dist = Vector2.Distance(eventData.position, pitScreen);

            if (dist <= snapRadiusScreenPx && !currentHoverPit.occupied)
            {
                Vector2 pitAnchored = ScreenToAnchored(dragLayer, pitScreen);
                float t = Mathf.Clamp01(1f - dist / snapRadiusScreenPx); 
                float strength = magnetStrength * t;
                target = Vector2.Lerp(mouseAnchored, pitAnchored, strength);
            }
        }

        rectTransform.anchoredPosition = target;
    }


    public void HandleEndDrag(PointerEventData eventData)
    {
        PitSlot targetPit = null;
        if (currentHoverPit != null && !currentHoverPit.occupied)
        {
            Vector2 pitScreen = RectTransformUtility.WorldToScreenPoint(parentCanvas.worldCamera, currentHoverPit.rectTransform.position);
            float dist = Vector2.Distance(eventData.position, pitScreen);
            if (dist <= snapRadiusScreenPx) targetPit = currentHoverPit;
        }

        if (targetPit != null)
        {
            
            Vector2 pitAnchored = WorldToAnchored(dragLayer, targetPit.rectTransform.position);
            StartCoroutine(TweenAnchored(rectTransform.anchoredPosition, pitAnchored, snapTime, () =>
            {
                targetPit.TryPlace(this);  
                //MorphToFlower(true);        
            }));
        }
        else
        {
          
            StartCoroutine(TweenAnchored(rectTransform.anchoredPosition, slotAnchoredInDrag, returnTime, () =>
            {
                Destroy(gameObject);
            }));
        }

        currentHoverPit = null;
    }

   
    //public void MorphToFlower(bool playBloom)
    //{
        //if (image == null) return;

       
        //Vector2 baseSize = rectTransform.sizeDelta;

        //if (flowerSprite != null)
        //{
            //image.sprite = flowerSprite;
        //}

      
       // rectTransform.sizeDelta = baseSize * Mathf.Max(0.01f, flowerScaleMultiplier);

       
        //image.raycastTarget = false;

      
        //if (playBloom)
            //StartCoroutine(Bloom(0.1f, 0.85f, 1f));
    //}

   
    IEnumerator Bloom(float duration, float fromScale, float toScale)
    {
        float t = 0f;
        Vector3 from = Vector3.one * fromScale;
        Vector3 to = Vector3.one * toScale;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / Mathf.Max(0.0001f, duration);
            float ease = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            rectTransform.localScale = Vector3.LerpUnclamped(from, to, ease);
            yield return null;
        }
        rectTransform.localScale = Vector3.one;
    }

  
    private void UpdatePosition(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            dragLayer,
            screenPos,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out var local);
        rectTransform.anchoredPosition = local;
    }

    Vector2 ScreenToAnchored(RectTransform parent, Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent, screenPos,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out var local);
        return local;
    }

    Vector2 WorldToAnchored(RectTransform parent, Vector3 worldPos)
    {
        Vector2 screen = RectTransformUtility.WorldToScreenPoint(parentCanvas.worldCamera, worldPos);
        return ScreenToAnchored(parent, screen);
    }

    IEnumerator TweenAnchored(Vector2 from, Vector2 to, float dur, System.Action onComplete)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / Mathf.Max(0.0001f, dur);
            float ease = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            rectTransform.anchoredPosition = Vector2.LerpUnclamped(from, to, ease);
            yield return null;
        }
        onComplete?.Invoke();
    }
}
