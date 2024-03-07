using UnityEngine;

[CreateAssetMenu(fileName = "New TurretData", menuName = "Scriptable Objects/Turret Data")]
public class TurretData : ScriptableObject
{
    [Header("Base stats")]
    public float maxHealth = 35f;
    public float health = 35f;

    [Space, Header("Attack stats")]
    public float rangeAttackDistance = 7.5f;

    public TurretData GetCopy()
    {
        TurretData copy = CreateInstance<TurretData>();
        copy.maxHealth = maxHealth;
        copy.health = health;
        copy.rangeAttackDistance = rangeAttackDistance;

        return copy;
    }
}
