using UnityEngine;

[CreateAssetMenu(fileName = "New DashAbility", menuName = "Scriptable Objects/Abilities/Dash")]
public class Dash : Ability
{
    public float dashSpeed;

    private PlayerMovement playerMovement;
    private float defaultSpeed;

    public override void Activate(GameObject parent)
    {
        playerMovement = parent.GetComponent<PlayerMovement>();
        defaultSpeed = playerMovement.playerData.speed;

        playerMovement.playerData.speed = dashSpeed;
    }

    public override void BeginCooldown(GameObject parent) => playerMovement.playerData.speed = defaultSpeed;
}
