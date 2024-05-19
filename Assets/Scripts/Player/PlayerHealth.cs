using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerData originalPlayerData;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private GameObject playerDeathPrefab;
    [SerializeField] private Material playerDefautMaterial;
    [SerializeField] private UnityEvent onPlayerDeath;
    [SerializeField] private bool receiveDamage;

    private Coroutine regenerationCoroutine;

    [HideInInspector] public PlayerData playerData;

    private void Start()
    {
        playerData = originalPlayerData.GetCopy();
        healthBar.InitializeHealthBar(playerData.maxHealth, playerData.health);
    }

    public void TakeDamage(float damageAmount)
    {
        flashEffect.SingleFlash();
        CameraShakeController.Instance.Shake(35f, 0.35f);

        if (receiveDamage)
        {
            playerData.health -= damageAmount;
            healthBar.UpdateHealthBar(playerData.health);

            if (playerData.health <= 0f)
            {
                PlayerDeath();
            }
            else
            {
                if (regenerationCoroutine != null)
                    StopCoroutine(regenerationCoroutine);

                regenerationCoroutine = StartCoroutine(RegenerateHealth());
            }
        }
    }

    private void PlayerDeath()
    {
        Instantiate(playerDeathPrefab, transform.position, transform.rotation, null);
        onPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().material = playerDefautMaterial;
        playerData.health = playerData.maxHealth;
        healthBar.UpdateHealthBar(playerData.health);
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(playerData.regenerationDelay);

        while (playerData.health < playerData.maxHealth)
        {
            playerData.health += playerData.regenerationRate * Time.deltaTime;
            playerData.health = Mathf.Min(playerData.health, playerData.maxHealth);

            healthBar.UpdateHealthBar(playerData.health);

            yield return null;
        }
    }
}
