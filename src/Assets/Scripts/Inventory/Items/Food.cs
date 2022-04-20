using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName  =  "Food", menuName = "Items/Food", order = 3)]
public class Food : ItemObject, IUseable
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float mana;

    public void Use()
    {
        Remove();

        if(health > 0)
            PlayerStats.MyInstance.RestoreHealth(health);
        if(stamina > 0)
            PlayerStats.MyInstance.RestoreStamina(stamina);
        if(mana > 0)
            PlayerStats.MyInstance.RestoreMana(mana);
    }

    public string GetDescription(string stats)
    {
        if(health > 0)
            stats += "\n" + health + " Health" ;

        if(stamina > 0)
            stats += "\n" + stamina + " Stamina";

        if(mana > 0)
            stats += "\n" + mana + " Mana";
        return stats;

    }
}

