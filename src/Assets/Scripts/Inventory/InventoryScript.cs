using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ItemCountChanged(ItemObject item);

public class InventoryScript : MonoBehaviour
{
    public event ItemCountChanged ItemCountChangedEvent;

    private static InventoryScript instance;

    public static InventoryScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InventoryScript>();
            }
            return instance;
        }
    }

    private SlotScript fromSlot;
    private SlotScript selected;



    //DEBUGGING
    [SerializeField]
    private ItemObject[] items;

    [SerializeField]
    private BagButton bagSlot;

    private List<Bag>bags = new List<Bag>();

    public bool CanAddBag
    {
        get{return bags.Count < 2;}
    }
    public BagButton MyBagSlot { get { return bagSlot;}}

    public SlotScript MyFromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;
            if(value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }
    public SlotScript MySelectedSlot
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            if(value != null)
            {
                selected.MyIcon.color = Color.yellow;
            }
        }
    }
    public List<Bag> MyBags
    {
        get {
            return bags;
        }
    }

    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach(Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }
            return count;
        }
    }

    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;
            foreach(Bag bag in bags)
            {
                count += bag.MyBagScript.MySlots.Length;
            }
            return count;
        }
    }

    public int MyFullSlotCount 
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }

    private Bag BaseInventory;

    public Bag MyBaseInventory
    {
        get { return BaseInventory;}
    }


    private void Awake()
    {
        BaseInventory = (Bag)Instantiate(items[0]);
        BaseInventory.Initialize(20);
        //bags.Add(bag);
        BaseInventory.Use();
    }

    private async void Update()
    {
                
    }
    public bool AddItem(ItemObject item)
    {
        if(item.stackable == true)
        {
            if(PlaceInStack(item))
            {
                return true;
            }
            else
            {
                if(PlaceInEmpty(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
                return false;
            }
        }
        else
        {
            if(PlaceInEmpty(item))
            {
                OnItemCountChanged(item);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public void RemoveItem(ItemObject item)
    {
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem == item)
                {
                    slot.RemoveItem(item);
                    break;
                }
            }
        }
    }
    private bool PlaceInEmpty(ItemObject item)
    {
        foreach(Bag bag in bags)
        {
            if(bag.MyBagScript.AddItem(item))
            {
                OnItemCountChanged(item);
                return true;
            }
        }
        return false;
    }

    private bool PlaceInStack(ItemObject item)
    {
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slots in bag.MyBagScript.MySlots)
            {
                if(slots.StackItem(item))
                {
                    OnItemCountChanged(item);
                    return true;
                }
            }
        }
        return false;

    }


    public void AddBag(Bag bag)
    {
        Debug.Log(string.Concat("Bags: "+bags.Count));
        
        if(bags.Count > 2)
        {
            bag.MyBagScript.MyIndex = 1;
            bags.Add(bag);
            SwapBags(MyBagSlot.MyBag, bag);
        }
        if(bags.Count > 1)
        {
            bag.MyBagScript.MyIndex = 1;
            Debug.Log("bagSlot.MyBag = bag");
            bagSlot.MyBag = bag;
            bags.Add(bag);
            bag.MyBagButton = bagSlot;

        }
        else if(bags.Count == 0)
        {
            bag.MyBagScript.MyIndex = 0;
            bags.Add(bag);
        }
        else{
            Debug.Log("ERROR in AddBag in InventoryScript");
        }
    }

    
    public void LoadBag(Bag bag)
    {
        bag.SetUpScript();
        //bag.Use();
        bag.MyBagScript.MyIndex = 1;
        bagSlot.MyBag = bag;
        bags.Add(bag);
        bag.MyBagButton = bagSlot;
    }

    public void RemoveBag(Bag bag)
    {
        bag.MyBagScript.MyIndex = -1;
        bags.Remove(bag);
        Destroy(bag.MyBagScript.gameObject);
    }

    public void SwapBags(Bag oldBag, Bag newBag)
    {
        int newSlotCount = (MyTotalSlotCount - oldBag.MySlotCount) + newBag.MySlotCount;

        if(newSlotCount - MyFullSlotCount >= 0)
        {
            //Swap bags
            List<ItemObject> bagItems = oldBag.MyBagScript.GetItems();
            RemoveBag(oldBag);

            newBag.Use();

            foreach(ItemObject item in bagItems)
            {
                if(item != newBag)//To keep from duplicating bags
                {
                    AddItem(item);
                }
            }
            AddItem(oldBag);
            HandScript.MyInstance.Drop();

            MyInstance.fromSlot = null;
        }
    }

    public int GetItemCount(string type)
    {
        int itemCount = 0;
        Debug.Log(type + " = type");
        foreach(Bag bag in bags)
        {
            foreach(SlotScript slot in bag.MyBagScript.MySlots)
            {
                if(!slot.IsEmpty){  
                    Debug.Log(slot.MyItem.MyTitle + " Item is slot");
                    if (slot.MyItem.MyTitle == type)
                    {
                        itemCount += slot.MyItems.Count;
                        Debug.Log(itemCount + ": itemCount");
                    }
                }
            }
        }
        Debug.Log(itemCount + " = "+ itemCount);
        return itemCount;
    }

    public void OnItemCountChanged(ItemObject item)
    {
        if(ItemCountChangedEvent != null)
        {
            //Debug.Log(ItemCountChangedEvent+ " ICCE Not NULL" + item.MyTitle);
            ItemCountChangedEvent.Invoke(item);
        }
        //Debug.Log(ItemCountChangedEvent + "ICCE is NULL " + item.MyTitle);
    }

    public List<SlotScript> GetAllItems()
    {
        List<SlotScript> slots = new List<SlotScript>();

        foreach(SlotScript slot in BaseInventory.MyBagScript.MySlots)
        {
            if(!slot.IsEmpty)
            {
                slots.Add(slot);
            }
        }
        if(MyBagSlot.MyBag != null){
        foreach(SlotScript slot in MyBagSlot.MyBag.MyBagScript.MySlots)
        {
            if(!slot.IsEmpty)
            {
                slots.Add(slot);
            }
        }
        }
        return slots;
    }

    public void PlaceInSpecific(ItemObject item, int slotIndex, int bagIndex)
    {
        if(bagIndex == 0)
        {
            Debug.Log(slotIndex + " in base inventory bag");
            BaseInventory.MyBagScript.MySlots[slotIndex].AddItem(item);
        }
        else if(bagIndex == 1)
        {
            Debug.Log(slotIndex + " in equpped bag");
            MyBagSlot.MyBag.MyBagScript.MySlots[slotIndex].AddItem(item);
        }
        else{
            Debug.Log("Bag does not have proper index");
        }
    }

    

    
}
