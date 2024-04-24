using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    [SerializeField] private float flashDuration = 0.15f;

    private Material originalMaterial;

    private void Start() => originalMaterial = spriteRenderers[0].material;

    public void SingleFlash() => StartCoroutine(SingleFlashCoroutine());

    private IEnumerator SingleFlashCoroutine()
    {
        foreach (SpriteRenderer renderer in spriteRenderers)
            renderer.material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        foreach (SpriteRenderer renderer in spriteRenderers)
            renderer.material = originalMaterial;
    }
}
