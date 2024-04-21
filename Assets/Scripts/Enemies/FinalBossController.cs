using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : EnemyController
{
    [SerializeField] private Transform missilesFirePoint;

    public void LaunchMissile()
    {
        GameObject instanciatedProjectile = ObjectPool.Instance.GetPooledObject(ObjectPool.ObjectType.MISSILE);
        instanciatedProjectile.transform.position = missilesFirePoint.transform.position;
        instanciatedProjectile?.SetActive(true);

        Projectile projectileController = instanciatedProjectile.GetComponent<Projectile>();
        projectileController?.ApplyBulletSpeed(missilesFirePoint.up);
    }
}
