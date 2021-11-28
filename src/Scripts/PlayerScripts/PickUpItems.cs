using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItems : MonoBehaviour
{
    public InventoryObject Inventory;
    public float TheDistance;
    public bool closeEnough = false;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public BaseItem item;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }
    void OnMouseOver()
    {
            if(TheDistance <= 5)
            {
                closeEnough = true;
                ActionText.GetComponent<Text>().text = "Pick up";
                ActionDisplay.SetActive(true);
                ActionText.SetActive(true);
            }
            if(Input.GetButtonDown("Action"))
            {
                if(TheDistance <= 5)
                {
                    ActionDisplay.SetActive(false);
                    ActionText.SetActive(false);
                    Inventory.AddItem(item, 1);
                    Destroy(gameObject);
                }
            }
            if(TheDistance > 5)
            {
                closeEnough = false;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
            }
    
    }
    void OnMouseExit() {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }
}
