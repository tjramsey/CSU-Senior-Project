using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType{
    OneHandedSword,
    TwoHandedSword,
    OneHandedAxe,
    TwoHandedAxe,
    Mace,
    Warhammer,
    Bow,
    Crossbow,
    Dagger
}

[CreateAssetMenu(fileName  =  "New Weapon Object", menuName = "Inventory System/Items/Weapon")]

public class WeaponItem : BaseItem
{
    public float BasePierceDamage;
    public float BaseBluntDamage;
    public float BaseSlashDamage;

    public float SlashDamage;
    public float PierceDamage;
    public float BluntDamage;

    public float Damage;
    public float BaseDamage;

    public float Durability;
    public float BaseDurability;
    public float DurabilityLoss;
    public float Speed;

    public void Awake()
    {
        type = ItemType.Weapon;
    }
}
