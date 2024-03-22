using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
public class EnemyMovement : MonoBehaviour
{
    private int _wayPointIndex;

    private Transform _target;
    private EnemyStats _enemyStats;
    
    private void Start()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _target = Waypoints.WayPoints[0]; 
    }    
    
    private void Update()
    {
        var direction = _target.position - transform.position;
        transform.Translate(direction.normalized * _enemyStats.movementSpeed * Time.deltaTime,Space.World);
        RotateTowards();
        
        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            FindNextWaypoint();
        }

        _enemyStats.movementSpeed = _enemyStats.startMovementSpeed;
    }
    
    /// <summary>
    /// Used to find what the next waypoint is
    /// </summary>
    private void FindNextWaypoint()
    {
        if (_wayPointIndex >= Waypoints.WayPoints.Length - 1)
        {
            EndPath();
            return;
        }
        
        _wayPointIndex++;
        _target = Waypoints.WayPoints[_wayPointIndex];
    }

    /// <summary>
    /// Used to Handle what happens on the end of the path
    /// </summary>
    private void EndPath()
    {
        if (PlayerStats.Lives > 0)
        {
            PlayerStats.Lives--;
        }

        WaveSpawner.SpawnedEnemies--;
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles enemy rotation towards the next waypoint
    /// </summary>
    private void RotateTowards()
    {
        var direction = _target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(-direction);
        var rotation = Quaternion.Lerp(_enemyStats.partToRotate.rotation,lookRotation,Time.deltaTime * _enemyStats.rotationSpeed).eulerAngles;
        _enemyStats.partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }
}
