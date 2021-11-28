using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateContainer : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Container;
    public GameObject Lid;
    public int open = 0;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver () {
        if(TheDistance <= 3 && open == 0)
        {
            ActionText.GetComponent<Text>().text = "Open Container";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }
        if(TheDistance <= 3 && open ==1 )
        {
            ActionText.GetComponent<Text>().text = "Close Container";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }

        if(Input.GetButtonDown("Action"))
        {
            if(TheDistance <= 3 && open == 1)
            {
                open = 1;
                Lid.GetComponent<Animation>().Play("CrateAnim");
                
            }
            
        }
        
        if(TheDistance > 3)
        {
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);
        }
    }

    void OnMouseExit() {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }




}

