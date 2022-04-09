using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;

    private BagScript bagScript;

    public BagScript MyBagScript
    {
        get
        {
            return bagScript;
        }
    }

    [SerializeField]
    private SlotScript[] slots;

    public SlotScript[] MySlots
    {
        get { return slots;}
    }

     [SerializeField]
    private int bagIndex;

    public int MyIndex 
    { 
        get {return bagIndex;} 
        set {bagIndex = value;}
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach(SlotScript slot in MySlots)
            {
                if(slot.IsEmpty)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public List<ItemObject> GetItems()
    {
        List <ItemObject> items = new List<ItemObject>();
        foreach(SlotScript slot in MySlots)
        {
            if(!slot.IsEmpty)
            {
                foreach(ItemObject item in slot.MyItems)
                {
                    item.data.SlotIndex = slot.MyIndex;
                    Debug.Log(item.MyTitle + " : "+ item.data.SlotIndex + " : " + slot.MyIndex);
                    items.Add(item);
                }
            }
        }
        return items;
    }
    
    public void AddSlots(int slotCount)
    {
        slots = new SlotScript[slotCount];
        for(int i = 0; i<slotCount;i++)
        {
           SlotScript slot =  Instantiate(slotPrefab,transform).GetComponent<SlotScript>();
           slot.MyBag = this;
           slot.MyIndex = i;
           slots[i] = slot;
        }
    }

    public bool AddItem(ItemObject item)
    {
        print("AddItem in bagscript");
        print(slots.Length);
        foreach(SlotScript slot in slots)
        {
            Debug.Log(slot.IsEmpty);
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
    



    public void Clear()
    {
        foreach(SlotScript slot in slots)
        {
            slot.Clear();
        }
    }
}
