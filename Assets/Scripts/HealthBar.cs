using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Health playerHealth;

    private void Start()
    {
        slider.maxValue = playerHealth.maxHealth;
        slider.value = playerHealth.currentHealth;
    }

    private void Update()
    {
        slider.value = playerHealth.currentHealth;
    }
}
