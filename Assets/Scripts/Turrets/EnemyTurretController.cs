using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretController : MonoBehaviour, IDamageable
{
    [Header("Turret components settings")]
    [SerializeField] private TurretData originalTurretData;
    [SerializeField] private TurretStateMachine stateMachine;
    [SerializeField] private Animator turretAnimator;
    [SerializeField] private Transform pointOfFire;

    [HideInInspector]
    public TurretData turretData;

    public Animator animator => turretAnimator;
    public Transform firePoint => pointOfFire;

    private void Start() => turretData = originalTurretData.GetCopy();

    public void LaunchProjectile()
    {
        GameObject instanciatedProjectile = ObjectPool.Instance.GetPooledObject(ObjectPool.ObjectType.GREEN_PROJECTILE);
        instanciatedProjectile.transform.position = firePoint.transform.position;
        instanciatedProjectile?.SetActive(true);

        Projectile projectileController = instanciatedProjectile.GetComponent<Projectile>();
        projectileController?.ApplyBulletSpeed(firePoint.up);
    }

    public bool CheckLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up, turretData.rangeAttackDistance);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag(Tags.Player))
                return true;
        }

        return false;
    }

    public void TakeDamage(float damageAmount)
    {
        turretData.health -= damageAmount;

        if (turretData.health <= 0f)
            stateMachine.SwithState(stateMachine.deathState);
    }
}
