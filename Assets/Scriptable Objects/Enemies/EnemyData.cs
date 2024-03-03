using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Scriptable Objects/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum AttackType
    {
        UNDEFINED,
        DISTANCE,
        MELEE,
        HYBRID
    }

    [Header("Base stats")]
    public AttackType attackType;
    public float maxHealth = 100f;
    public float health = 100f;
    public float speed = 5f;

    [Space, Header("Attack stats")]
    public float rangeAttackDistance = 5f;
    public float meleeAttackDistance = 1.5f;
    public float fieldOfViewAngle = 25f;
    public float fieldOfViewTolerancy = 0.35f;

    public EnemyData GetCopy()
    {
        EnemyData copy = CreateInstance<EnemyData>();
        copy.attackType = attackType;
        copy.maxHealth = maxHealth;
        copy.health = health;
        copy.speed = speed;
        copy.rangeAttackDistance = rangeAttackDistance;
        copy.meleeAttackDistance = meleeAttackDistance;
        copy.fieldOfViewAngle = fieldOfViewAngle;
        copy.fieldOfViewTolerancy = fieldOfViewTolerancy;

        return copy;
    }
}
