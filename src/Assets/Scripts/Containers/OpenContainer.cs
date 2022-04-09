using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OpenContainer : MonoBehaviour
{
    public float TheDistance;

    [SerializeField]
    private GameObject ActionDisplay;

    [SerializeField]
    private GameObject ActionText;

    void Start() 
    {
        ActionDisplay = GameObject.Find("ActionDisplay");
        ActionText = GameObject.Find("ActionText");
    }
    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver () {
        if(TheDistance <= 3)
        {
            ActionText.GetComponent<Text>().text = "Open";
            ActionDisplay.GetComponent<Text>().text = "[E]";
            if(Input.GetButtonDown("Action"))
            {
                ActionText.GetComponent<Text>().text = "";
                ActionDisplay.GetComponent<Text>().text = "";
            }
        }
        else
        {
            ActionText.GetComponent<Text>().text = "";
            ActionDisplay.GetComponent<Text>().text = "";
        }  
    }

    void OnMouseExit() {
        ActionText.GetComponent<Text>().text = "";
        ActionDisplay.GetComponent<Text>().text = "";
    }




}
