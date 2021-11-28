using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Armor,
    Weapon,
    Potion,
    Misc,
    Default
}





public abstract class BaseItem : ScriptableObject
{
   public GameObject prefab;
   public ItemType type;
   public float weight;
   public int GoldValue;
   public string Name;
   [TextArea(15, 20)]
   public string description;


}
