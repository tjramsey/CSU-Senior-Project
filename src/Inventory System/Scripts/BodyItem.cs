using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName  =  "New Body armor Object", menuName = "Inventory System/Items/BodyArmor")]

public class BodyItem : ArmorItem
{
    public void Awake()
    {
        type = ItemType.Armor;
        type2 = ArmorType.Body;
    }
}
