using UnityEngine;
using System.Collections.Generic;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] List<Transform> waypoints; // Assign waypoints in Inspector
    int waypointIndex = 0;
    waveConfig waveConfig;

    void Start()
    {
        // Set starting position of the Enemy ship to the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            // Save the current waypoint in targetPosition
            var targetPosition = waypoints[waypointIndex].transform.position;
            targetPosition.z = 0f;

            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            // If we reached the target waypoint
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        // If enemy moved to all waypoints
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetWaveConfig(waveConfig config)
    {
        waveConfig = config;
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.GetMoveSpeed();
    }
}
