using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData originalPlayerData;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private InputActionReference verticalInputAction;
    [SerializeField] private InputActionReference horizontalInputAction;

    private PlayerData playerData;
    private Vector2 movementInput;

    private void OnEnable()
    {
        verticalInputAction.action.Enable();
        horizontalInputAction.action.Enable();
    }

    private void Start() => playerData = originalPlayerData.GetCopy();

    private void Update()
    {
        GetPlayerInput();
        FaceDirection();
        Animate();
    }

    private void FixedUpdate() => Move();

    private void GetPlayerInput()
    {
        float verticalInput = verticalInputAction.action.ReadValue<float>();
        float horizontalInput = horizontalInputAction.action.ReadValue<float>();

        movementInput = new Vector2(horizontalInput, verticalInput);
        movementInput.Normalize();
    }

    private void FaceDirection()
    {
        if (Mathf.Abs(movementInput.x) > 0.1f)
        {
            float angle = Mathf.Atan2(0f, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    private void Move() => playerRB.velocity = new Vector2(movementInput.x * playerData.speed, movementInput.y * playerData.speed);

    private void Animate()
    {
        if (movementInput != Vector2.zero)
            playerAnimator.CrossFade(Animations.Player.Run, 0f, 0);
        else
            playerAnimator.CrossFade(Animations.Player.Idle, 0f, 0);
    }

    private void OnDisable()
    {
        verticalInputAction.action.Disable();
        horizontalInputAction.action.Disable();
    }
}
