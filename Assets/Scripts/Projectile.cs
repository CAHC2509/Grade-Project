using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData bulletData;
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private BulletType bulletType;

    private enum BulletType { BLUE, GREEN, MISSILE }
    private Vector2 bulletDiection;

    private void OnEnable()
    {
        if (CompareTag(Tags.EnemyProjectile))
            gameObject.layer = Layers.EnemyProjectiles;

        Invoke(nameof(DisableProjectile), bulletData.lifeTime);
    }

    public void ApplyBulletSpeed(Vector2 direction)
    {
        bulletDiection = direction;
        bulletRB.velocity = direction.normalized * bulletData.speed;

        float localScaleX = Mathf.Abs(transform.localScale.x);
        bool isGoingRight = bulletDiection.x > 0f;
        localScaleX = isGoingRight ? localScaleX : -localScaleX;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void DisableProjectile() => gameObject.SetActive(false);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable is IDamageable)
            damageable.TakeDamage(bulletData.damage);

        ObjectPool.ObjectType objectType = ObjectPool.ObjectType.NONE;

        switch (bulletType)
        {
            case BulletType.BLUE:
                objectType = ObjectPool.ObjectType.BLUE_PARTICLES;
                break;

            case BulletType.GREEN:
                objectType = ObjectPool.ObjectType.GREEN_PARTICLES;
                break;

            case BulletType.MISSILE:
                objectType = ObjectPool.ObjectType.MISSILE_PARTICLES;
                break;
        }

        GameObject pooledParticle = ObjectPool.Instance.GetPooledObject(objectType);
        pooledParticle.transform.position = transform.position;

        var particleLocalScale = pooledParticle.transform.localScale;

        if (bulletDiection.x < 0f)
            pooledParticle.transform.localScale = new Vector2(-particleLocalScale.x, particleLocalScale.y);

        pooledParticle.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnDisable() => CancelInvoke(nameof(DisableProjectile));
}
