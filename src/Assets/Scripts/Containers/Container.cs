using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum containertype {box, barrel, chest,enemy,shop}
public class Container : MonoBehaviour
{
    [SerializeField]
    private ItemDatabaseObject database;

    public float TheDistance;

    [SerializeField] //Only for looting enemies
    private Enemy enemy;
    [SerializeField]
    private GameObject ActionDisplay;
    public GameObject MyActionDisplay
    {
        get{
            return ActionDisplay;
        }
    }
    [SerializeField]
    private GameObject ActionText;
    public GameObject MyActionText
    {
        get{
            return ActionText;
        }
    }
    [SerializeField]
    private int FirstOpening;
    public int MyFirstOpening
    {
        get{
            return FirstOpening;
        }
        set{
            FirstOpening = value;
        }
    }
    [SerializeField]
    private List<ItemObject> items;

    public List<ItemObject> MyItems
    {
        get { return items;}
        set {items = value;}
    }
    [SerializeField]
    private List<ItemPosition> positions;

    public List<ItemPosition> MyPositions
    {
        get { return positions;}
        set {positions = value;}
    }
    [SerializeField]
    private ContainerScript bag;
    public ContainerScript MyBag
    {
        get{return bag;}
        set{bag = value;}
    }

    [SerializeField]
    private LootTable ltable;

    [SerializeField]
    private containertype ctype;

    [SerializeField]
    private GameObject contInv;

    public containertype Mytype
    {
        get{return ctype;}
    }

    // [SerializeField]
    // public ItemObject[] itemsInSlots = new ItemObject[20];

    
    void Awake()
    {
        ActionDisplay = GameObject.Find("ActionDisplay");
        ActionText = GameObject.Find("ActionText");
        if(Mytype == containertype.shop)
        {
            contInv = GameObject.Find("ShopInv");
        }
        else
            contInv = GameObject.Find("ContainerInv");
        bag = contInv.GetComponent<ContainerScript>();
    }
    

    void Start() 
    {
        FirstOpening = 0;
    }
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
        if(Mytype == containertype.enemy && enemy.IsDead && MyFirstOpening == 1 && UiManagerScript.MyInstance.MyContainerOpen == false && IsEmpty())
        {
            ActionText.GetComponent<Text>().text = "";
            ActionDisplay.GetComponent<Text>().text = "";
            StartCoroutine(enemy.DestroyBody());
        }

    }

    void OnMouseOver () {
        if(TheDistance <= 3 && Mytype != containertype.shop)
        {
            if(UiManagerScript.MyInstance.MyContainerOpen == false){
                if((Mytype == containertype.enemy && enemy.IsDead) || Mytype != containertype.enemy){
                ActionText.GetComponent<Text>().text = "Open";
                ActionDisplay.GetComponent<Text>().text = "[E]";
                }
            }
            if(Input.GetButtonDown("Action") && UiManagerScript.MyInstance.MyContainerOpen == false)
            {
                ActionText.GetComponent<Text>().text = "";
                ActionDisplay.GetComponent<Text>().text = "";
                bag.container = this;
                if(FirstOpening == 0)
                {
                    Debug.Log("First opened");
                    items = ltable.RollLoot();
                    foreach(ItemObject item in items)
                    {
                        bag.AddItemInContainer(item);
                    }
                    FirstOpening = 1;
                    StoreItems();
                    
                }
                else{
                    if(bag.MyEmptySlotCount < bag.MySlots.Length)
                    {
                        bag.Clear();
                    }
                    AddItems();
                }
                    UiManagerScript.MyInstance.OpenCloseContainer();
                    UiManagerScript.MyInstance.OpenCloseInventory();
            }
            else{
                if(Input.GetKey(KeyCode.Tab) && UiManagerScript.MyInstance.MyContainerOpen == true)
                {
                    StoreItems();
                    bag.Clear();
                    UiManagerScript.MyInstance.OpenCloseContainer();
                    ActionText.GetComponent<Text>().text = "Open";
                    ActionDisplay.GetComponent<Text>().text = "[E]";
                }
            }
        }
        else if(Mytype != containertype.shop)
        {
            ActionText.GetComponent<Text>().text = "";
            ActionDisplay.GetComponent<Text>().text = "";  
        }
        else
        {
        }  
    }

    public void OpenShop()
    {
        bag.container = this;
        if(FirstOpening == 0)
        {
            Debug.Log("First opened");
            items = ltable.RollLoot();
            foreach(ItemObject item in items)
            {
                bag.AddItemInContainer(item);
            }
            FirstOpening = 1;
            StoreItems();    
        }
        else{
            if(bag.MyEmptySlotCount < bag.MySlots.Length)
            {
                bag.Clear();
            }
            AddItems();
        }
        (bag as ShopScript).AbleToShop = true;
        UiManagerScript.MyInstance.OpenCloseShop();
        UiManagerScript.MyInstance.OpenCloseInventory();
        UiManagerScript.MyInstance.OpenCloseNPC();
        PlayerStats.MyInstance.shopping = true;
    }

    public void closeShop()
    {
        StoreItems();
        bag.Clear();
    }

    void OnMouseExit() {
        ActionText.GetComponent<Text>().text = "";
        ActionDisplay.GetComponent<Text>().text = "";
    }

    public void AddItems()
    {
        if(positions != null)
        {
            foreach(ItemPosition pos in positions)
            {
                Debug.Log(pos.MyTitle);
                ItemObject newItem = UnityEngine.Object.Instantiate(database.ItemObjects.Find( x=>x.MyTitle == pos.MyTitle));
                newItem.data = pos.Data;
                for(int i = 0; i < pos.Amount; i++){
                    this.bag.MySlots[pos.Position].AddItem(newItem);
                //items = bag.GetItems();
                }
            }
        }
    }

    public void StoreItems()
    {
        print("Bag empty slots = "+ bag.MyEmptySlotCount);
        items = bag.GetItems();
        MyPositions.Clear();
        foreach(SlotScript slot in bag.MySlots)
        {
            if(slot.MyItem != null){
                MyPositions.Add(new ItemPosition(slot.MyItem.MyTitle, slot.MyItem.data, slot.MyIndex, slot.MyItems.Count));
                //slot.Clear();
            }
            else{
                //Debug.Log("Slot" + slot.MyIndex + "Item = null");
            }
        }
        //bag.Clear();
        //print("Items");
    }

    public bool IsEmpty()
    {
        if(items.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class ItemPosition
{
    public string MyTitle;
    public Item Data;
    public int Position;
    public int Amount;

    public ItemPosition()
    {

    }
    public ItemPosition(string title, Item data, int slotIndex, int amount)
    {
        this.MyTitle = title;
        this.Data = data;
        this.Position = slotIndex;
        this.Amount = amount;
    }
}