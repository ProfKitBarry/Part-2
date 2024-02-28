using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void TakeDamage(float value)
    {
        slider.value -= value;
    }

    void SetHealth(float value)
    {
        slider.value = value;
    }
}
