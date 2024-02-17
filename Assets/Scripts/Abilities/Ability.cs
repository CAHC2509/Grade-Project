using UnityEngine;

public class Ability : ScriptableObject
{
    public string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) { }
    public virtual void BeginCooldown(GameObject parent) { }
}
