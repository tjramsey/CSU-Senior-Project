using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  =  "New Feet armor Object", menuName = "Inventory System/Items/FeetArmor")]

public class FeetItem : ArmorItem
{
    public void Awake()
    {
        type = ItemType.Armor;
        type2 = ArmorType.Feet;
    }
}
