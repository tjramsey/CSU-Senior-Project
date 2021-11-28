using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC001 : MonoBehaviour
{
    public float TheDistance;
    public int Talking = 0;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject ThePlayer;    
    public GameObject TextBox;
    public GameObject NPCName;
    public GameObject NPCText;
    public GameObject Crosshair;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;
    }

    void OnMouseOver () {
        if(TheDistance <= 3 && Talking == 0)
        {
            ActionText.GetComponent<Text>().text = "Talk";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
            Crosshair.SetActive(false);
        }
        else{
            ActionDisplay.SetActive(false);
            ActionText.SetActive(false);
            Crosshair.SetActive(true);
        }

        if(Input.GetButtonDown("Action"))
        {
            if(TheDistance <= 3)
            {
                Talking = 1;
                AttackBlocker.BlockSword = 1;
                Screen.lockCursor = false;
                Cursor.visible = true;
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                //ThePlayer.SetActive(false);
                StartCoroutine (NPC001Active ());
            }
        }
    }

    void OnMouseExit() {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
        Crosshair.SetActive(true);
    }

    IEnumerator NPC001Active () {
        TextBox.SetActive (true);
        NPCName.GetComponent<Text>().text = "Julia";
        NPCName.SetActive(true);
        NPCText.GetComponent<Text>().text = "Hello friend, I may have a quest for you if you wish to accept it. Please come back later this afternoon.";
        NPCText.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        NPCName.SetActive(false);
        NPCName.SetActive(false);
        TextBox.SetActive(false);
        Talking = 0;
        ActionDisplay.SetActive(true);
        ActionText.SetActive(true);        
    }
}

