using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerData originalPlayerData;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private bool receiveDamage;
    
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
        }
    }
}
