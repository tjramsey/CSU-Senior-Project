using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 4)]
public class HealthPotion : ItemObject, IUseable
{
    [SerializeField]
    private float health;

    public void Use()
    {
        Remove();

        PlayerStats.MyInstance.RestoreHealth(health);
    }
}
