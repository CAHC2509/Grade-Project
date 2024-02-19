using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
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
        Projectile instanciatedBullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, null);
        instanciatedBullet.ApplyBulletSpeed(firePoint.up);
    }
}
