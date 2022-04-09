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

    public EquipmentType MyType
    {
        get{return type;}
    }

    public void Equip()
    {
        EquipmentPanel.MyInstance.EquipItem(this);
        

    }
    public void show()
    {

        if(type == EquipmentType.MainHand || type == EquipmentType.OffHand)
        {
            if(type == EquipmentType.MainHand)
            {
                equipPosition = GameObject.Find("MainHandItem");
            }
            if(type == EquipmentType.OffHand)
            {
                equipPosition = GameObject.Find("OffHandItem");
            }
            // EQUIPPEDITEM = Instantiate(PrefabObject);
            // Rigidbody rb = EQUIPPEDITEM.GetComponent<Rigidbody>();
            // rb.useGravity = false;
            // rb.isKinematic = true;
            // EQUIPPEDITEM.GetComponent<BoxCollider>().enabled = false;
            // EQUIPPEDITEM.GetComponentInChildren<BoxCollider>().enabled = false;
            // EQUIPPEDITEM.transform.parent = equipPosition.transform;
            // EQUIPPEDITEM.transform.position = new Vector3(equipPosition.transform.position.x,equipPosition.transform.position.y, equipPosition.transform.position.z);
            // EQUIPPEDITEM.transform.rotation = new Quaternion(-90,0,180,0);
        }
    }
    public void hideItem()
    {
        Destroy(EQUIPPEDITEM);
    }
        
    
}

