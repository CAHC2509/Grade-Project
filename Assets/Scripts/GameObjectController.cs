using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    [Header("Camera shake settings")]
    [SerializeField] private float shakeIntensity = 30;
    [SerializeField] private float shakeDuration = 0.5f;

    public void DeactivateGameObject() => gameObject.SetActive(false);

    public void ResetLocalScale()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector2(Mathf.Abs(localScale.x), Mathf.Abs(localScale.y));
    }

    public void ShakeCamera() => CameraShakeController.Instance.Shake(shakeIntensity, shakeDuration);
}
