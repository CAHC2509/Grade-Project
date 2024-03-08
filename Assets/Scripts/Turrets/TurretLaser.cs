using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{
    [SerializeField] private TurretData turretData;
    [SerializeField] private LineRenderer laser;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Update()
    {
        startPosition = transform.position;

        endPosition = transform.position;
        endPosition.x += turretData.rangeAttackDistance;

        laser.SetPosition(0, startPosition);
        laser.SetPosition(1, endPosition);
    }
}
