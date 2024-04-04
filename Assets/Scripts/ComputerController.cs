using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ComputerController : MonoBehaviour
{
    [SerializeField] private InputActionMap inputActionMap;
    [SerializeField] private InputActionReference interactionInput;
    [SerializeField] private UnityEvent onPlayerEnter;
    [SerializeField] private UnityEvent onPlayerExit;
    [SerializeField] private UnityEvent onPlayerInteraction;
    [SerializeField] private UnityEvent onQuestCompleted;

    private bool playerIsNear = false;

    public void QuestCompleted() => onQuestCompleted?.Invoke();

    private void Update()
    {
        if (playerIsNear)
        {
            if (interactionInput.action.triggered)
                onPlayerInteraction?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIsNear = collision.gameObject.CompareTag(Tags.Player);

        if (playerIsNear)
            onPlayerEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsNear = !collision.gameObject.CompareTag(Tags.Player);

        if (!playerIsNear)
            onPlayerExit?.Invoke();
    }
}
