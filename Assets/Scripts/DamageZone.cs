using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float damageInflicted;
    [SerializeField] private float pushingForce;
    [SerializeField] private ForceMode2D forceMode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            Rigidbody2D rigidbody2D = collision.GetComponent<Rigidbody2D>();

            if (rigidbody2D != null)
            {
                Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
                rigidbody2D.AddForce(pushDirection * pushingForce, forceMode);
            }
        }
    }
}
