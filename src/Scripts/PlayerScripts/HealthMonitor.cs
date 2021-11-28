using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonitor : MonoBehaviour
{
    public static int HealthValue;
    public int InternalHealth;
    public GameObject Heart01;
    public GameObject Heart02;
    public GameObject Heart03;
    void Start()
    {
        HealthValue = 1;
    }

    void Update()
    {
        InternalHealth = HealthValue;
        if(HealthValue == 1)
        {
            Heart01.SetActive(true);
        }
        if (HealthValue == 2)
        {
            Heart02.SetActive(true);
        }
        if (HealthValue == 3)
        {
            Heart03.SetActive(true);
        }
    }
}
