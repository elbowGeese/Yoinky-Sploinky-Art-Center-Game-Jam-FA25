using UnityEngine;

public class PitSlot : MonoBehaviour
{
    public bool occupied { get; private set; }
    private SeedInstance currentSeed;

    [Header("Placement")]
    public Transform anchor;                 
    public Vector3 localOffset = Vector3.zero; 

    private SpriteRenderer sr;
    private Color baseColor;
    public Color canColor = new Color(0.7f, 1f, 0.7f);
    public Color cannotColor = new Color(1f, 0.7f, 0.7f);

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr) baseColor = sr.color;

        if (!anchor)
        {
            var a = transform.Find("SeedAnchor");
            if (a) anchor = a;
        }
    }

    public bool TryPlace(SeedInstance seed)
    {
        if (occupied) return false;
        occupied = true;
        currentSeed = seed;

       
        Transform parentT = anchor ? anchor : transform;
        seed.transform.SetParent(parentT, /*worldPositionStays:*/ true);

        
        Vector3 worldTarget = parentT.TransformPoint(localOffset);
        seed.transform.position = worldTarget;
        seed.transform.rotation = Quaternion.identity; 
       

        
        var col = seed.GetComponent<Collider2D>(); if (col) col.enabled = false;
        var rb = seed.GetComponent<Rigidbody2D>(); if (rb) rb.simulated = false;

        seed.SetPlaced(this);
        SetNormal();
        return true;
    }

    public void Vacate()
    {
        occupied = false;
        currentSeed = null;
        SetNormal();
    }

    public void SetHighlight(bool canPlace)
    {
        if (!sr) return;
        sr.color = canPlace ? canColor : cannotColor;
    }
    public void SetNormal()
    {
        if (!sr) return;
        sr.color = baseColor;
    }
}
