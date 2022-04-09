using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QGQuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    private bool MarkedTaken = false;

    

    public void Select()
    {
        QuestGiverWindow.MyInstance.ShowQuestInfo(MyQuest);
    }
    public void IsTaken()
    {
        Debug.Log("Is it complete : "+MyQuest.IsTaken);
        if(MyQuest.IsTaken && MarkedTaken == false)
        {
            if(MyQuest.TakenByPlayer){
                GetComponent<Text>().text += ("Taken");
            }
            else{
                GetComponent<Text>().text += ("Unavailable");
            }
            MarkedTaken = true;
        }
        else if (!MyQuest.IsComplete)
        {
            MarkedTaken =false;
            GetComponent<Text>().text = MyQuest.MyTitle;
        }
    }
    
}
