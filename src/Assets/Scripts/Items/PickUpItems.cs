using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItems : MonoBehaviour
{
    //public InventoryObject Inventory;
    public float TheDistance;
    public bool closeEnough = false;

    [SerializeField]
    public GameObject ActionDisplay;

    [SerializeField]
    public GameObject ActionText;

    public ItemObject item;

    void Start() {
        ActionDisplay = GameObject.Find("ActionDisplay");
        ActionText = GameObject.Find("ActionText");
    }

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
            if(TheDistance <= 2)
            {
                closeEnough = true;
                ActionText.GetComponent<Text>().text = "Pick up";
                ActionDisplay.GetComponent<Text>().text = "[E]";
                if(Input.GetButtonDown("Action"))
                {
                
                    ActionText.GetComponent<Text>().text = "";
                    ActionDisplay.GetComponent<Text>().text = "";
                    
                    if(InventoryScript.MyInstance.AddItem(item))
                        Destroy(gameObject);
                }
            }
            
            if(TheDistance > 2)
            {
                closeEnough = false;
                ActionText.GetComponent<Text>().text = "";
                ActionDisplay.GetComponent<Text>().text = "";
            }
    
    }
    void OnMouseExit() {
        ActionDisplay.GetComponent<Text>().text = "";
        ActionText.GetComponent<Text>().text = "";
    }
}
