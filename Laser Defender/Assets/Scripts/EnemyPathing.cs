using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;
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

    public void SetWaveConfig(WaveConfig _waveConfig)
    {
        waveConfig = _waveConfig;
    }

    private void Move()
    {
        if (waveConfig == null) return;

        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, waveConfig.GetMoveSpeed() * Time.deltaTime);

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
