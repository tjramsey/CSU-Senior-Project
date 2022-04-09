using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Bag bag;

    public int index;

    [SerializeField]
    private Sprite full, empty;

    private void Start()
    {
        index = 1;
    }
    
    public Bag MyBag
    {
        get
        {
            return bag;
        }
        set
        {
            if(value != null)
            {
                GetComponent<Image>().sprite = full;
                GetComponent<Image>().color = new Color(1,1,1,1);
            }
            else
            {
                GetComponent<Image>().sprite = empty;
                //GetComponent<Image>().color = new Color(0,0,0,0);
            }
            bag = value;
        }
    }
     public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(InventoryScript.MyInstance.MyFromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
            {
                if(MyBag != null)
                {
                    InventoryScript.MyInstance.SwapBags(MyBag, HandScript.MyInstance.MyMoveable as Bag);
                }
                else{
                    Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
                    tmp.MyBagButton = this;
                    tmp.Use();
                    MyBag = tmp;
                    HandScript.MyInstance.Drop();
                    InventoryScript.MyInstance.MyFromSlot = null;
                }
            }
            else{
                if(MyBag != null)
                    HandScript.MyInstance.TakeMoveable(MyBag);
            }
        }
        
    }
    public void RemoveBag()
    {
        InventoryScript.MyInstance.RemoveBag(MyBag);
        MyBag.MyBagButton = null;

        foreach (ItemObject item in MyBag.MyBagScript.GetItems())
        {
            InventoryScript.MyInstance.AddItem(item);
        }
        MyBag = null;
    }
}
