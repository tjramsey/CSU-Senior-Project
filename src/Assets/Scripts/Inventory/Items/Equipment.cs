using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType {
    Head,
    Chest,
    Legs,
    OffHand,
    MainHand

}


[CreateAssetMenu(fileName  =  "Equipment", menuName = "Items/Equipment", order = 2)]
public class Equipment : ItemObject
{
    [SerializeField]
    private EquipmentType type;
    
    [SerializeField]
    private GameObject equipPosition;

    [SerializeField]
    private GameObject EQUIPPEDITEM;

    public GameObject MyObject
    {
        get { return EQUIPPEDITEM;}
    }

    private bool unsheathed = false;

    public EquipmentType MyType
    {
        get{return type;}
    }

    public void Equip()
    {
        EquipmentPanel.MyInstance.EquipItem(this);
        

    }  
}

