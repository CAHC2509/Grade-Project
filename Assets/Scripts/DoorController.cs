using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public enum DoorState { LOCKED, UNLOCKED }

    [SerializeField] private Animator doorAnimator;
    [SerializeField] private DoorState doorState;

    private void Start() => ChangeDoorLockedState(doorState is DoorState.UNLOCKED);

    public void ChangeDoorLockedState(bool newState)
    {
        doorState = newState ? DoorState.UNLOCKED : DoorState.LOCKED;

        if (doorState is DoorState.UNLOCKED)
            doorAnimator.CrossFade(Animations.Door.Unlock, 0f);
        else
            doorAnimator.CrossFade(Animations.Door.Lock, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorState is DoorState.UNLOCKED)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
                doorAnimator.CrossFade(Animations.Door.Open, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (doorState is DoorState.UNLOCKED)
        {
            if (collision.gameObject.CompareTag(Tags.Player))
                doorAnimator.CrossFade(Animations.Door.Close, 0f);
        }
    }
}
