using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Q001_Object02 : MonoBehaviour
{
    public float TheDistance;
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Container;
    public GameObject TheObjective;
    public int CloseObjective;
    public GameObject TakeSword;

    void Update()
    {
        TheDistance = PlayerCasting.DistanceFromTarget;

        if(CloseObjective == 2){
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
            ActionText.GetComponent<Text>().text = "Open Chest";
            ActionDisplay.SetActive(true);
            ActionText.SetActive(true);
        }

        if(Input.GetButtonDown("Action"))
        {
            if(TheDistance <= 3)
            {
                this.GetComponent<BoxCollider>().enabled = false;
                Container.GetComponent<Animation>().Play("Q01_ChestOpen");
                StartCoroutine(FinishObjective());
                TakeSword.SetActive(true);
                ActionDisplay.SetActive(false);
                ActionText.SetActive(false);
            }
        }
    }
    IEnumerator FinishObjective() {
        TheObjective.SetActive(true);
        yield return new WaitForSeconds(1f);
        CloseObjective = 2;
    }

    void OnMouseExit() {
        ActionDisplay.SetActive(false);
        ActionText.SetActive(false);
    }




}

