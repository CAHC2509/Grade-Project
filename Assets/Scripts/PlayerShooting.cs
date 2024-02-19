using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InputActionReference shootInputAction;

    private void OnEnable() => shootInputAction.action.Enable();

    private void Start() => SetFireRate();

    private void Update()
    {
        if (shootInputAction.action.IsPressed())
            playerAnimator.CrossFade(Animations.Player.Shoot, 0f, 0);
    }

    public void Shoot()
    {
        Projectile instanciatedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, null);
        instanciatedProjectile.ApplyBulletSpeed(firePoint.right);
    }

    public void SetFireRate() => playerAnimator.SetFloat(Animations.Player.Parameters.ShootingSpeedMultiplier, playerData.fireRate);

    private void OnDisable() => shootInputAction.action.Disable();
}
