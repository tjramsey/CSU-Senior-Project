using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventoryScript : MonoBehaviour, Iinteractable
{
    private bool isOpen;
    
    public void Interact()
    {
        if(isOpen)
        {
            StopInteract();
        }
        else
        {
           isOpen = true;
          // UiManagerScript.MyInstance.OpenCloseOtherInventory(); 
        }
    }
    public void StopInteract()
    {

    }
}
