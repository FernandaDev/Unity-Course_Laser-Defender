using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float waypointsThreshold = 0.5f;
    int waypointIndex = 0;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if ((transform.position - targetPosition).sqrMagnitude < waypointsThreshold)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
