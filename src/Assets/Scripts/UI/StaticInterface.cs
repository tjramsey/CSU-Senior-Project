﻿// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.Events;
// using UnityEngine.UI;
// using TMPro;

// public class StaticInterface : UserInterface
// {
//     public GameObject[] slots;
//     public override void CreateSlots() 
//     {
//         slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
//         for(int i = 0; i < inventory.Container.Slots.Length; i++)
//         {
//             var obj = slots[i];

//             AddEvent(obj, EventTriggerType.PointerEnter, delegate{OnEnter(obj);});
//             AddEvent(obj, EventTriggerType.PointerExit, delegate{OnExit(obj);});
//             AddEvent(obj, EventTriggerType.BeginDrag, delegate{OnDragStart(obj);});
//             AddEvent(obj, EventTriggerType.EndDrag, delegate{OnDragEnd(obj);});
//             AddEvent(obj, EventTriggerType.Drag, delegate{OnDrag(obj);});
//             inventory.GetSlots[i].slotDisplay = obj;
//             slotsOnInterface.Add(obj, inventory.Container.Slots[i]);
//         }
//     }
// }
