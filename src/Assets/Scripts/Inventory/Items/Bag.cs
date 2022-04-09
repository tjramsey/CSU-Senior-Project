using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName  =  "Bag", menuName = "Items/Bag", order = 1)]
public class Bag : ItemObject, IUseable
{
    [SerializeField]
    private int slotCount;

    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript {get; set;}
    
    public BagButton MyBagButton {get; set;}

   


    public int MySlotCount { get => slotCount;}

    public void Initialize(int slots)
    {
        this.slotCount = slots;
    }

    public void SetUpScript()
    {
        MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
        MyBagScript.AddSlots(MySlotCount);
    }

    public void Use()
    {
        if(InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab,InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slotCount);
            InventoryScript.MyInstance.AddBag(this);    
        }
        
    }

}
