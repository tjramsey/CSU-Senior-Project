using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName  =  "Food", menuName = "Items/Food", order = 3)]
public class Food : ItemObject, IUseable
{
    [SerializeField]
    private float health;

    public void Use()
    {
        Remove();

        PlayerStats.MyInstance.RestoreHealth(health);
    }
}

