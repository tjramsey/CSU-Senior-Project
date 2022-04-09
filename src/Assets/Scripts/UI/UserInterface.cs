// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.Events;
// using UnityEngine.UI;
// using TMPro;

// public abstract class UserInterface : MonoBehaviour
// {
//     public InventoryObject inventory;
//     public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    
//     void Start()
//     {
//         for(int i = 0; i < inventory.GetSlots.Length; i++)
//         {
//             inventory.GetSlots[i].parent = this;
//             inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;
//         }
//         CreateSlots();
//         AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
//         AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
//     }

//     private void OnSlotUpdate(InventorySlot _slot)
//     {
//         if(_slot.item.Id >= 0)
//             {
//                 _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot._itemObject.uiDisplay;
//                 _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
//                 _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "": _slot.amount.ToString("n0");
//             }
//             else
//             {
//                 _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
//                 _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
//                 _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
//             }
//     }

//     public abstract void CreateSlots();

//     protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
//     {
//         EventTrigger trigger = obj.GetComponent<EventTrigger>();
//         var eventTrigger = new EventTrigger.Entry();
//         eventTrigger.eventID = type;
//         eventTrigger.callback.AddListener(action);
//         trigger.triggers.Add(eventTrigger);
//     }

//     public void OnEnter(GameObject obj)
//     {
//         MouseData.SlotHoveredOver = obj;

//     }
//     public void OnExit(GameObject obj)
//     {
//         MouseData.SlotHoveredOver = null;
        
//     }
//     public void OnEnterInterface(GameObject obj)
//     {
//        MouseData.InterfaceMouseIsOver = obj.GetComponent<UserInterface>();
        
//     }
//     public void OnExitInterface(GameObject obj)
//     {
//        MouseData.InterfaceMouseIsOver = null;

        
//     }
//     public void OnDragStart(GameObject obj)
//     {
        
//         MouseData.TempItemBeingDragged = CreateTempItem(obj);
//     }

//     public GameObject CreateTempItem(GameObject obj)
//     {
//         GameObject TempItem = null;
//         if(slotsOnInterface[obj].item.Id >= 0)
//         {
//             TempItem = new GameObject();
//             var rt = TempItem.AddComponent<RectTransform>();
//             rt.sizeDelta = new Vector2(50,50);
//             TempItem.transform.SetParent(transform.parent);
//             var img = TempItem.AddComponent<Image>();
//             img.sprite =  slotsOnInterface[obj]._itemObject.uiDisplay;
//             img.raycastTarget = false;
//         }
//         return TempItem;
//     }
//     public void OnDragEnd(GameObject obj)
//     {   
//         Destroy(MouseData.TempItemBeingDragged);
//         if(MouseData.InterfaceMouseIsOver == null)
//         {
//             slotsOnInterface[obj].RemoveItem();
//             return;
//         }
//         if(MouseData.SlotHoveredOver)
//         {
//             InventorySlot mouseHoverSlotData = MouseData.InterfaceMouseIsOver.slotsOnInterface[MouseData.SlotHoveredOver];
//             inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
//         }
        

//     }
//     public void OnDrag(GameObject obj)
//     {
//         if(MouseData.TempItemBeingDragged != null)
//         {
//             Debug.Log(MouseData.SlotHoveredOver);
//             MouseData.TempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
//         }
//     }
// }

// public static class MouseData
// {
//     public static UserInterface InterfaceMouseIsOver;
//     public static GameObject TempItemBeingDragged;
//     public static GameObject SlotHoveredOver;
// }

// public static class ExtensionMethods
// {
//     public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
//     {
//        foreach(KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
//         {
//             if(_slot.Value.item.Id >= 0)
//             {
//                 _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value._itemObject.uiDisplay;
//                 _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
//                 _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "": _slot.Value.amount.ToString("n0");
//             }
//             else
//             {
//                 _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
//                 _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
//                 _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
//             }
//         } 
//     }
// }
