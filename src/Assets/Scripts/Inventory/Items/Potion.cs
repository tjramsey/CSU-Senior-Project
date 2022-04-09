using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[CreateAssetMenu(fileName  =  "Potion", menuName = "Items/Potion", order = 2)]
public class Potion : ItemObject, IUseable
{
    
    [SerializeField]
    private float health;
    [SerializeField]
    private float mana;
    [SerializeField]
    private float stamina;

    public void Use()
    {


        if(health != 0){
            if(PlayerStats.MyInstance.NeedsHealth()){
                Remove();
                PlayerStats.MyInstance.RestoreHealth(health);
            }
        }
        if(mana != 0){
            if(PlayerStats.MyInstance.NeedsMana()){
                Remove();
                PlayerStats.MyInstance.RestoreMana(mana);
            }
        }
        if(stamina != 0){
            if(PlayerStats.MyInstance.NeedsStamina()){
                Remove();
                PlayerStats.MyInstance.RestoreStamina(stamina);
            }
        }
        
    }
}
