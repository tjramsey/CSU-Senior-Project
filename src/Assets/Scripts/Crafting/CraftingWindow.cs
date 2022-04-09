using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingWindow : MonoBehaviour
{
    private CraftingStation CS;

    [SerializeField]
    private TextMeshProUGUI stationName;
    
    [SerializeField]
    private GameObject craftPrefab;

    [SerializeField]
    private Transform craftArea;

    [SerializeField]
    private CraftScript selected;

    public CraftScript MySelected
    {
        get {
            return selected;
        }
    }

    [SerializeField]
    private int CraftAmount;

    [SerializeField]
    private int CraftAllAmount;
    

    private static CraftingWindow instance;

    public static CraftingWindow MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CraftingWindow>();
            }
            return instance;
        }
    }

    [SerializeField]
    private List<GameObject> recipes = new List<GameObject>();

    [SerializeField]
    private GameObject craftBtn, closeBtn;


    public void ShowCrafts(CraftingStation cs)
    {
        this.CS = cs;
        stationName.text = CS.name.ToUpper();
        foreach(GameObject go in recipes)
        {
            Destroy(go);
        }
        foreach(ItemObject item in CS.MyCraftables)
        {
            GameObject go = Instantiate(craftPrefab, craftArea);
            go.GetComponent<CraftScript>().MyItem = item;
            go.GetComponent<CraftScript>().ShowComponents();
            recipes.Add(go);

        }
    }

    public void SelectRecipe(CraftScript cs)
    {
        if(selected != null)
        {
            selected.Deselect();
        }
        this.selected = cs;
    }
    public void Close()
    {
        CS.Close();
        UiManagerScript.MyInstance.OpenCloseCrafting();
    }

    public void IncreaseAmount()
    {
        CraftAmount++;
    }
    public void DecreaseAmount()
    {
        CraftAmount--;
    }
    public void Craft()
    {
        if(AbleToCraftAmount()){
            for(int i = 0; i < CraftAmount; i++)
            {
                
                if(MySelected.MyItem.stackable == false)
                {
                    ItemObject CraftedItem = Object.Instantiate(MySelected.MyItem);
                    CraftedItem.data = new Item(CraftedItem);
                    if(InventoryScript.MyInstance.AddItem(CraftedItem))
                    {
                        RemoveMaterials();
                    }
                    else{
                        Debug.Log("Not enough space in inventory");
                    }
                }
                else{
                    if(InventoryScript.MyInstance.AddItem(MySelected.MyItem))
                    {
                        RemoveMaterials();
                    }
                    else{
                        Debug.Log("Not enough space in inventory");
                    }
                }
            }
        }
        else{
            print("You do not have enough materials to craft"+ CraftAmount.ToString() + " "+ MySelected.MyItem.MyTitle + "(s)");
        }
    }
    private bool AbleToCraftAmount()
    {
        Debug.Log("Craft Amount = " + CraftAmount);
        foreach(Component c in MySelected.MyItem.MyComponents)
        {
            int count = InventoryScript.MyInstance.GetItemCount(c.MyItem.MyTitle);
            if (count < c.MyAmount * CraftAmount)
            {

                return false;
            }
        }
        return true;
    }

    private void RemoveMaterials()
    {
        foreach(Component c in MySelected.MyItem.MyComponents)
            {
                for(int j = 0; j < c.MyAmount; j++)
                {
                    InventoryScript.MyInstance.RemoveItem(c.MyItem);
                }
            }
    }

    public void CraftAll()
    {
        bool notEnough = false;
        int  previousAmount = CraftAmount;
        CraftAmount = 1;
        while(AbleToCraftAmount())
        {
            CraftAmount++;
            if(CraftAmount > InventoryScript.MyInstance.MyEmptySlotCount)
            {
                CraftAmount--;
                break;
            }
        }
        CraftAmount--;
        Craft();
        CraftAmount = previousAmount;
    }
}
