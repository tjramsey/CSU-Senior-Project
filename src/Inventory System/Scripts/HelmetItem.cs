using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  =  "New Helmet Object", menuName = "Inventory System/Items/Helmet")]

public class HelmetItem : ArmorItem
{
    public void Awake()
    {
        type = ItemType.Armor;
        type2 = ArmorType.Helmet;
    }
}
