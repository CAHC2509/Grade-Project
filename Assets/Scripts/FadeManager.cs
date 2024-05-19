using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    private Coroutine transitionCoroutine;

    public void FadeIn(float transitionTime = 0.75f)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(TransitionAlpha(1f, transitionTime));
    }

    public void FadeOut(float transitionTime = 0.75f)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine = StartCoroutine(TransitionAlpha(0f, transitionTime));
    }

    private IEnumerator TransitionAlpha(float targetAlpha, float transitionTime)
    {
        float startAlpha = targetImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionTime);
            Color newColor = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, newAlpha);
            targetImage.color = newColor;

            yield return null;
        }

        targetImage.color = new Color(targetImage.color.r, targetImage.color.g, targetImage.color.b, targetAlpha);
    }
}
