using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;

    public void SetStamina(float stamina)
    {
        
        slider.value = stamina;
        valueText.text = slider.value.ToString() + " / " + slider.maxValue;

    }

    public void SetMaxStamina(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
        valueText.text = slider.value.ToString() + " / " + slider.maxValue;

    }
}
