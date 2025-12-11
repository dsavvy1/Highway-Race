using UnityEngine;
using System.Collections;

public class PointGiverSpawner : MonoBehaviour
{
    public PointGiverData pointGiverData;
    public int totalPointGiversToSpawn = 15; // Bumped up from 10 for buffer
    public GameManager gameManager; // assign in inspector

    private int spawnedCount = 0;
    private int activePetrol = 0;
    private Coroutine spawnCoroutine;

    void Start()
    {
        Debug.Log($"[Spawner] Start - totalToSpawn={totalPointGiversToSpawn}");
        StartSpawning();
    }

    void StartSpawning()
    {
        spawnedCount = 0;
        activePetrol = 0;
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnPointGivers());
    }

    IEnumerator SpawnPointGivers()
    {
        for (int i = 0; i < totalPointGiversToSpawn; i++)
        {
            float waitTime = Random.Range(pointGiverData.minimumTimeToSpawn, pointGiverData.maximumTimeToSpawn);
            yield return new WaitForSeconds(waitTime);
            var go = Instantiate(pointGiverData.pointGiverPrefab, transform.position, Quaternion.identity);
            PointGiver pg = go.GetComponent<PointGiver>();
            if (pg == null)
            {
                Debug.LogError("[Spawner] Spawned prefab has no PointGiver component!");
                Destroy(go);
                continue;
            }
            pg.spawner = this;
            pg.gameManager = gameManager;
            spawnedCount++;
            activePetrol++;
            Debug.Log($"[Spawner] Spawned #{spawnedCount}. activePetrol={activePetrol}");
        }
        Debug.Log("[Spawner] Finished spawning batch.");
      
       
    }

   
  
}