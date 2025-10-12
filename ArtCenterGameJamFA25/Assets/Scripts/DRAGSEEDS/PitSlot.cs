using UnityEngine;
using UnityEngine.UI;

public class PitSlot: MonoBehaviour
{
    public bool occupied { get; private set; }
    [HideInInspector] public RectTransform rectTransform;

    [Header("Visual")]
    [SerializeField] private Color highlightColor = new Color(0.8f, 1f, 0.8f);

    private Color baseColor;
    private Image img;
    private SeedInstance currentSeed;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        baseColor = img ? img.color : Color.white;
    }

    public bool TryPlace(SeedInstance seed)
    {
        if (occupied) return false;
        occupied = true;
        currentSeed = seed;

        seed.transform.SetParent(transform, worldPositionStays: false);
        seed.rectTransform.anchoredPosition = Vector2.zero;

        if (img) img.color = highlightColor;
        if (seed.image) seed.image.raycastTarget = false;
        return true;
    }

    public void Vacate()
    {
        occupied = false;
        currentSeed = null;
        if (img) img.color = baseColor;
    }
}
