using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType
{
    Helmet,
    Arms,
    Body,
    Legs,
    Feet
}
public class ArmorItem : BaseItem
{
    public int ArmorValue;
    public int Durability;
    public int DurabilityLoss;
    public ArmorType type2;  
}


