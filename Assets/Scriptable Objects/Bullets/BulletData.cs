using UnityEngine;

[CreateAssetMenu(fileName = "New BulletData", menuName = "Scriptable Objects/Bullet Data")]
public class BulletData : ScriptableObject
{
    public float damage = 25f;
    public float speed = 10f;
    public float lifeTime = 5f;
}
