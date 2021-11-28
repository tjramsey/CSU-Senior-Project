using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName  =  "New Food Object", menuName = "Inventory System/Items/Food")]

public class FoodItem : BaseItem
{
    public int restoreHealthValue;
    public int restoreStaminaValue;
    public int restoryManaValue;

    public void Awake()
    {
        type = ItemType.Food;
    }
}
