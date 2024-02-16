using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Scriptable Objects/Player Data")]
public class PlayerData : ScriptableObject
{
    public float life = 100f;
    public float speed = 5f;

    public PlayerData GetCopy()
    {
        PlayerData copy = CreateInstance<PlayerData>();
        copy.life = life;
        copy.speed = speed;

        return copy;
    }
}
