using UnityEngine;

[CreateAssetMenu(fileName = "PointGiverData", menuName = "Game/Point Giver Data")]
public class PointGiverData : ScriptableObject
{
    [Header("Spawning Settings")]
    public GameObject pointGiverPrefab;
    public float minimumTimeToSpawn = 0.5f;
    public float maximumTimeToSpawn = 7f;
}
