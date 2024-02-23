using UnityEngine;

public class GameObjectController : MonoBehaviour
{
    public void DeactivateGameObject() => gameObject.SetActive(false);

    public void ResetLocalScale()
    {
        var localScale = transform.localScale;

        transform.localScale = new Vector2(Mathf.Abs(localScale.x), Mathf.Abs(localScale.y));
    }
}
