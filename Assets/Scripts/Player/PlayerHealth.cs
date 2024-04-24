using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerData originalPlayerData;
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private bool receiveDamage;
    
    [HideInInspector] public PlayerData playerData;

    private void Start() => playerData = originalPlayerData.GetCopy();

    public void TakeDamage(float damageAmount)
    {
        flashEffect.SingleFlash();

        if (receiveDamage)
        {
            playerData.life -= damageAmount;

            if (playerData.life <= 0f)
            {
                Debug.Log("Player death");
            }
        }
    }
}
