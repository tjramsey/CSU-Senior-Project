using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName  =  "New Leg armor Object", menuName = "Inventory System/Items/LegArmor")]

public class LegItem : ArmorItem
{
    public void Awake()
    {
        type = ItemType.Armor;
        type2 = ArmorType.Legs;
    }
}
