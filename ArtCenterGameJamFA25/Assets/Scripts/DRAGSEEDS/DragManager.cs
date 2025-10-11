using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragManager : MonoBehaviour
{
    [Header("Pit Detection")]
    [SerializeField] private LayerMask pitLayer;
    [SerializeField] private float pitSnapRadius = 0.6f;   // 开始吸附的距离
    [SerializeField] private float magnetStrength = 0.35f; // 吸附强度
    [SerializeField] private float placeSnapTime = 0.12f;  // 吸附动画时间

    private Camera cam;
    private SeedInstance draggingSeed;
    private PitSlot hoverPit;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // === 输入检测 ===
        Vector2 screenPos;
        if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        Vector3 worldMouse = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
        worldMouse.z = 0f;

        bool down = (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                    || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

        bool held = (Mouse.current != null && Mouse.current.leftButton.isPressed)
                    || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed);

        bool up = (Mouse.current != null && Mouse.current.leftButton.wasReleasedThisFrame)
                    || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasReleasedThisFrame);

        // === 开始拖拽 ===
        if (down && draggingSeed == null)
        {
            var hit = Physics2D.Raycast(worldMouse, Vector2.zero);
            if (hit.collider != null)
            {
                var slot = hit.collider.GetComponent<SeedSlotSpawner>();
                if (slot != null)
                {
                    draggingSeed = slot.SpawnSeed();
                    draggingSeed.BeginDrag(worldMouse);
                }
            }
        }

        // === 拖拽中（磁吸 + 高亮）===
        if (held && draggingSeed != null)
        {
            PitSlot nearest = FindNearestPit(worldMouse, pitSnapRadius, pitLayer, out float sqrDist);

            if (hoverPit != nearest)
            {
                if (hoverPit != null) hoverPit.SetNormal();
                hoverPit = nearest;
            }
            if (hoverPit != null) hoverPit.SetHighlight(!hoverPit.occupied);

            Vector3 target = worldMouse;
            if (hoverPit != null && !hoverPit.occupied)
            {
                float dist = Mathf.Sqrt(sqrDist);
                float t = Mathf.Clamp01(1f - dist / pitSnapRadius);
                float strength = magnetStrength * t;
                target = Vector3.Lerp(worldMouse, hoverPit.transform.position, strength);
            }
            draggingSeed.UpdateDragging(target);
        }

        // === 松手：放置或飞回 ===
        if (up && draggingSeed != null)
        {
            if (hoverPit != null && !hoverPit.occupied)
            {
                StartCoroutine(SnapThenPlace(draggingSeed, hoverPit, placeSnapTime));
            }
            else
            {
                draggingSeed.EndDrag(false);
            }

            if (hoverPit != null) hoverPit.SetNormal();
            hoverPit = null;
            draggingSeed = null;
        }
    }

    // 找最近的坑
    private PitSlot FindNearestPit(Vector3 pos, float radius, LayerMask layer, out float bestSqr)
    {
        bestSqr = float.MaxValue;
        PitSlot best = null;
        var cols = Physics2D.OverlapCircleAll(pos, radius, layer);
        foreach (var c in cols)
        {
            var pit = c.GetComponent<PitSlot>();
            if (pit == null) continue;
            float d = (pit.transform.position - pos).sqrMagnitude;
            if (d < bestSqr) { bestSqr = d; best = pit; }
        }
        return best;
    }

    // 平滑吸附再放置
    private IEnumerator SnapThenPlace(SeedInstance seed, PitSlot pit, float duration)
    {
        Vector3 start = seed.transform.position;
        Vector3 end = pit.transform.position;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float ease = 1f - Mathf.Cos(t * Mathf.PI * 0.5f); // EaseOutSine
            seed.transform.position = Vector3.Lerp(start, end, ease);
            yield return null;
        }

        pit.TryPlace(seed);
        seed.EndDrag(true);
    }
}
