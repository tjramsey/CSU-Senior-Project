using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    [SerializeField]
    private ItemObject item;

    [SerializeField]
    private float dropChance;

    [SerializeField]
    private int dropAmount;

    public ItemObject MyItem
    {
        get
        {
            return item;
        }
    }
    public float MyDropChance
    {
        get
        {
            return dropChance;
        }
    }
    public int MyDropAmount{
        get
        {
            return dropAmount;
        }
    }
}
