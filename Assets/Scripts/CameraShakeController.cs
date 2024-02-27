using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float shakeIntensityDivider = 5f;

    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    public static CameraShakeController Instance { get; private set; }

    private void Awake() => Instance = this;

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin multiChannelPerlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - (shakeTimer / shakeTimerTotal));
        }
    }

    public void Shake(float intensity, float time = 0.5f)
    {
        CinemachineBasicMultiChannelPerlin multiChannelPerlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannelPerlin.m_AmplitudeGain = intensity / shakeIntensityDivider;

        startingIntensity = intensity / shakeIntensityDivider;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
}
