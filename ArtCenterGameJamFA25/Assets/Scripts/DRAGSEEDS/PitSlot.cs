using UnityEngine;

public class PitSlot : MonoBehaviour
{
    public bool occupied { get; private set; }
    [HideInInspector] public RectTransform rectTransform;

    private SeedInstance currentSeed;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    
    public bool TryPlace(SeedInstance seed)
    {
        if (occupied) return false;
        occupied = true;
        currentSeed = seed;

        seed.transform.SetParent(transform, worldPositionStays: false);
        seed.rectTransform.anchoredPosition = Vector2.zero;

      
        if (seed.image) seed.image.raycastTarget = false;

        return true;
    }


    public void Vacate()
    {
        occupied = false;
        currentSeed = null;
    }
}
