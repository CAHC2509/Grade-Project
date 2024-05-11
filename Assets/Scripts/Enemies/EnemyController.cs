using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Components")]
    [SerializeField] private EnemyData originalEnemyData;
    [SerializeField] private NavMeshAgent meshAgent;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private FlashEffect flashEffect;

    [Space, Header("Distance attack settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Space, Header("Debug settings")]
    [SerializeField] private bool showGizmos;

    [HideInInspector]
    public EnemyData enemyData;
    public NavMeshAgent navMeshAgent => meshAgent;
    public Animator animator => enemyAnimator;

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = originalEnemyData.speed;

        enemyData = originalEnemyData.GetCopy();

        healthBar.InitializeHealthBar(enemyData.maxHealth, enemyData.health);
    }

    private void Update()
    {
        if (enemyData.health <= 0f)
            stateMachine.SwithState(stateMachine.deathState);
    }

    public void CheckFacing()
    {
        Vector3 directionToPlayer = PlayerMovement.Instance.transform.position - transform.position;
        bool playerIsLeftSide = directionToPlayer.x < 0f;

        transform.localScale = playerIsLeftSide ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    public void LaunchProjectile()
    {
        GameObject instanciatedProjectile = ObjectPool.Instance.GetPooledObject(ObjectPool.ObjectType.GREEN_PROJECTILE);
        instanciatedProjectile.transform.position = firePoint.transform.position;
        instanciatedProjectile?.SetActive(true);

        Projectile projectileController = instanciatedProjectile.GetComponent<Projectile>();
        projectileController?.ApplyBulletSpeed(firePoint.up);
    }

    public void LaunchLaser()
    {
        GameObject instanciatedProjectile = ObjectPool.Instance.GetPooledObject(ObjectPool.ObjectType.GREEN_LASER);
        instanciatedProjectile.transform.position = firePoint.transform.position;
        instanciatedProjectile?.SetActive(true);

        Projectile projectileController = instanciatedProjectile.GetComponent<Projectile>();
        projectileController?.ApplyBulletSpeed(firePoint.up);
    }

    public void ProjectilesExplosion()
    {
        int numProjectiles = enemyData.projectilesAmount;
        float explosionRadius = 0.5f;

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = (360f / numProjectiles) * i;
            float radians = Mathf.Deg2Rad * angle;

            float x = explosionRadius * Mathf.Cos(radians);
            float y = explosionRadius * Mathf.Sin(radians);

            GameObject instantiatedProjectile = ObjectPool.Instance.GetPooledObject(ObjectPool.ObjectType.GREEN_PROJECTILE);
            instantiatedProjectile.transform.position = firePoint.transform.position + new Vector3(x, y, 0f);
            instantiatedProjectile?.SetActive(true);

            Projectile projectileController = instantiatedProjectile.GetComponent<Projectile>();
            projectileController?.ApplyBulletSpeed(new Vector2(x, y).normalized);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        flashEffect.SingleFlash();
        CameraShakeController.Instance.Shake(15f, 0.25f);

        enemyData.health -= damageAmount;

        healthBar.UpdateHealthBar(enemyData.health);
    }

    public bool IsPlayerInFieldOfVision()
    {
        Vector3 playerDirection = GetPlayerCenter() - transform.position;

        float angleToPlayer = Vector3.Angle(firePoint.up, playerDirection);

        return angleToPlayer < enemyData.fieldOfViewAngle * enemyData.fieldOfViewTolerancy;
    }

    public bool LineOfSightClear()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up, enemyData.rangeAttackDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(Tags.Enviroment))
                return false;
        }

        return true;
    }

    private Vector3 GetPlayerCenter()
    {
        Transform player = PlayerMovement.Instance.transform;
        Collider2D playerCollider = player.GetComponent<Collider2D>();

        Vector2 localCenter = playerCollider.bounds.center;

        return new Vector3(localCenter.x, localCenter.y, transform.position.z);
    }


    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, enemyData.rangeAttackDistance);

            float halfFOV = enemyData.fieldOfViewAngle * 0.5f;
            Vector3 direction1 = Quaternion.AngleAxis(halfFOV, Vector3.forward) * firePoint.up;
            Vector3 direction2 = Quaternion.AngleAxis(-halfFOV, Vector3.forward) * firePoint.up;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (direction1 * enemyData.rangeAttackDistance));
            Gizmos.DrawLine(transform.position, transform.position + (direction2 * enemyData.rangeAttackDistance));
        }
    }
}
