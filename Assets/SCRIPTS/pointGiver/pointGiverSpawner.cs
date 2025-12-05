using UnityEngine;
using System.Collections;

public class PointGiverSpawner : MonoBehaviour
{
    [Header("Scriptable Object Reference")]
    public PointGiverData pointGiverData;

    [Header("Spawn Settings")]
    public int totalPointGiversToSpawn = 10;

    private int pointGiversSpawned = 0;
    private int totalPointsCollected = 0;

    void Start()
    {
        _ = StartCoroutine(SpawnPointGivers());
    }

    IEnumerator SpawnPointGivers()
    {
        // Loop to spawn 10 Point-Givers
        for (int i = 0; i < totalPointGiversToSpawn; i++)
        {
            // Random wait time between min and max
            float waitTime = Random.Range(
                pointGiverData.minimumTimeToSpawn,
                pointGiverData.maximumTimeToSpawn
            );

            yield return new WaitForSeconds(waitTime);

            // Spawn at empty object's position
            Instantiate(
                pointGiverData.pointGiverPrefab,
                transform.position,
                Quaternion.identity
            );

            pointGiversSpawned++;
            Debug.Log($"Point-Giver {pointGiversSpawned} spawned!");



        }

        Debug.Log("All Point-Givers spawned!");
    }
}
