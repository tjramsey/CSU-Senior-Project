using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
        public TextMeshProUGUI valueText;

    

    public void SetHealth(float health)
    {
        
        if(health < slider.maxValue)
            slider.value = health;
        else
            slider.value = slider.maxValue;

        valueText.text = slider.value.ToString() + " / " + slider.maxValue;

        

    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        valueText.text = slider.value.ToString() + " / " + slider.maxValue;

    }
}
