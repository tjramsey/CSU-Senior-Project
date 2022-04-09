using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    [SerializeField]
    private ItemObject[] craftableItems;
    public ItemObject[] MyCraftables
    {
        get { return craftableItems;}
    }
    [SerializeField]
    private int craftingAmount;
    public float TheDistance;
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
    private GameObject CraftingScreen;

    public GameObject MyCraftingScreen
    {
        get { return CraftingScreen;}
        set { CraftingScreen = value;}
    }

    [SerializeField]
    private CraftingWindow CW;

    public CraftingWindow MyCW
    {
        get { return CW;}
        set { CW = value;}
    }

    [SerializeField]
    private GameObject craftStation;

    void Awake()
    {
        ActionDisplay = GameObject.Find("ActionDisplay");
        ActionText = GameObject.Find("ActionText");
        MyCraftingScreen = GameObject.Find("CraftingScreen");
        MyCW = MyCraftingScreen.GetComponent<CraftingWindow>();
    }
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver () {
        if(TheDistance <= 3 /*&& UiManager.MyInstance.CraftingOpen != true*/)
        {
             ActionText.GetComponent<Text>().text = "Use "+ this.craftStation.name;
            ActionDisplay.GetComponent<Text>().text = "[E]";

            if(Input.GetButtonDown("Action"))
            {
                ActionText.GetComponent<Text>().text = "";
                ActionDisplay.GetComponent<Text>().text = "";
                UiManagerScript.MyInstance.OpenCloseCrafting();
                MyCW.ShowCrafts(this);      
            }
        }
        else
        {
            ActionText.GetComponent<Text>().text = "";
            ActionDisplay.GetComponent<Text>().text = "";
        }  
    }
    public void Close()
    {
        ActionText.GetComponent<Text>().text = "Use " + this.craftStation.name;
        ActionDisplay.GetComponent<Text>().text = "[E]";
    }
    
    void OnMouseExit() {
        ActionText.GetComponent<Text>().text = "";
        ActionDisplay.GetComponent<Text>().text = "";
    }
}
