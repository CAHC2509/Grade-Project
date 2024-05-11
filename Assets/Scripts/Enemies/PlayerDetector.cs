using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    [Header("Detection components")]
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private CircleCollider2D circleCollider;

    [Space, Header("Custom properties")]
    [SerializeField] private float customDetectionRange;
    [SerializeField] private bool useCustomProperties;


    private void Start() => circleCollider.radius = useCustomProperties ? customDetectionRange : enemyData.detectionRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
            stateMachine.ChasePlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, useCustomProperties ? customDetectionRange : enemyData.detectionRange);
    }
}
