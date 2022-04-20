using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthbarUI;
    public Slider slider;

    public Enemy enemy;



    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
        healthbarUI.SetActive(false);
    }

    void Update()
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthbarUI.SetActive(true);
        }

        if(health <= 0 && !enemy.IsDead)
        {
            enemy.DeathEnemy();
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public float CalculateHealth()
    {
        return health/maxHealth;
    }
}
