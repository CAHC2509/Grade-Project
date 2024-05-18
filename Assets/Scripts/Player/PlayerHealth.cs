using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerData originalPlayerData;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private bool receiveDamage;

    private Coroutine regenerationCoroutine;
    private bool isRegenerating;

    [HideInInspector] public PlayerData playerData;

    private void Start()
    {
        playerData = originalPlayerData.GetCopy();
        healthBar.InitializeHealthBar(playerData.maxHealth, playerData.health);
    }

    public void TakeDamage(float damageAmount)
    {
        flashEffect.SingleFlash();

        if (receiveDamage)
        {
            playerData.health -= damageAmount;
            healthBar.UpdateHealthBar(playerData.health);

            if (playerData.health <= 0f)
            {
                Debug.Log("Player death");
            }
            else
            {
                if (regenerationCoroutine != null)
                    StopCoroutine(regenerationCoroutine);

                regenerationCoroutine = StartCoroutine(RegenerateHealth());
            }
        }
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(playerData.regenerationDelay);

        isRegenerating = true;

        while (playerData.health < playerData.maxHealth)
        {
            playerData.health += playerData.regenerationRate * Time.deltaTime;
            playerData.health = Mathf.Min(playerData.health, playerData.maxHealth);

            healthBar.UpdateHealthBar(playerData.health);

            yield return null;
        }

        isRegenerating = false;
    }
}
