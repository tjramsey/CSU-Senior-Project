using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
            }
            return instance;
        }
    }
    [SerializeField]
    private ItemDatabaseObject items;
    public GameObject Player;
    public GameObject Camera;
    public FirstPersonController FPC;
    public Container[] containers;
    public CharacterButton[] equipment;

    [SerializeField]
    private SavedGame[] saveSlots;

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private TextMeshProUGUI dialogueText;

    private SavedGame current;


    private string Action;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        containers = FindObjectsOfType<Container>();
        equipment = FindObjectsOfType<CharacterButton>();
        foreach(SavedGame saved in saveSlots)
        {
            ShowSavedFiles(saved);
        }  
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("Load"))
        {
            //Debug.Log("Loading game");
            Load(saveSlots[PlayerPrefs.GetInt("Load")]);
            PlayerPrefs.DeleteKey("Load");
        }
        else{
            PlayerStats.MyInstance.SetStartStats();
            TimeController.MyInstance.StartTimer();

        }

    }

    void Update()
    {
    }

    public void ShowDialogue(GameObject clickButton)
    {
        Action = clickButton.name;

        switch(Action)
        {
            case "Load":
                dialogueText.text = "Load " + clickButton.GetComponentInParent<SavedGame>().name +"?";
                break;
            case "Save":
                dialogueText.text = "Save "+ clickButton.GetComponentInParent<SavedGame>().name +"?";
                break;
            case "Delete":
                dialogueText.text = "Delete "+ clickButton.GetComponentInParent<SavedGame>().name +"?";
                break;
        }
        current = clickButton.GetComponentInParent<SavedGame>();
        dialogue.SetActive(true);

    }
    public void ExecuteAction()
    {
        switch(Action)
        {
            case "Load":
                LoadScene(current);
                break;
            case "Save":
                Save(current);
                break;
            case "Delete":
                Delete(current);
                break;
        }
        CloseDialogue();
    }

    private void LoadScene(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat", FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(file);
                file.Close();

                PlayerPrefs.SetInt("Load", savedGame.MyIndex);
                Debug.Log(data.MyScene);
                SceneManager.LoadScene(data.MyScene);
                //TimeController.MyInstance.SetCurrentDate(data.GameTime);
                //LoadTime(data);
            }
    }
    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    private void ShowSavedFiles(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat"))
        {
            //Debug.Log(savedGame+ " found");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
            savedGame.ShowInfo(data);
        }
        else
        {
            savedGame.HideInfo();
        }
    }

    public void Save(SavedGame savedGame)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat",FileMode.Create);

            SaveData data = new SaveData();

            data.MyScene = SceneManager.GetActiveScene().name;
            //Debug.Log("Scene" + data.MyScene);

            SaveTime(data);
            SaveBags(data);
            SaveInventory(data);
            SaveEquipment(data);
            SavePlayer(data);
            SaveContainers(data);
            SaveQuestGivers(data);
            SaveQuests(data);

            bf.Serialize(file, data);

            file.Close();

            ShowSavedFiles(savedGame);
            //Debug.Log("Saved game");
        }
        catch(System.Exception)
        {
            throw;
            //this is for handeling errors or corrupted data
        }
    }

    private void SavePlayer(SaveData data)
    {
        var tmp = PlayerStats.MyInstance;
        data.MyPlayerData = new PlayerData(tmp.MyName,
            tmp.MyLevel,
            tmp.MyXp,tmp.MyMaxXp, 
            tmp.currentHealth, tmp.maxHealth, 
            tmp.currentMana, tmp.maxMana, 
            tmp.currentStamina, tmp.maxStamina, 
            Player.transform.position,
            FPC.m_MouseLook.m_CameraTargetRot.x,
            FPC.m_MouseLook.m_CameraTargetRot.w,
            FPC.m_MouseLook.m_CharacterTargetRot.y,
            FPC.m_MouseLook.m_CharacterTargetRot.w,
            tmp.PlayerGold);
        
        foreach(Attribute a in PlayerStats.MyInstance.attributes)
        {
            data.MyPlayerData.MyAttributes.Add(new AttributeData(a.type, a.value));
        }
    }

    private void SaveContainers(SaveData data)
    {
        //Debug.Log("Start Container save");
        for(int i = 0; i < containers.Length; i++)
        {
            string  containername = containers[i].name;
            //Debug.Log("container name success");

            int opened = containers[i].MyFirstOpening;
            //Debug.Log("opened success");

            data.MyContainerData.Add(new ContainerData(containername, opened));
            //Debug.Log(" add container success");

            if(opened == 1)
            {
                foreach(ItemPosition pos in containers[i].MyPositions)
                {

                    string title = pos.MyTitle;
                    //Debug.Log("title success");
                    Item Idata = pos.Data;
                    //Debug.Log("data success");
                    int amount = pos.Amount;
                    //Debug.Log("amount  success");
                    int index = pos.Position;
                    //Debug.Log("index success");
                    ItemData temp = new ItemData(title, Idata,amount,index);

                    data.MyContainerData[i].AddItemData(temp);
                    //Debug.Log("Add item success");
                }
            }
            
        }
    }
    public void SaveBags(SaveData data)
    {
        if(InventoryScript.MyInstance.MyBagSlot.MyBag != null)
        {
            data.MyInventoryData.MyBagData = new BagData(InventoryScript.MyInstance.MyBagSlot.MyBag.MyTitle, InventoryScript.MyInstance.MyBagSlot.MyBag.MySlotCount, InventoryScript.MyInstance.MyBagSlot.MyBag.data);
            //Debug.Log(data.MyInventoryData.MyBagData.MyTitle + "saved");
        }
    }


    private void SaveEquipment(SaveData data)
    {
        foreach(CharacterButton equipSlot in equipment)
        {
            if(equipSlot.MyItem != null)
            {
                data.MyEquipmentData.Add(new EquipmentData(equipSlot.MyItem.MyTitle, equipSlot.MyItem.MyType, equipSlot.MyItem.data));
            }
        }
    }

    private void SaveInventory(SaveData data)
    {
        List<SlotScript> slots = InventoryScript.MyInstance.GetAllItems();

        foreach(SlotScript slot in slots)
        {
            int index = 0;
            if(slot.MyBag.MyIndex == 1)
            {
                index = slot.MyBag.MyIndex;
            }
            //Debug.Log("Saving item in inventory: " +slot.MyItem.MyTitle +slot.MyItem.data.Id + slot.MyItems.Count + slot.MyIndex + index);
            data.MyInventoryData.MyItems.Add(new ItemData(slot.MyItem.MyTitle, slot.MyItem.data, slot.MyItems.Count, slot.MyIndex, index));
        }
    }

    private void SaveQuests(SaveData data)
    {
        foreach(Quest temp in QuestLog.MyInstance.MyQuests)
        {
            data.MyQuestData.Add(new QuestData(temp.MyTitle, temp.MyDescription, temp.MyGold, temp.MyExperience, temp.IsTaken, temp.TakenByPlayer, temp.MarkedSeen, temp.MyCollectObjectives, temp.MyKillObjectives, temp.MyQuestGiverId, temp.MyId, temp.MyStartDate.ToString(), temp.MyCreationDate.ToString()));
        }
    }

    private void SaveQuestGivers(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();
        foreach(QuestGiver qg in questGivers)
        {
            QuestGiverData temp = new QuestGiverData();
            temp.MyQuestGiverId = qg.MyId;
            temp.MyTime = qg.MyTime.ToString();
            temp.QuestGiverChecked = qg.MyChecked;
            foreach(Quest quest in qg.MyQuests)
            {
                temp.MyQuests.Add(new QuestData(quest.MyTitle, quest.MyDescription, quest.MyGold, quest.MyExperience, quest.IsTaken, quest.TakenByPlayer, quest.MarkedSeen, quest.MyCollectObjectives, quest.MyKillObjectives, quest.MyQuestGiverId, quest.MyId, quest.MyStartDate.ToString(), quest.MyCreationDate.ToString()));
            }
            data.MyQuestGiverData.Add(temp);
        }

    }

    private void SaveTime(SaveData data)
    {
        data.GameTime = TimeController.MyInstance.getCurrentDay();
        //Debug.Log("Saved Game Time: "+ data.GameTime.ToString());
    }



    public void Load(SavedGame savedGame)
    {
        try
        {

            if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat")){
            
                BinaryFormatter bf = new BinaryFormatter();
                //Debug.Log("Load step 1 bf");
                FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat",FileMode.Open);
                //Debug.Log("Load step 2 file");
                file.Seek(0, SeekOrigin.Begin);
                //Debug.Log("Load step 3 Seek");
                SaveData data = (SaveData)bf.Deserialize(file);
                //Debug.Log("Load step 4 data");
                file.Close();
                //Debug.Log(data.MyPlayerData.MyLevel + "Loaded as player data");

                LoadTime(data);
                LoadBags(data);
                LoadInventory(data);
                LoadEquipment(data);
                LoadPlayer(data);
                LoadContainers(data);
                LoadQuestGivers(data);
                LoadQuests(data);

                //Debug.Log("Loaded Game");
            }
        }
        catch(System.Exception)
        {
            throw;
            Delete(savedGame);
            PlayerPrefs.DeleteKey("Load");
           //throw;
            //this is for handeling errors or corrupted data
        }
    }
    private void LoadTime(SaveData data)
    {
        TimeController.MyInstance.SetCurrentDate(data.GameTime);
        //Debug.Log("Loaded Game Time: "+ data.GameTime.ToString());

    }
    private void LoadPlayer(SaveData data)
    {
        PlayerStats.MyInstance.MyName = data.MyPlayerData.MyName;
        PlayerStats.MyInstance.MyLevel = data.MyPlayerData.MyLevel;
        PlayerStats.MyInstance.MyXp = data.MyPlayerData.MyXp;
        PlayerStats.MyInstance.MyMaxXp = data.MyPlayerData.MyMaxXp;
        PlayerStats.MyInstance.currentHealth = data.MyPlayerData.MyHealth;
        PlayerStats.MyInstance.maxHealth = data.MyPlayerData.MyMaxHealth;
        PlayerStats.MyInstance.currentMana = data.MyPlayerData.MyMana;
        PlayerStats.MyInstance.maxMana = data.MyPlayerData.MyMaxMana;
        PlayerStats.MyInstance.currentStamina = data.MyPlayerData.MyStamina;
        PlayerStats.MyInstance.maxStamina = data.MyPlayerData.MyMaxStamina;
        PlayerStats.MyInstance.PlayerGold = data.MyPlayerData.MyGold;
        
        Player.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY, data.MyPlayerData.MyZ);
        FPC.m_MouseLook.m_CharacterTargetRot = new Quaternion (0f, data.MyPlayerData.MyRotY, 0f,data.MyPlayerData.MyRotW);
        FPC.m_MouseLook.m_CameraTargetRot = new Quaternion (data.MyPlayerData.MyRotX,0f, 0f, data.MyPlayerData.MyRotW2);

        foreach(AttributeData ad in data.MyPlayerData.MyAttributes)
        {
            foreach(Attribute a in PlayerStats.MyInstance.attributes)
            {
                if(ad.type == a.type)
                {
                    a.value = ad.value;
                }
                PlayerStats.MyInstance.updateStatNums(a);
            }
        }
        
    }
    private void LoadContainers(SaveData data)
    {
        foreach(ContainerData container in data.MyContainerData)
        {
            Container c = Array.Find(containers, x=> x.name == container.MyName);
            c.MyFirstOpening = container.Opened;
            foreach(ItemData item in container.MyItems)
            {
                // ItemObject newItem = UnityEngine.Object.Instantiate(items.ItemObjects.Find( x=>x.MyTitle == item.MyTitle));
                // newItem.data = item.MyData;
                c.MyPositions.Add(new ItemPosition(item.MyTitle, item.MyData, item.MySlotIndex, item.MyStackCount));
                
            
            }
        }
    }
    private void LoadEquipment(SaveData data)
    {
        foreach(EquipmentData eData in data.MyEquipmentData)
        {
            CharacterButton cb = Array.Find(equipment, x=>x.MyType == eData.MyType);
            ItemObject newItem = UnityEngine.Object.Instantiate(items.ItemObjects.Find( x=>x.MyTitle == eData.MyTitle));
            newItem.data = eData.MyData;
            cb.Equip((newItem as Equipment));
        }
    }
    private void LoadBags(SaveData data)
    {
        if(data.MyInventoryData.MyBagData != null && data.MyInventoryData.MyBagData.MyData != null){
            Bag newBag = (Bag)Instantiate(items.ItemObjects.Find( x=>x.MyTitle == data.MyInventoryData.MyBagData.MyTitle));
           // Debug.Log("New Bag loaded: " + newBag.MyTitle);
            newBag.data  = data.MyInventoryData.MyBagData.MyData;
            newBag.Initialize(data.MyInventoryData.MyBagData.MySlotCount);
            InventoryScript.MyInstance.LoadBag(newBag);
           // Debug.Log("Load bag");
        }

    }
    private void LoadInventory(SaveData data)
    {
        foreach(ItemData item in data.MyInventoryData.MyItems)
        {
             ItemObject newItem = UnityEngine.Object.Instantiate(items.ItemObjects.Find( x=>x.MyTitle == item.MyTitle));
             newItem.data = item.MyData;

             for(int i = 0; i < item.MyStackCount; i++)
             {
                 InventoryScript.MyInstance.PlaceInSpecific(newItem, item.MySlotIndex, item.BagIndex);
             }
        }
    }
    private void LoadQuests(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();
        foreach(QuestData QD in data.MyQuestData)
        {
            QuestGiver qg = Array.Find(questGivers, x=> x.MyId == QD.MyQuestGiverId);
            Quest newQuest = qg.MyQuests.Find(x => x.MyId == QD.MyQuestId && x.MyQuestGiverId == QD.MyQuestGiverId);
            QuestLog.MyInstance.AcceptQuest(newQuest);
        }
    }
    private void LoadQuestGivers(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();
        foreach(QuestGiverData QGD in data.MyQuestGiverData)
        {
            QuestGiver qg = Array.Find(questGivers, x=> x.MyId == QGD.MyQuestGiverId);
            qg.loadedGame = true;
            foreach(QuestData QD in QGD.MyQuests)
            {
                    Debug.Log(QD.MyTitle);
                    Quest newQuest = new Quest(QD.MyTitle, QD.MyDescription, QD.MyGoldReward, QD.MyExpReward);
                    newQuest.MyCollectObjectives = QD.MyCollectObjectives;
                    newQuest.MyKillObjectives = QD.MyKillObjecives;
                    newQuest.IsTaken = QD.MyTaken;
                    newQuest.TakenByPlayer = QD.MyTakenByPlayer;
                    newQuest.MarkedSeen = QD.MarkedSeen; 
                    newQuest.MyId = QD.MyQuestId;
                    newQuest.MyQuestGiverId = qg.MyId;
                    newQuest.MyStartDate = Convert.ToDateTime(QD.MyStartDate);
                    newQuest.MyCreationDate = Convert.ToDateTime(QD.MyCreationDate);
                    qg.MyQuests.Add(newQuest);          
            }
        }
    }

    private void Delete(SavedGame savedGame)
    {
        //Debug.Log("Delete" + savedGame.name);
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat")){
            File.Delete(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat");
            savedGame.HideInfo();
        }
    }

}

        