using UnityEngine;

public class PitSlot : MonoBehaviour
{
    public bool occupied { get; private set; }
    private SeedInstance currentSeed;

    [Header("Placement")]
    public Transform anchor;                 // �ϵ�ÿ�����µ� SeedAnchor����ѡ��
    public Vector3 localOffset = Vector3.zero; // ��ê������ϵ�µ�΢��

    // ��ѡ�����ӷ���
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

        // 1) �Ȱ�������Ϊ��(��ê��)�������壬����������������/λ��/��ת��
        Transform parentT = anchor ? anchor : transform;
        seed.transform.SetParent(parentT, /*worldPositionStays:*/ true);

        // 2) �ٰ����ƶ���ê��ı���ƫ��λ�ã��������ţ�
        Vector3 worldTarget = parentT.TransformPoint(localOffset);
        seed.transform.position = worldTarget;
        seed.transform.rotation = Quaternion.identity; // ��Ҫ�Ļ�������ת
        // ע�⣺��Ҫ�� seed.transform.localScale ���� ����ԭ���Ŀɼ���С

        // ����ѡ���̶�����ø���/��ײ
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
