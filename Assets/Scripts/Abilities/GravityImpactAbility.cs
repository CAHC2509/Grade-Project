using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GravityImpactAbility", menuName = "Scriptable Objects/Abilities/Gravity Impact")]
public class GravityImpactAbility : Ability
{
    public float impactForce;
    public float impactDamage;
    [Range(0f, 10f)] public float explosionRadius;

    public override void Activate(GameObject parent)
    {
        ShockwaveController.Instance.CallShockwave();
        CameraShakeController.Instance.Shake(impactForce * 2f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(parent.transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == parent)
                continue;

            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = (collider.transform.position - parent.transform.position).normalized;
                rb.AddForce(direction * impactForce, ForceMode2D.Impulse);
            }

            IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

            if (damageable is IDamageable)
                damageable.TakeDamage(impactDamage);

            if (collider.gameObject.CompareTag(Tags.EnemyProjectile))
                collider.gameObject.layer = Layers.Player;
        }
    }
}
