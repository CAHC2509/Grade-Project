using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ComputerController : MonoBehaviour
{
    [SerializeField] private InputActionReference interactionInput;
    [SerializeField] private AudioSource interactionSFX;
    [SerializeField] private UnityEvent onPlayerEnter;
    [SerializeField] private UnityEvent onPlayerExit;
    [SerializeField] private UnityEvent onPlayerInteraction;
    [SerializeField] private UnityEvent onQuestCompleted;

    private float onQuestCompletedDelay = 1f;
    private bool playerIsNear = false;

    public void QuestCompleted() => Invoke(nameof(CompleteQuest), onQuestCompletedDelay);

    private void Update()
    {
        if (playerIsNear)
        {
            if (interactionInput.action.triggered)
            {
                onPlayerInteraction?.Invoke();
                interactionSFX?.Play();
            }
        }
    }

    private void CompleteQuest() => onQuestCompleted?.Invoke();

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
