using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRB;
    [SerializeField] private float bulletSpeed = 10f;

    public void ApplyBulletSpeed(Vector2 direction) => bulletRB.velocity = direction.normalized * bulletSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
