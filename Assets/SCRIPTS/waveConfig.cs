using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "waveConfig", menuName = " enemyWaveConfig")]
public class waveConfig : ScriptableObject
{
   
    [SerializeField] GameObject enemyPrefab;

    [SerializeField] GameObject pathPrefab;

    [SerializeField] float timeBetweenSpawns = 0.5f;

    [SerializeField] float spawnRandomFactor = 0.3f;

    [SerializeField] int numberOfEnemies = 5;

    [SerializeField] float enemyMoveSpeed = 2f;

    public GameObject GetEnemyPrefab() 
    {
        return enemyPrefab;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }

    public float GetMoveSpeed()
    {
        return enemyMoveSpeed; // Assuming 'enemyMoveSpeed' is the field you meant
    }
    
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform)
        {
            waveWaypoints.Add(waypoint);
        }
        return waveWaypoints;
    }
}
