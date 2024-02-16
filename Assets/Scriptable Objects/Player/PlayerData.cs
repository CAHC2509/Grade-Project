using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Base stats")]
    public float life = 100f;
    public float speed = 5f;

    [Space, Header("Shooting stats")]
    public float fireRate = 1f;

    public PlayerData GetCopy()
    {
        PlayerData copy = CreateInstance<PlayerData>();
        copy.life = life;
        copy.speed = speed;
        copy.fireRate = fireRate;

        return copy;
    }
}
