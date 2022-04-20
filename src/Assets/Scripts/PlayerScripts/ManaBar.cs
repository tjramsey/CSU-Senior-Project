using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;


    public void SetMana(float mana)
    {
        
        slider.value = mana;
        valueText.text = slider.value.ToString() + " / " + slider.maxValue;
    }

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
        valueText.text = slider.value.ToString() + " / " + slider.maxValue;
    }
}
