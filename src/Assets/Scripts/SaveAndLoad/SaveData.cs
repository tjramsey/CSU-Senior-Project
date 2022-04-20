using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData{ get; set;}
    
    public List<ContainerData> MyContainerData { get; set;}
    
    public List<EquipmentData> MyEquipmentData { get; set;}
    
    public InventoryData MyInventoryData { get; set;}
    
    public List<QuestData> MyQuestData {get; set;}
    
    public List<QuestGiverData> MyQuestGiverData {get; set;}
    
    public DateTime MyDateTime { get; set; }

    public DateTime GameTime { get; set;}

    public string MyScene { get; set;}
    
    public SaveData()
    {
        MyContainerData = new List<ContainerData>();
        MyEquipmentData = new List<EquipmentData>();
        MyInventoryData = new InventoryData();
        MyQuestGiverData = new List<QuestGiverData>();
        MyQuestData = new List<QuestData>();
        MyDateTime = DateTime.Now;
    }
}

[Serializable]
public class PlayerData
{
    public string MyName{ get; set;}
    public int MyLevel{ get; set;}
    public float MyXp{ get; set;}
    public float MyMaxXp{ get; set;}
    public float MyHealth{ get; set;}
    public float MyMaxHealth{ get; set;}
    public float MyMana{ get; set;}
    public float MyMaxMana{ get; set;}
    public float MyStamina{ get; set;}
    public float MyMaxStamina{ get; set;}
    
    public float MyX{ get; set;}
    public float MyY{ get; set;}
    public float MyZ{ get; set;}
    public float MyRotX{ get; set;}
    public float MyRotY{ get; set;}
    public float MyRotZ{ get; set;}
    public float MyRotW{ get; set;}
    public float MyRotW2{ get; set;}

    public List<AttributeData> MyAttributes;

    public int MyGold {get; set;}

    public PlayerData(string name, int level, float xp, float maxXp, float health, float maxHealth, float mana, float maxMana, float stamina, float maxStamina, Vector3 position, float camerax, float cameraw, float charactery, float characterw, int gold)
    {
        this.MyName = name;
        this.MyLevel = level;
        this.MyXp = xp;
        this.MyMaxXp = maxXp;
        this.MyHealth = health;
        this.MyMaxHealth = maxHealth;
        this.MyMana = mana;
        this.MyMaxMana = maxMana;
        this.MyStamina = stamina;
        this.MyMaxStamina = maxStamina;

        this.MyX = position.x;
        this.MyY = position.y;
        this.MyZ = position.z;

        this.MyRotY = charactery;
        this.MyRotX = camerax;
        this.MyRotW2 = characterw;
        this.MyRotW = cameraw;

        this.MyAttributes = new List<AttributeData>();

        this.MyGold = gold;

    }
}

[Serializable]
public class AttributeData
{
    public Attributes type;
    public int value;

    public AttributeData(Attributes t, int v)
    {
        this.type = t;
        this.value = v;

    }
}

[Serializable]
public class ItemData
{
    public string MyTitle {get; set;}

    public int MyStackCount { get; set;}

    public int MySlotIndex { get; set;}

    public Item MyData {get; set;}

    public int BagIndex {get; set;}


    public ItemData(string title, Item data, int stackCount, int slotIndex)
    {
        MyTitle = title;
        MyData = data;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
    }
    public ItemData(string title, Item data, int stackCount, int slotIndex, int bagIndex)
    {
        MyTitle = title;
        MyData = data;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
        BagIndex = bagIndex;
    }

}

[Serializable]
public class ContainerData
{
    public string MyName {get; set;}
    public List<ItemData> MyItems { get; set;}
    public int Opened {get; set;}
    public ContainerData(string name, int opened)
    {
        MyName = name;
        MyItems = new List<ItemData>();
        Opened = opened;
    }
    public void AddItemData(ItemData data)
    {
        MyItems.Add(data);
    }
}

[Serializable]
public class InventoryData
{
    public BagData MyBagData {get; set;}
    
    public List<ItemData> MyItems { get; set;}
    
    public InventoryData()
    {
        MyItems = new List<ItemData>();
    }

}
[Serializable]
public class BagData 
{
    public string MyTitle {get; set;}
    public int MySlotCount { get; set;}
    public Item MyData { get; set;} 
    public BagData (string title, int slotCount, Item data)
    {
        this.MyTitle = title;
        this.MySlotCount = slotCount;
        this.MyData = data;
    }
}

[Serializable]
public class EquipmentData
{
    public string MyTitle { get; set; }
    public EquipmentType MyType { get; set;}
    public Item MyData { get; set;}
    public EquipmentData(string title, EquipmentType type, Item data)
    {
        MyTitle = title;
        MyType = type;
        MyData = data;
    }
}

[Serializable]
public class QuestGiverData{
    public int MyQuestGiverId { get; set;}
    public List<QuestData> MyQuests;
    public bool QuestGiverChecked;
    public string MyTime;
    public QuestGiverData()
    {
        MyQuests = new List<QuestData>();
    }
}
[Serializable]
public class QuestData
{
    public string MyTitle { get; set;}

    public string MyDescription {get; set;}
    public int MyGoldReward{get; set;}
    public int MyExpReward{get; set;}
    public bool MyTaken{get; set;}
    public bool MyTakenByPlayer{get; set;}
    public bool MarkedSeen{ get; set;}

    public List<CollectObjective> MyCollectObjectives { get; set;}
    public List<KillObjective> MyKillObjecives { get; set;}

    public int MyQuestGiverId{get; set;}
    public int MyQuestId { get; set;}
    public string MyStartDate { get; set;}
    public string MyCreationDate { get; set;}

    
    public QuestData(string title, string description, int gold, int exp, bool taken, bool takenByPlayer, bool markedSeen, List<CollectObjective> COs, List<KillObjective> KOs, int questGiverId, int questId, string startDate, string creationDate)
    {
        MyTitle = title;
        MyDescription = description;
        MyGoldReward = gold;
        MyExpReward = exp;
        MyTaken = taken;
        MyTakenByPlayer = takenByPlayer;
        MarkedSeen = markedSeen;
        MyCollectObjectives = COs;
        MyKillObjecives = KOs;
        MyQuestGiverId = questGiverId;
        MyQuestId = questId;
        MyStartDate = startDate;
        MyCreationDate = creationDate;
    }
}


