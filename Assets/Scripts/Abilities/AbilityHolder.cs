using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField] private Ability ability;
    [SerializeField] private InputActionReference abilityInput;
    [SerializeField] private UnityEvent onAbilityActivated;
    [SerializeField] private UnityEvent onAbilityReady;

    private float cooldownTime;
    private float activeTime;
    private AbilitySate state = AbilitySate.READY;

    private enum AbilitySate { READY, ACTIVE, COOLDOWN };

    private void Update()
    {
        switch (state)
        {
            case AbilitySate.READY:
                if (abilityInput.action.triggered)
                {
                    ability.Activate(gameObject);
                    state = AbilitySate.ACTIVE;
                    activeTime = ability.activeTime;

                    onAbilityActivated?.Invoke();
                }
                break;

            case AbilitySate.ACTIVE:
                if (activeTime > 0f)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(gameObject);
                    state = AbilitySate.COOLDOWN;
                    cooldownTime = ability.cooldownTime;
                }
                break;

            case AbilitySate.COOLDOWN:
                if (cooldownTime > 0f)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilitySate.READY;
                    onAbilityReady?.Invoke();
                }
                break;
        }
    }
}
