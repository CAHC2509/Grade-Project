using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileData bulletData;
    [SerializeField] private Rigidbody2D bulletRB;

    private void Start() => Invoke(nameof(DisableProjectile), bulletData.lifeTime);

    private void DisableProjectile() => gameObject.SetActive(false);

    public void ApplyBulletSpeed(Vector2 direction) => bulletRB.velocity = direction.normalized * bulletData.speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable is IDamageable)
            damageable.TakeDamage(bulletData.damage);

        gameObject.SetActive(false);
    }
}
