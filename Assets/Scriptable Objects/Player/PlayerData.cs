using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Base stats")]
    public float health = 100f;
    public float maxHealth = 100f;
    public float speed = 5f;

    [Space, Header("Shooting stats")]
    [Range(1, 2)] public float fireRate = 1f;

    public PlayerData GetCopy()
    {
        PlayerData copy = CreateInstance<PlayerData>();
        copy.health = health;
        copy.maxHealth = maxHealth;
        copy.speed = speed;
        copy.fireRate = fireRate;

        return copy;
    }
}
