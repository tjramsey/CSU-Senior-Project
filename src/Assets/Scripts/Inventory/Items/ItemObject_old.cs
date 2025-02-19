﻿// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public enum ItemType
// {
//     Food,
//     Helmet,
//     ChestPiece,
//     LegArmor,
//     Shield,
//     FeetArmor,
//     Weapon,
//     Potion,
//     Misc,
//     Default
// }

// public enum Mat
// {
//     Wood,
//     Leather,
//     Iron,
//     Steel,
//     Silver
// }

// public enum Attributes
// {
//     Agility,
//     Intellect,
//     Strength,
//     Charisma,
//     Endurance,
//     Damage,
//     Armor
// }
// [CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
// public class ItemObject : ScriptableObject
// {
//     public string Name;
//     public Sprite uiDisplay;
//     public ItemType type;
//     public bool stackable;
//     public int GoldValue;
//     public float weight;
//     [TextArea(15, 20)]
//     public string description;


//     public Item data = new Item();

//    public Item CreateItem()
//    {
//        Item newItem = new Item(this);
//        return newItem;
//    }
// }

// [System.Serializable]
// public class Item 
// {
//     public string Name;
//     public int Id = -1;
//     public ItemBuff[] buffs;
//     public Item()
//     {
//         Name = "";
//         Id =-1;
//     }
//     public Item(ItemObject item)
//     {
//         Name = item.name;
//         Id = item.data.Id;
//         Debug.Log(item.data.buffs.Length);


//         buffs = new ItemBuff[item.data.buffs.Length];
//         for (int i = 0; i < buffs.Length; i++)
//         {
//             buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
//             Debug.Log(buffs[i].value);
//             {
//                 buffs[i].attribute = item.data.buffs[i].attribute;
//             }
//         }
//     }
// }

// [System.Serializable]
// public class ItemBuff : IModifier
// {
//     public Attributes attribute;
//     public int value;
//     public int min;
//     public int max;
//     public ItemBuff(int _min, int _max)
//     {
//         min = _min;
//         max = _max;
//         value = UnityEngine.Random.Range(min, max);
//         //GenerateValue();
//     }

//     public void AddValue(ref int baseValue)
//     {
//         baseValue += value;
//     }
//     public void GenerateValue()
//     {
//         value = UnityEngine.Random.Range(min, max);
//     }
// }