using UnityEngine;

public class PitSlot : MonoBehaviour
{
    public bool occupied { get { return currentFlower != null; } }
    [HideInInspector] public RectTransform rectTransform;

    private GameObject currentFlower;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    
    public bool TryPlace(SeedInstance seed)
    {
        if (occupied) return false;
        //occupied = true;

        currentFlower = Instantiate(seed.flowerPrefab, rectTransform);
        Destroy(seed.gameObject);

        //currentSeed = seed;

        //seed.transform.SetParent(transform, worldPositionStays: false);
        //seed.rectTransform.anchoredPosition = Vector2.zero;

      
        //if (seed.image) seed.image.raycastTarget = false;

        return true;
    }


    public void Vacate()
    {
        //occupied = false;
        //currentSeed = null;
    }
}
