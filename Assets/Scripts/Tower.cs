using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header ("General")]
    [SerializeField]private Transform target;
    [SerializeField]private EnemyStats enemyScript;
    [SerializeField]private float range = 15f;
    
    [Header("Use Bullets")]
    [SerializeField]private float fireRate = 1f;
    [SerializeField]private float fireCountdown = 0f;
    [SerializeField]private GameObject arrowPrefab;

    [Header("Use Magic Beam")] 
    [SerializeField]private bool useBeam;
    [SerializeField]private int damageOverTime = 30;
    [SerializeField]private float slowAmount = 0.5f;
    [SerializeField]private LineRenderer lineRenderer;
    [SerializeField]private ParticleSystem impactFX;

    [Header ("Unity Setup Fields")]
    [SerializeField]private string enemyTag = "Enemy";
    [SerializeField]private Transform partToRotate;
    [SerializeField]private float rotationSpeed = 10f;
    [SerializeField]private Transform firePoint;
    [SerializeField]private AudioSource shootSFX;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTarget),0f,0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            if (!useBeam) return;
            if (!lineRenderer.enabled) return;
            impactFX.Stop();
            lineRenderer.enabled = false;
            return;
        }

        LockOnTarget();

        if (useBeam)
        {
            ShootBeam();
        }

        if (arrowPrefab == null)
        {
            return;
        }
        
        if (fireCountdown <= 0f)
        {
            ShootProjectile();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    /// <summary>
    /// Used to lock in on the nearest target
    /// </summary>
    private void LockOnTarget()
    {
        var direction = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(direction);
        var rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime * rotationSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }
    
    /// <summary>
    /// Used to shoot a projectile
    /// </summary>
    private void ShootProjectile()
    {
        shootSFX.Play();
        var arrowObject = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        var projectile = arrowObject.GetComponent<Projectile>();

        if (projectile != null)
        {
            projectile.Seek(target);
        }
    }

    /// <summary>
    /// Used to shoot a beam instead of a projectile(used on mage towers)
    /// </summary>
    private void ShootBeam()
    {
        enemyScript.TakeDamage(damageOverTime * Time.deltaTime);
        enemyScript.Slow(slowAmount);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            if (!impactFX.isPlaying)
            {
                impactFX.Play();
            }
            
        }

        var firePointPosition = firePoint.position;
        var targetPosition = target.position;
        
        lineRenderer.SetPosition(0, firePointPosition);
        lineRenderer.SetPosition(1,targetPosition);

        var direction = firePointPosition - targetPosition;
        impactFX.transform.position = targetPosition + direction.normalized;
        impactFX.transform.rotation = Quaternion.LookRotation(-direction);
    }

    /// <summary>
    /// Used to update the current target of the tower
    /// </summary>
    private void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        var shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (var enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (!(distanceToEnemy < shortestDistance)) continue;
            shortestDistance = distanceToEnemy;
            nearestEnemy = enemy;
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            enemyScript = nearestEnemy.GetComponent<EnemyStats>();
        }
        else
        {
            target = null;
        }
    }

    /// <summary>
    /// Used to test the range of the selected tower
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
