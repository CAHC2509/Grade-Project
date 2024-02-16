using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private Rigidbody2D bulletRB;

    private void Start() => Destroy(gameObject, bulletData.lifeTime);

    public void ApplyBulletSpeed(Vector2 direction) => bulletRB.velocity = direction.normalized * bulletData.speed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
