using UnityEngine;

[CreateAssetMenu(fileName = "New ProjectileData", menuName = "Scriptable Objects/Projectile Data")]
public class ProjectileData : ScriptableObject
{
    public float damage = 25f;
    public float speed = 10f;
    public float lifeTime = 5f;
}
