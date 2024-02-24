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

    [Space, Header("Distance attack settings")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    public EnemyData enemyData;
    public NavMeshAgent navMeshAgent => meshAgent;
    public Animator animator => enemyAnimator;

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.speed = originalEnemyData.speed;

        enemyData = originalEnemyData.GetCopy();
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
        enemyData.life -= damageAmount;

        if (enemyData.life <= 0f)
        {
            Debug.Log("Enemy Killed!");
        }
    }

    public bool IsPlayerInFieldOfVision()
    {
        Vector3 center = GetPlayerCenter();
        Vector2 directionToPlayer = center - transform.position;

        float angleToPlayer = Vector2.Angle(firePoint.up, directionToPlayer);
        float crossProduct = Vector3.Cross(firePoint.up, directionToPlayer).z;

        if (crossProduct < 0)
            angleToPlayer = 360 - angleToPlayer;

        if (angleToPlayer <= enemyData.fieldOfViewAngle * 0.5f && directionToPlayer.magnitude <= enemyData.rangeAttackDistance)
            return true;

        return false;
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
