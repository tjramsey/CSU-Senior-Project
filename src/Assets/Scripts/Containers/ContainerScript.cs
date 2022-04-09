using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : BagScript
{
    public Container container;
     void Awake()
     {
         AddSlots(20);
     }
    
    public bool AddItemInContainer(ItemObject item)
    {
        print("AddItem in bagscript");
        print(MySlots.Length);
        foreach(SlotScript slot in MySlots)
        {
            Debug.Log("Checking Slot");
            if(slot.IsEmpty)
            {
                Debug.Log("Added "+item.MyTitle);
                slot.AddItem(item);
                return true;
            }
            else{
                if(slot.StackItem(item))
                {
                    return true;
                }
            }
        }   
        return false;
    }
    public bool ReAddItems(ItemObject item)
    {
        print("AddItem in bagscript");
        print(MySlots.Length);
        bool added = false;
        foreach(SlotScript slot in MySlots)
        {
            if(slot.MyIndex == item.data.SlotIndex)
            {
                if(slot.MyItem != null)
                {
                    if(slot.MyItem.stackable == true)
                    {
                        slot.AddItem(item);
                    }
                }
                else{
                    slot.AddItem(item);
                }
            }
        }   
        return false;
    }
    
}
