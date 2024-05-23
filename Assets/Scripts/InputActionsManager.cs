using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActionsManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionMap;

    private InputActionReference reference;

    public static InputActionsManager Instance;

    private void Awake() => Instance = this;

    private void OnEnable() => EnableInteractions();

    private void OnDisable() => DisableInteractions();

    public void DisableInteractions()
    {
        foreach (InputAction input in inputActionMap)
            input.Disable();
    }

    public void EnableInteractions()
    {
        foreach (InputAction input in inputActionMap)
            input.Enable();
    }

    public void ChangeInputActionActiveState(InputActionReference inputAction, bool state)
    {
        if (state)
            inputAction.action.Enable();
        else
            inputAction.action.Disable();
    }
}
