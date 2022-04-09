using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    private static EquipmentPanel instance;

    public static EquipmentPanel MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<EquipmentPanel>();

            }
            return instance;
        }
    }
    [SerializeField]
    private CharacterButton head, chest, legs, main, off;
    
    public CharacterButton SelectedButton{ get; set;}
    
    public void Awake()
    {
        
    }
    public void EquipItem(Equipment equip)
    {
        switch(equip.MyType)
        {
           case EquipmentType.Head:
                head.Equip(equip);
                break;
            case EquipmentType.Chest:
                chest.Equip(equip);
                break;
            case EquipmentType.Legs:
                legs.Equip(equip);
                break;
            case EquipmentType.OffHand:
                off.Equip(equip);
                break;
            case EquipmentType.MainHand:
                main.Equip(equip);
                break;
        
        }
    }

}
