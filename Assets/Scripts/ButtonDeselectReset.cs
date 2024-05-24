using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDeselectReset : MonoBehaviour
{
    private static EventSystem eventSystem;

    void Start()
    {
        if (eventSystem != null)
            return;

        GameObject myEventSystem = GameObject.Find("EventSystem");
        eventSystem = myEventSystem.GetComponent<EventSystem>();

        if (TryGetComponent<Button>(out Button button))
            button.onClick.AddListener(ResetEventSystem);

        if (TryGetComponent<Slider>(out Slider slider))
            slider.onValueChanged.AddListener(ResetEventSystemSlider);
    }

    public void ResetEventSystem() => eventSystem.SetSelectedGameObject(null);

    public void ResetEventSystemSlider(float value) => eventSystem.SetSelectedGameObject(null);
}
