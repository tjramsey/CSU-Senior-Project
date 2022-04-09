using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;


public class UiManagerScript: MonoBehaviour
{
    private static UiManagerScript instance;

    public static UiManagerScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UiManagerScript>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Button[] actionButtons;

    private KeyCode action1, action2, action3, openMenu, openInventory;

    [SerializeField]
    private GameObject OptionsMenu;

    [SerializeField]
    private GameObject SaveMenu;

    [SerializeField]
    private GameObject GameMenu;

    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private GameObject Equipment;
    [SerializeField]
    private GameObject QuestLog;
    [SerializeField]
    private GameObject ContainerInventory;
    [SerializeField]
    private GameObject QuestGiver;
    [SerializeField]
    private GameObject Shop;
    [SerializeField]
    private GameObject Crafting;
    [SerializeField]
    private GameObject NPCTalk;

    [SerializeField]
    private bool OptionsMenuOpen;
    [SerializeField]
    private bool SaveMenuOpen;
    [SerializeField]
    private bool GameMenuOpen;
    [SerializeField]
    private bool InventoryOpen;
    [SerializeField]
    private bool EquipmentOpen;
    [SerializeField]
    private bool QuestLogOpen;
    [SerializeField]
    private bool CursorLocked;
    [SerializeField]
    private bool ContainerOpen;
    [SerializeField]
    private bool QuestGiverOpen;
    [SerializeField]
    private bool ShopOpen;
    [SerializeField]
    private bool CraftingOpen;
    [SerializeField]
    private bool NPCOpen;

    public bool MenuOpen;

    public bool MyContainerOpen
    {
        get {return ContainerOpen;}
    }
    public bool MyQuestGiverOpen
    {
        get {return QuestGiverOpen;}
    }
    public bool MyCraftingOpen
    {
        get {return CraftingOpen;}
    }
        
    private GameObject[] keybindButtons;

    public GameObject FPScontroller;

    [SerializeField]
    private GameObject ToolTip;

    private TextMeshProUGUI ToolTipText;

    public bool MyCursorLocked 
    {
        get { return CursorLocked;}
    }





    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("KeyBind");

        ToolTipText = ToolTip.GetComponentInChildren<TextMeshProUGUI>();
    }



    // Start is called before the first frame update
    void Start()
    {
        //Keybinds
       action1 = KeyCode.Alpha1;
       action2 = KeyCode.Alpha2;
       action3 = KeyCode.Alpha3;
       openMenu = KeyCode.M;
       openInventory = KeyCode.Tab;

        OptionsMenuOpen = false;
        GameMenuOpen= false;
        InventoryOpen= false;
        EquipmentOpen= false;
        QuestLogOpen= false;
        CursorLocked = true;
        ContainerOpen = false;
        QuestGiverOpen = false;
        SaveMenuOpen = false;
        CraftingOpen = false;
        NPCOpen = false;


    }

    
    // Update is called once per frame
    void Update()
    {
        if(GameMenuOpen || OptionsMenuOpen || SaveMenuOpen || InventoryOpen || EquipmentOpen ||ContainerOpen|| QuestGiverOpen || QuestLogOpen || CraftingOpen || NPCOpen)
        {
            //Debug.Log("Menu is open");
            Time.timeScale = 0;
            MenuOpen = true;
            UnlockCursor();
        }
        else
        {
            Time.timeScale = 1;
            MenuOpen = false;
            LockCursor();
        }
        if(Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }
        if(Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);
        }
        if(Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);
        }
        if(Input.GetKeyDown(openMenu))
        {
            HideToolTip();
            if(OptionsMenuOpen == true){
                OpenCloseOptions();
                //OpenCloseGameMenu();
            }
            else if (SaveMenuOpen == true){
                OpenCloseSave();
                //OpenCloseGameMenu();
            }
            else
            {
                OpenCloseGameMenu();
            }
        }
        if(Input.GetKeyDown(openInventory))
        {
            HideToolTip();
            HandScript.MyInstance.Drop();
            if(OptionsMenuOpen != true) 
            {
                if(GameMenuOpen != true && SaveMenuOpen != true){
                    if(QuestGiverOpen == true)
                    {
                        OpenCloseQuestGiver();
                        OpenCloseQuestLog();
                    }
                    else if(InventoryOpen == true)
                    {
                        OpenCloseInventory();
                        if(QuestLogOpen == true)
                        {
                            OpenCloseQuestLog();
                        }
                        else if(ContainerOpen == true && ShopOpen != true)
                        {
                            OpenCloseContainer();
                        }
                        else if(ShopOpen == true)
                        {
                            OpenCloseShop();
                        }
                        else if (EquipmentOpen == true)
                        {
                            OpenCloseEquipment();
                        }
                    }
                    else{
                        OpenCloseInventory();
                        OpenCloseEquipment();
                    }
                    
                }
            }
        }
    }

    private void ActionButtonOnClick(int buttonIndex)
    {
        actionButtons[buttonIndex].onClick.Invoke();
    }

    public void OpenCloseOptions()
    {
        CanvasGroup cg = OptionsMenu.GetComponent<CanvasGroup>();
        if(OptionsMenuOpen == false)
        {
            OpenCloseGameMenu();
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            OptionsMenuOpen = true;
            
        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            OptionsMenuOpen = false;
            OpenCloseGameMenu();
        }

    }
    public void OpenCloseSave()
    {
        CanvasGroup cg = SaveMenu.GetComponent<CanvasGroup>();
        if(SaveMenuOpen == false)
        {
            OpenCloseGameMenu();
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            SaveMenuOpen = true;
            
        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            SaveMenuOpen = false;
            OpenCloseGameMenu();
        }

    }
    public void OpenCloseGameMenu()
    {
        CanvasGroup cg = GameMenu.GetComponent<CanvasGroup>();
        if(GameMenuOpen == false)
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            GameMenuOpen = true;
        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            GameMenuOpen = false;
        }
        

    }
    public void OpenCloseInventory()
    {
        CanvasGroup cg = Inventory.GetComponent<CanvasGroup>();
        if(InventoryOpen == false)
        {
            // if(ContainerOpen == false)
            //     if(EquipmentOpen == false)
            //         OpenCloseEquipment();
            
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            InventoryOpen = true;
        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            InventoryOpen = false;
        }
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }
    public void OpenCloseContainer()
    {
        CanvasGroup cg = ContainerInventory.GetComponent<CanvasGroup>();
        if(ContainerOpen == false)
        {

            cg.alpha = 1;
            cg.blocksRaycasts = true;
            ContainerOpen = true;

        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            ContainerOpen = false;
        }
    }
    public void OpenCloseEquipment()
    {
        CanvasGroup cg = Equipment.GetComponent<CanvasGroup>();
        Debug.Log("EquipmentOpen = "+EquipmentOpen);
        if(EquipmentOpen == false)
        {
            
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            EquipmentOpen = true;
        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            EquipmentOpen = false;
        }

    }
    public void OpenCloseQuestLog()
    {
        CanvasGroup cg = QuestLog.GetComponent<CanvasGroup>();
        if(QuestLogOpen == false)
        {
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            QuestLogOpen = true;
        }
        else
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            QuestLogOpen= false;
        }

    }
    public void OpenCloseQuestGiver()
    {
        CanvasGroup cg = QuestGiver.GetComponent<CanvasGroup>();
        if(QuestGiverOpen == false)
        {
            //Debug.Log("Open Quest Giver");
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            QuestGiverOpen = true;

        }
        else
        {
            //Debug.Log("Close Quest Giver");
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            QuestGiverOpen= false;

        }

    }
    public void OpenCloseShop()
    {
        CanvasGroup cg = Shop.GetComponent<CanvasGroup>();
        if(ContainerOpen == false)
        {
            //Debug.Log("Open Shop");
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            ContainerOpen = true;
            ShopOpen = true;
        }
        else
        {
            //Debug.Log("Close Shop");
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            ContainerOpen= false;
            ShopOpen = false;
        }
    }
    public void OpenCloseCrafting()
    {
        CanvasGroup cg = Crafting.GetComponent<CanvasGroup>();
        if(CraftingOpen == false)
        {
            //Debug.Log("Open Shop");
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            CraftingOpen = true;
            LockCursor();
        }
        else
        {
            //Debug.Log("Close Shop");
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            CraftingOpen = false;
            UnlockCursor();

        }

    }
    public void OpenCloseNPC()
    {
        CanvasGroup cg = NPCTalk.GetComponent<CanvasGroup>();
        if(NPCOpen == false)
        {
            //Debug.Log("Open Shop");
            cg.alpha = 1;
            cg.blocksRaycasts = true;
            NPCOpen = true;
            LockCursor();
        }
        else
        {
            //Debug.Log("Close Shop");
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            NPCOpen = false;
            UnlockCursor();

        }

    }
    public void LockCursor()
    {
        CursorLocked = true;
		FPScontroller.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CursorLocked = true;
        Debug.Log(string.Concat(Cursor.lockState + " and " + Cursor.visible + " locked "));
    }

    public void UnlockCursor()
    {
        CursorLocked = false;
		 	FPScontroller.GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CursorLocked = false;
            Debug.Log(string.Concat(Cursor.lockState + " and " + Cursor.visible + " unlocked "));
    }
    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x =>x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else{
            clickable.MyStackText.color = new Color(0,0,0,0);
            clickable.MyIcon.color = Color.white;
        }
        if(clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0,0,0,0);
            clickable.MyStackText.color = new Color(0,0,0,0);
        }
    }

    public void ShowToolTip(Vector3 position, IDescribeable description)
    {
        ToolTip.SetActive(true);
        ToolTip.transform.position = position;
        ToolTipText.text = description.GetDescription();
    }
    public void HideToolTip()
    {
        ToolTip.SetActive(false);
    }
    public void RefreshToolTip(IDescribeable description)
    {
        ToolTipText.text = description.GetDescription();
    }
}
