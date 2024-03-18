using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
    [SerializeField] private GameObject shockwavePanel;
    [SerializeField] private GravityImpactAbility impactAbility;
    [SerializeField] private Material shockwaveMaterial;
    [SerializeField] private float distanceDivider = 12.5f;
    [SerializeField] float vanishDuration = 0.5f;

    private Coroutine shockwaveCoroutine;
    private int waveDistanceFromCenter = Shader.PropertyToID("_WaveDistance");
    private int materialOpacity = Shader.PropertyToID("_Opacity");

    public static ShockwaveController Instance;

    private void Awake() => Instance = this;

    public void CallShockwave()
    {
        if (shockwaveCoroutine != null)
            StopCoroutine(shockwaveCoroutine);

        shockwaveCoroutine = StartCoroutine(ShockwaveCoroutine());
    }

    private IEnumerator ShockwaveCoroutine()
    {
        shockwaveMaterial.SetFloat(waveDistanceFromCenter, -0.1f);
        shockwaveMaterial.SetFloat(materialOpacity, 1f);
        shockwavePanel.SetActive(true);

        float lerpedAmount = 0f;
        float elapsedTime = 0f;
        float radius = impactAbility.explosionRadius / distanceDivider;

        while (elapsedTime < impactAbility.activeTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(-0.1f, radius, (elapsedTime / impactAbility.activeTime));
            shockwaveMaterial.SetFloat(waveDistanceFromCenter, lerpedAmount);

            yield return null;
        }

        lerpedAmount = 0f;
        elapsedTime = 0f;

        while (elapsedTime < vanishDuration)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(1f, 0f, (elapsedTime / vanishDuration));
            shockwaveMaterial.SetFloat(materialOpacity, lerpedAmount);

            yield return null;
        }

        shockwavePanel.SetActive(false);
    }
}
