using UnityEngine;

public class PitSlot : MonoBehaviour
{
    public bool occupied { get; private set; }
    private SeedInstance currentSeed;

    [Header("Placement")]
    public Transform anchor;                 // 拖到每个坑下的 SeedAnchor（可选）
    public Vector3 localOffset = Vector3.zero; // 在锚点坐标系下的微调

    // 可选：可视反馈
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

        // 1) 先把种子设为坑(或锚点)的子物体，但“保留世界缩放/位置/旋转”
        Transform parentT = anchor ? anchor : transform;
        seed.transform.SetParent(parentT, /*worldPositionStays:*/ true);

        // 2) 再把它移动到锚点的本地偏移位置（不改缩放）
        Vector3 worldTarget = parentT.TransformPoint(localOffset);
        seed.transform.position = worldTarget;
        seed.transform.rotation = Quaternion.identity; // 需要的话置零旋转
        // 注意：不要改 seed.transform.localScale —— 保留原来的可见大小

        // （可选）固定后禁用刚体/碰撞
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
