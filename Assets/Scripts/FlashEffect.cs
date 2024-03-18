using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.15f;

    private Material originalMaterial;

    private void Start() => originalMaterial = spriteRenderer.material;

    public void SingleFlash() => StartCoroutine(SingleFlashCoroutine());

    private IEnumerator SingleFlashCoroutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.material = originalMaterial;
    }
}
