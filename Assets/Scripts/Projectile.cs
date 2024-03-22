using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;
    
    [SerializeField]private GameObject explosionEffect;
    [SerializeField]private GameObject impactFX;

    [SerializeField]private float speed = 70f;
    [SerializeField]private int damage = 50;
    [SerializeField]private float explosionRadius;

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            var direction = _target.position - transform.position;
            var distanceThisFrame = speed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
            }
        
            transform.Translate(direction.normalized * distanceThisFrame,Space.World);
            transform.LookAt(_target);
        }
    }

    /// <summary>
    /// Used to assign the target that the projectile will seek
    /// </summary>
    /// <param name="target"></param>
    public void Seek(Transform target)
    {
        _target = target;
    }

    /// <summary>
    /// Handles what happens when the target gets hit based on the projectile
    /// </summary>
    private void HitTarget()
    {
        if (explosionRadius > 0f)
        {
            Explode();
            var effectInstance = Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effectInstance,3f);
        }
        else
        {
            var effectInstance = Instantiate(impactFX, transform.position, Quaternion.identity);
            Destroy(effectInstance,2f);
            Damage(_target);
        }
        
        Destroy(gameObject);
    }

    /// <summary>
    /// Handles explosion effect
    /// </summary>
    private void Explode()
    {
        var enemyColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var enemyCollider in enemyColliders)
        {
            if (enemyCollider.CompareTag("Enemy"))
            {
                Damage(enemyCollider.transform);
            }
        }
    }
    
    /// <summary>
    /// Handles enemy taking damage
    /// </summary>
    /// <param name="enemy"></param>
    private void Damage(Transform enemy)
    {
        var tempEnemy = enemy.GetComponent<EnemyStats>();
        if (tempEnemy != null)
        {
            tempEnemy.TakeDamage(damage);
        }
    }

    /// <summary>
    /// Used to draw gizmos of the explosion radius
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
