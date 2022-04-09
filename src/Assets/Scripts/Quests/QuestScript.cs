using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestScript : MonoBehaviour
{
    
    public Quest MyQuest{ get; set;}

    private bool MarkedComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        GetComponent<Text>().color = Color.red;
        QuestLog.MyInstance.ShowDescription(MyQuest);
        Debug.Log("Selected");
    }

    public void Deselect()
    {
        GetComponent<Text>().color = Color.black;
    }
    public void IsComplete()
    {
        Debug.Log("Is it complete : "+MyQuest.IsComplete);
        if(MyQuest.IsComplete && MarkedComplete == false)
        {
            GetComponent<Text>().text += ("Complete");
            MarkedComplete = true;
        }
        else if (!MyQuest.IsComplete)
        {
            MarkedComplete =false;
            GetComponent<Text>().text = MyQuest.MyTitle;
        }
    }
    
}
