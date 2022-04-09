using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q001_Object03 : MonoBehaviour
{
    public float TheDistance;
    public GameObject FakeSword;
    public GameObject RealSword;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject TheObjective;
    public int CloseObjective;
    public GameObject ChestBlock;
    public GameObject QuestComplete;
    public GameObject ExMark;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        if(CloseObjective == 3){
            if(TheObjective.transform.localScale.y <= 0.0f)
            {
                CloseObjective = 0;
                TheObjective.SetActive(false);
            }
            else{
            TheObjective.transform.localScale -= new Vector3(0.0f, 0.1f, 0.0f);
            }
        }
    }

    void OnMouseOver () {
        if(TheDistance <= 3)
        {
            ActionText.GetComponent<Text>().text = "Take Sword";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }

        if(Input.GetButtonDown("Action"))
        {
            if(TheDistance <= 3)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                FakeSword.SetActive(false);
                RealSword.SetActive(true);
                StartCoroutine (FinishObjective());
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
                ChestBlock.SetActive(true);
                ExMark.SetActive(true);
                QuestComplete.SetActive(true);
            }
        }
    }

    IEnumerator FinishObjective() {
        TheObjective.SetActive(true);
        yield return new WaitForSeconds(1f);
        CloseObjective = 3;
    }

    void OnMouseExit() {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }




}
