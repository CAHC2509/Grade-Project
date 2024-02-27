using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healtBar;

    public void InitializeHealthBar(float maxHealth, float currentHealt)
    {
        healtBar.maxValue = maxHealth;
        healtBar.value = currentHealt;
    }

    public void UpdateHealthBar(float newValue) => healtBar.value = newValue;
}
