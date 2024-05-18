using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsManager : MonoBehaviour
{
    [SerializeField] private Transform defaultCheckpoint;

    private Transform lastCheckPoint;
    public static CheckpointsManager Instance;

    private void Awake() => Instance = this;

    private void Start() => lastCheckPoint = defaultCheckpoint;

    public void UpdateLastCheckpoint(Transform newCheckpoint) => lastCheckPoint = newCheckpoint;

    public void TeleportPlayerToLastCheckpoint()
    {
        Transform player = PlayerMovement.Instance.transform;
        player.position = lastCheckPoint.position;
    }
}
