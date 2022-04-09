using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TextMeshProUGUI stackSize;

    //Reference to the slot this bag belongs to 
    public BagScript MyBag {get; set;}

    [SerializeField]
    private ObservableStack<ItemObject> Items = new ObservableStack<ItemObject>();

    public int MyIndex { get; set;}
    
    
    public bool IsEmpty
    {
        get{return Items.Count == 0;}
    }
    // public bool IsFull
    // {
    //     get{
    //         if(IsEmpty)
    //         {
    //             return false;
    //         }
    //         else {
    //             return true;
    //         }

    //     }
    // }
    [SerializeField]
    private ItemObject item;

    public ItemObject MyItem
    {
        get
        {
            if(!IsEmpty)
            {
                item = Items.Peek();
                return Items.Peek();
            }
            return null;
        }
    }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public ObservableStack<ItemObject> MyItems
    {
        get
        {
            return Items;
        }
    }

    public int MyCount
    {
        get 
        {
           return Items.Count; 
        }
    }
    public TextMeshProUGUI MyStackText
    {
        get
        {
            return stackSize;
        }
    }
    //[SerializeField]
    //public ObservableStack<ItemObject> Items1 { get => Items2; set => Items2 = value; }
    //public ObservableStack<ItemObject> Items2 { get => Items; set => Items = value; }

    private void Awake()
    {
        Items.OnPop += new UpdateStackEvent(UpdateSlot);
        Items.OnPush += new UpdateStackEvent(UpdateSlot);
        Items.OnClear += new UpdateStackEvent(UpdateSlot);

    }
    public void Update()
    {
        if(InventoryScript.MyInstance.MySelectedSlot == this)
        {
            Debug.Log("Selected" + InventoryScript.MyInstance.MySelectedSlot.MyItem.MyTitle);
            if(Input.GetKeyDown(KeyCode.E))
            {
                UseItem();
                StartCoroutine(useWait());
            }
        }
    }
    public bool AddItem(ItemObject item)
    {
        if(PlayerStats.MyInstance.shopping == true)
        {
            if(MyBag is ShopScript)
            {
                if((MyBag as ShopScript).AbleToShop == true)
                {
                    if(!(MyBag as ShopScript).SellItem(item))
                    {
                        return false;
                    }
                    else{
                        Debug.Log("Sold" + item.MyTitle);
                    }
                }
            }
            if(InventoryScript.MyInstance.MyFromSlot.MyBag != null && InventoryScript.MyInstance.MyFromSlot.MyBag is ShopScript )
            { 
                BagScript FromBag = InventoryScript.MyInstance.MyFromSlot.MyBag;
                if((FromBag as ShopScript).AbleToShop == true){
                    if(!(FromBag as ShopScript).BuyItem(item))
                    {
                        return false;
                    }
                    else
                    {
                        Debug.Log("Bought" + item.MyTitle);
                    }
                }
            }
        }


        Debug.Log("Item being added"+item.MyTitle);
        item.MySlot = this;
        item.data.SlotIndex = this.MyIndex;
        Items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white;
        return true;
    }
    public void RemoveItem(ItemObject item)
    {
        if(!IsEmpty)
        {
            Items.Pop();
            if(IsEmpty)
            {
                item.MySlot = null;
                InventoryScript.MyInstance.MySelectedSlot = null;
            }
        }
        
    }

    public void Clear()
    {
        int initCount = MyItems.Count;

        if(MyItems.Count > 0)
        {
            for(int i = 0; i < initCount; i++)
            {
                InventoryScript.MyInstance.OnItemCountChanged(MyItems.Pop());
            }  
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        { 
            if(InventoryScript.MyInstance.MyFromSlot == null && !IsEmpty && InventoryScript.MyInstance.MySelectedSlot == null) //Nothing to move
            {
                if(HandScript.MyInstance.MyMoveable != null )
                {
                    if(HandScript.MyInstance.MyMoveable is Bag){
                        if(MyItem is Bag)
                        {
                            InventoryScript.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }
                    else if (HandScript.MyInstance.MyMoveable is Equipment)
                    {
                        if(MyItem is Equipment && (MyItem as Equipment).MyType == (HandScript.MyInstance.MyMoveable as Equipment).MyType)
                        {
                            (MyItem as Equipment).Equip();
                            
                            HandScript.MyInstance.Drop();
                        }
                    }
                }
                else
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    InventoryScript.MyInstance.MyFromSlot = this;
                }

            }
            else if (InventoryScript.MyInstance.MyFromSlot == null)
            {
                if(HandScript.MyInstance.MyMoveable is Bag){
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;
                    if(bag.MyBagScript != MyBag && InventoryScript.MyInstance.MyEmptySlotCount - bag.MySlotCount > 0){
                        Bag UniqueBag = (Bag)Instantiate(bag);
                        UniqueBag.data = new Item(bag);
                        AddItem(UniqueBag);
                        bag.MyBagButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    }
                }
                else if(HandScript.MyInstance.MyMoveable is Equipment){
                    Equipment equipItem = (Equipment)HandScript.MyInstance.MyMoveable;
                    AddItem(equipItem);
                    EquipmentPanel.MyInstance.SelectedButton.Dequip();
                    HandScript.MyInstance.Drop();
                }
            
            }
            else if (InventoryScript.MyInstance.MyFromSlot != null)//Have something to move
            {
                if(PutItemBack() /*|| MergeItems(InventoryScript.MyInstance.MyFromSlot)*/ ||SwapItems(InventoryScript.MyInstance.MyFromSlot) || AddItems(InventoryScript.MyInstance.MyFromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();
                    
                    InventoryScript.MyInstance.MyFromSlot = null;
                    
                }
            }
        }
        if(eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null && this.MyItem != null)
        {
            if(InventoryScript.MyInstance.MySelectedSlot == null)
            {
                InventoryScript.MyInstance.MySelectedSlot = this;
            }
            else{
                InventoryScript.MyInstance.MySelectedSlot.MyIcon.color = Color.white;
                InventoryScript.MyInstance.MySelectedSlot = null;
            }
        }
    }


    public void UseItem()
    {
        if(MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
            
        }
        else if(MyItem is Equipment)
        {
            (MyItem as Equipment).Equip();            
        }
        RemoveItem(MyItem);
        
    }

    public bool StackItem(ItemObject item)
    {
        if(!IsEmpty && item.MyTitle == MyItem.MyTitle && item.data.Id == MyItem.data.Id && MyItem.stackable == true)
        {
            Items.Push(item);
            return true;
        }
        return false;
        
    }

    private bool PutItemBack()
    {
        if(InventoryScript.MyInstance.MyFromSlot == this)
        {
            InventoryScript.MyInstance.MyFromSlot.MyIcon.color = Color.white;
            return true;
        }
        return false;
    }

    public bool AddItems(ObservableStack<ItemObject> newItems)
    {
        if(IsEmpty || ( newItems.Peek().GetType() == MyItem.GetType() && newItems.Peek().data.Id == MyItem.data.Id))
        {
            int count = newItems.Count;

            for(int i = 0; i < count; i++)
            {
                ItemObject item = newItems.Pop();
                
                AddItem(item);
            }
            return true;
        }
        return false;
    }
    
    private bool SwapItems(SlotScript from)
    {
        if(IsEmpty)
        {
            return false;
        }
        if(from.MyItem.GetType() != MyItem.GetType() || from.MyItem.data.Id != MyItem.data.Id || MyItem.stackable == false)
        {
            //Copy all items to be swapped in the from slot
            ObservableStack<ItemObject>tmpFrom = new ObservableStack<ItemObject>(from.MyItems);
            
            //clear the from slot
            from.MyItems.Clear();
            //Add items to 'from' slot from the 'to' slot
            from.AddItems(Items);

            //Clear the 'to' slot
            Items.Clear();
            //Add items to 'to' slot from the 'from' slot
            AddItems(tmpFrom);
            return true;

        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if(IsEmpty)
        {
            return false;
        }
        if(from.MyItem.GetType() == MyItem.GetType())
        {
            //Debug.Log("Free space: " +free);
            AddItems(from.MyItems);
            // foreach(ItemObject item in from.MyItems)
            // {
            //     AddItem(item);
            //     Debug.Log("Pop "+ item.MyTitle);
            //     from.MyItems.Pop();
            //     if(from.MyItems.Count == 0)
            //     {
            //         break;
            //     }
            // }
            return true;
        }
        return false;
    }
    private void UpdateSlot()
    {
        UiManagerScript.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!IsEmpty)
        {
            UiManagerScript.MyInstance.ShowToolTip(transform.position, MyItem);

        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UiManagerScript.MyInstance.HideToolTip();
    }

    IEnumerator useWait(){
        yield return new WaitForSeconds(1.0f);
    }


}
