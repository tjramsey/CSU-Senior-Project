using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public float TheDistance;

    public bool met = false;

    [SerializeField]
    private GameObject ActionDisplay;

    [SerializeField]
    private GameObject ActionText;
    public GameObject ThePlayer;    
    
    public string NPCName;
    public GameObject NPCNameText;
    public Text NPCText;

    public GameObject ActionBtn;
    public GameObject ActionBtn2;

    TextMeshProUGUI Action;
    TextMeshProUGUI Action2;
    void Start()
    {
        ActionDisplay = GameObject.Find("ActionDisplay");
        ActionText = GameObject.Find("ActionText");
        Action = ActionBtn.GetComponentInChildren<TextMeshProUGUI>();
        Action2 = ActionBtn2.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void SetAction()
    {
        
        switch(this.gameObject.tag)
        {
            case "Shop":
                Action.text = "Shop";
                Action2.text = "Talk";
                break;
            case "Guard":
                Action.text = "Talk";
                ActionBtn2.SetActive(false);
                break;
            case "QuestGiver":
                Action.text = "Quest";
                Action2.text = "Talk";
                break;
            case "RandomNPC":
                Action.text = "Talk";
                ActionBtn2.SetActive(false);
                break;
        }
        
    }
    

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver () {
        if(TheDistance <= 3 && UiManagerScript.MyInstance.MyShopOpen != true)
        {
            ActionText.GetComponent<Text>().text = "Talk to " + NPCName;
            ActionDisplay.GetComponent<Text>().text = "[E]";
            //Crosshair.SetActive(false);
        }
        else{
            ActionText.GetComponent<Text>().text = "";
            ActionDisplay.GetComponent<Text>().text = "";
            //Crosshair.SetActive(true);
        }

        if(Input.GetButtonDown("Action") )
        {
            if(TheDistance <= 3 && UiManagerScript.MyInstance.MyShopOpen != true)
            {
                this.transform.LookAt(new Vector3(ThePlayer.transform.position.x, this.transform.position.y, ThePlayer.transform.position.z));
                UiManagerScript.MyInstance.OpenCloseNPC();
                ActionText.GetComponent<Text>().text = "";
                ActionDisplay.GetComponent<Text>().text = "";
                //ThePlayer.SetActive(false);
                NPCChoices.MyInstance.MyNPC = this;
                NPCGreet();

            }
        }
    }

    void OnMouseExit() {
        ActionText.GetComponent<Text>().text = "";
        ActionDisplay.GetComponent<Text>().text = "";
        //Crosshair.SetActive(true);
    }

    public void NPCGreet () {
        
        //yield return new WaitForSeconds(2.5f);
        if(met == false)
        {
            NPCText.text = "Hello there I don't believe we have met before. My name is "+NPCName +". How can I help you?";
            met = true;
        }
        else{
            NPCText.text = "Hello "+ PlayerStats.MyInstance.MyName + ". How can I help you?";            
        }
        SetAction();
        ActionText.GetComponent<Text>().text = "";
        ActionDisplay.GetComponent<Text>().text = "";        
    }

    public void Action1Execute()
    {
        string tmp = Action.text;
        if(tmp == "Shop")
        {
            this.gameObject.GetComponent<Container>().OpenShop();
        }
        else if(tmp == "Talk")
        {
            NPCText.text = "This will be where some dialogue line will be";//Say random dialogue line will set up later
        }
        else{
            Debug.Log("Something went wrong with action button 1");
        }
    }
    public void Action2Execute()
    {
        string tmp = Action2.text;
        if(tmp == "Talk")
        {
            NPCText.text = "This will be where some dialogue line will be";//Say random dialogue line will set up later
        }
    }

}

