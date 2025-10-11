using UnityEngine;

public class SeedSlotSpawner : MonoBehaviour
{
    [Header("Prefab & Points")]
    public SeedInstance seedPrefab;
    public Transform spawnPoint;

    public SeedInstance SpawnSeed()
    {
        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
        var seed = Instantiate(seedPrefab, pos, Quaternion.identity);
        seed.Init(pos, spawnPoint ? spawnPoint : transform);
        return seed;
    }
}
