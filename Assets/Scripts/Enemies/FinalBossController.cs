using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FinalBossController : EnemyController
{
    [SerializeField] private Transform missilesFirePoint;
    [SerializeField] private AudioSource missileSFX;

    public void LaunchMissile()
    {
        GameObject instanciatedProjectile = ObjectPool.Instance.GetPooledObject(ObjectPool.ObjectType.MISSILE);
        instanciatedProjectile.transform.position = missilesFirePoint.transform.position;
        instanciatedProjectile?.SetActive(true);

        Projectile projectileController = instanciatedProjectile.GetComponent<Projectile>();
        projectileController?.ApplyBulletSpeed(missilesFirePoint.up);

        missileSFX?.Play();
    }

    private void OnDisable()
    {
        GameObject flowchartObject = GameObject.FindGameObjectWithTag(Tags.GameFlowchart);
        Flowchart gameFlowchart = flowchartObject.GetComponent<Flowchart>();
        gameFlowchart.SendFungusMessage("FINALBOSSDEATH");
    }
}
