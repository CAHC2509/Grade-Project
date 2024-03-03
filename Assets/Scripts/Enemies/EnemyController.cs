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

    [Space, Header("Distance attack settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

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

    public void TakeDamage(float damageAmount)
    {
        enemyData.health -= damageAmount;

        healthBar.UpdateHealthBar(enemyData.health);

        if (enemyData.health <= 0f)
            stateMachine.SwithState(stateMachine.deathState);
    }

    public bool IsPlayerInFieldOfVision()
    {
        Vector3 playerDirection = GetPlayerCenter() - transform.position;

        float angleToPlayer = Vector3.Angle(transform.right, playerDirection);

        return angleToPlayer < enemyData.fieldOfViewAngle * enemyData.fieldOfViewTolerancy;
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
        // Draw attack range and vision range
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
