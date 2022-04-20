using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : ContainerScript
{
    [SerializeField]
    private int ShopGold;

    [SerializeField]
    private Text ShopGoldText;

    public Text ShopReaction;

    public bool AbleToShop;

    void Awake()
    {
        AddSlots(30);
        UpdateShopGold();
        ShopGold = 1000;
    }

    public bool BuyItem(ItemObject item, SlotScript newSpot)
    {
        if(newSpot.MyBag is ShopScript)
        {
            return true;
        }
        if(item.data.value > PlayerStats.MyInstance.PlayerGold)
        {
            ShopReaction.text = "You do not have enough gold for that item";
            return false;
        }
        else{
            ShopReaction.text = "Thank you for your purchase";
            ShopGold += item.data.value;
            PlayerStats.MyInstance.PlayerGold -= item.data.value;
            UpdateShopGold();
            PlayerStats.MyInstance.UpdatePlayerGold();
            return true;
        }
    }
    public bool SellItem(ItemObject item, SlotScript newSpot)
    {

        if(item.data.value > ShopGold)
        {
            ShopReaction.text = "I do not have enough gold for that item";
            return false;
        }
        else{
            ShopReaction.text = "Thank you for your sale";
            ShopGold -= item.data.value;
            PlayerStats.MyInstance.PlayerGold += item.data.value;
            UpdateShopGold();
            PlayerStats.MyInstance.UpdatePlayerGold();
            return true;
        }
    }
    // public bool BuyItems(ObservableStack<ItemObject> items)
    // {
    //     if(item.MyValue > PlayerStats.MyInstance.PlayerGold)
    //     {
    //         return false;
    //     }
    //     else{
    //         ShopGold += item.MyValue;
    //         PlayerStats.MyInstance.PlayerGold -= item.MyValue;
    //         return true;
    //     }
    // }
    // public bool SellItems(ObservableStack<ItemObject> items)
    // {
    //     if(item.MyValue > ShopGold)
    //     {
    //         Text.GetComponent<Text>().text = "I do not have enough gold for that item";
    //         return false;
    //     }
    //     else{
    //         ShopGold -= item.MyValue;
    //         PlayerStats.MyInstance.PlayerGold += item.MyValue;
    //     }
    // }



    public void UpdateShopGold()
    {
        ShopGoldText.GetComponent<Text>().text = ShopGold.ToString();
    }


    public void closeShop()
    {
        container.closeShop();
        AbleToShop = false;
        PlayerStats.MyInstance.shopping = false;
        UiManagerScript.MyInstance.OpenCloseShop();
        UiManagerScript.MyInstance.OpenCloseInventory();
        UiManagerScript.MyInstance.OpenCloseNPC();
        
    }
}
