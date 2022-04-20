using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public delegate void SlotUpdated(CharacterButton cb);

public class CharacterButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private EquipmentType type;

    [SerializeField]
    private Equipment equippedItem;

    [SerializeField]
    private Image icon;

    public Equipment MyItem
    {
        get {return equippedItem;}
        set {equippedItem = value;}
    }
    public EquipmentType MyType
    {
        get {return type;}
    }

    public Image MyIcon
    {
        get{return icon;}
        set{icon = value;}
    }

    private void Awake()
    {
        MyIcon.GetComponent<Image>().color = new Color(0,0,0,0);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(HandScript.MyInstance.MyMoveable is Equipment)
            {
                Equipment tmp = (Equipment)HandScript.MyInstance.MyMoveable;

                if(tmp.MyType == type)
                {
                    Equip(tmp);
                }
                UiManagerScript.MyInstance.RefreshToolTip(tmp);
            }
            else if(HandScript.MyInstance.MyMoveable == null && equippedItem != null)
            {
                HandScript.MyInstance.TakeMoveable(equippedItem);
                EquipmentPanel.MyInstance.SelectedButton = this;
            }
            else{

            }
        }

    }
    public void Equip(Equipment equip)
    {
        Equipment prev;
        Equipment current;
        equip.Remove();
        PlayerControls.MyInstance.Dequip();
        if(equippedItem != null)
        {
            if(equippedItem != equip)
            {
                prev = equippedItem;
                if(equip.MySlot != null){
                    equip.MySlot.AddItem(prev);
                }
                else{
                    InventoryScript.MyInstance.AddItem(prev);
                }
                
            }
            else{
                prev = equip;
            }
            UiManagerScript.MyInstance.RefreshToolTip(equippedItem);
        }
        else{
            prev = null;
            UiManagerScript.MyInstance.HideToolTip();
        }
        MyIcon.GetComponent<Image>().color = new Color(1,1,1,1);
        icon.sprite = equip.MyIcon;
        this.equippedItem = equip; //A reference to the equiped armor
        
        if(HandScript.MyInstance.MyMoveable == (equip as IMoveable))
            HandScript.MyInstance.Drop();

        current = equip;
        PlayerControls.MyInstance.Equip();
        PlayerStats.MyInstance.UpdateEquipmentStats(prev,current);
        PlayerStats.MyInstance.updateStats();

    }

    public void Dequip()
    {
        MyIcon.GetComponent<Image>().color = new Color(0,0,0,0);
        icon.sprite = null;
        PlayerStats.MyInstance.UpdateEquipmentStats(this.equippedItem,null);
        this.equippedItem = null;
        PlayerStats.MyInstance.updateStats();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(equippedItem != null)
        {
            UiManagerScript.MyInstance.ShowToolTip(transform.position,equippedItem);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UiManagerScript.MyInstance.HideToolTip();
    }
}
