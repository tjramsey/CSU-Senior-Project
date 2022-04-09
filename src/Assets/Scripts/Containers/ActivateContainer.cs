using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityStandardAssets.Characters.FirstPerson;


public class ActivateContainer : MonoBehaviour
{
    // public float TheDistance;

    // [SerializeField]
    // private GameObject ActionDisplay;

    // [SerializeField]
    // private GameObject ActionText;

    // public GameObject ContainerInventory;
    // public GameObject PlayerInventory;
    // public GameObject ThePlayer; 
    // public int tick;


    // void Start() 
    // {
    //     ActionDisplay = GameObject.Find("ActionDisplay");
    //     ActionText = GameObject.Find("ActionText");
    // }

    // void Update()
    // {
    //     TheDistance = PlayerCasting.DistanceFromTarget;
    //     tick = Menus.TabTick;
    // }

    // void OnMouseOver () {

    //     if(TheDistance <= 3)
    //     {
    //         if(Menus.PlayerInventory == 0 && Menus.StoreInventory == 0 && Menus.ContainerInventory == 0 && Menus.TabTick >= 20){
    //             ActionText.GetComponent<Text>().text = "Open";
    //             ActionDisplay.GetComponent<Text>().text = "[E]";
    //             if(Input.GetButtonDown("Action"))
    //             {
    //                 ActionText.GetComponent<Text>().text = "";
    //                 ActionDisplay.GetComponent<Text>().text = "";
    //                 Cursor.lockState = 0;
    //                 AttackBlocker.BlockSword = 1;
    //                 Cursor.visible = true;
    //                 ContainerInventory.SetActive(true);
    //                 PlayerInventory.SetActive(true);
    //                 GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
    //                 Menus.ContainerInventory = 1;
    //                 Menus.TabTick = 0;
                

    //             }
    //         }
    //         else{
    //             if(Input.GetKey(KeyCode.Tab) && Menus.ContainerInventory == 1 && tick >= 20)
    //             {
    //                 ActionText.GetComponent<Text>().text = "Open";
    //                 ActionDisplay.GetComponent<Text>().text = "[E]";
    //                 Cursor.lockState = CursorLockMode.Locked;
    //                 AttackBlocker.BlockSword = 0;
    //                 Cursor.visible = false;
    //                 ContainerInventory.SetActive(false);
    //                 PlayerInventory.SetActive(false);
    //                 GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
    //                 Menus.ContainerInventory = 0;
    //                 Menus.TabTick = 0;
                

    //             }
    //         }

    //     }
    //     else
    //     {
    //         ActionText.GetComponent<Text>().text = "";
    //         ActionDisplay.GetComponent<Text>().text = "";
    //     }
        
    // }

    // void OnMouseExit() {
    //     AttackBlocker.BlockSword = 0;
    //         ActionText.GetComponent<Text>().text = "";
    //         ActionDisplay.GetComponent<Text>().text = "";
    // }




}

