using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Attributes{
    Agility,
    Intellect,
    Strength,
    Charisma,
    Endurance,
    Damage,
    Armor
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/BaseItem")]
public abstract class ItemObject : ScriptableObject, IMoveable, IDescribeable
{
    
    [SerializeField]
    private string title;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    public bool stackable;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int dropChance;
    [SerializeField]
    private Component[] Components;

    public Component[] MyComponents
    {
        get { return Components;}
    }

    private SlotScript slot;

     public SlotScript MySlot 
    { 
        get{return slot;} 
        set{slot = value;}
    }

    public string MyTitle
    {
        get {return title;}
    }
    public Sprite MyIcon
    {
        get {return icon;}
    }

    public GameObject PrefabObject
    {
        get{
            return prefab;
        }
    }
    public int MyDropChance
    {
        get
        {
            return dropChance;
        }
    }
    
    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    public string GetDescription()
    {
        string stats = string.Empty;
        title = MyTitle;
        stats += MyTitle+ "\n"+ data.value;

        for(int i = 0; i < data.buffs.Length; i++)
            {
                if(data.buffs[i].value > 0)
                {
                    Debug.Log(data.buffs[i].attribute.ToString());
                    stats += "\n" + data.buffs[i].value + " " +data.buffs[i].attribute.ToString();
                }

            }

        return stats;
    }
    public void Remove()
    {
        if(MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
    public void dropItem(GameObject dropArea)
    {
        //Debug.Log("Player Position: x = " + dropArea.transform.position.x + " y = "+ dropArea.transform.position.y + " z = " + dropArea.transform.position.z);
        GameObject newItem = Instantiate(prefab);
        newItem.GetComponent<PickUpItems>().item = this;
        newItem.GetComponent<PickUpItems>().ActionDisplay = GameObject.Find("ActionDisplay");
        newItem.GetComponent<PickUpItems>().ActionText = GameObject.Find("ActionText");
        newItem.transform.position = new Vector3(dropArea.transform.position.x,dropArea.transform.position.y, dropArea.transform.position.z);
        GameManager.MyInstance.WorldObjects.Add(newItem);
    }


}

[Serializable]
public class Component
{
    [SerializeField]
    private ItemObject item;

    public ItemObject MyItem
    {
        get { return item;}
    }
    [SerializeField]
    private int amount;
    public int MyAmount{
        get { return amount;}
    }
}

[Serializable]
public class Item 
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public int value;
    public int MaxVal;
    public int MinVal;
    public int SlotIndex;

    public Item()
    {
        Name = "";
        Id =-1;
        value = 0;
    }
    public Item(ItemObject item)
    {
        Name = item.MyTitle;
        Id = item.data.Id;
        value = UnityEngine.Random.Range(MinVal, MaxVal +1);
        Debug.Log(item.data.buffs.Length);


        buffs = new ItemBuff[item.data.buffs.Length];
        Debug.Log("buffs.Length = " + buffs.Length);
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
            Debug.Log(buffs[i].value);
            {
                buffs[i].attribute = item.data.buffs[i].attribute;
            }
        }
    }
}

[Serializable]
public class ItemBuff : IModifier
{
    public Attributes attribute;
    public int min;
    public int max;
    public int value;

    public ItemBuff(int _min, int _max)
    {
        Debug.Log("NewBuff max = " + _max + " min = " + _min);
        min = _min;
        max = _max;
        //value = UnityEngine.Random.Range(min, max);
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}
