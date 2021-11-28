using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName  =  "New Arm armor Object", menuName = "Inventory System/Items/ArmArmor")]

public class ArmItem : ArmorItem
{
    public void Awake()
    {
        type = ItemType.Armor;
        type2 = ArmorType.Arms;
    }
}
