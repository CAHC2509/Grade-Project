using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private bool disableAfterTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool collidedWithPlayer = collision.gameObject.CompareTag(Tags.Player);

        if (collidedWithPlayer)
            onTriggerEnter?.Invoke();

        gameObject.SetActive(!disableAfterTrigger);
    }
}
