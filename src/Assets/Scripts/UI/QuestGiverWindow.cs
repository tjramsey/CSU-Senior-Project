using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiverWindow : MonoBehaviour
{
    private QuestGiver questGiver;

    [SerializeField]
    private GameObject acceptBtn, completeBtn;

    [SerializeField]
    private Text questDescription;

    [SerializeField]
    private List<GameObject> quests = new List<GameObject>();

    private Quest selectedQuest;
    private static QuestGiverWindow instance;

    public static QuestGiverWindow MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<QuestGiverWindow>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject QuestPrefab;

    [SerializeField]
    private Transform questArea;

    public void NPCCompleteQuest(Quest quest)
    {
        foreach(GameObject go in quests)
        {
            if(go.GetComponent<QGQuestScript>().MyQuest == quest)
            {
                Destroy(go);
            }
        }
    }

    public void ShowQuests(QuestGiver questGiver)
    {
        this.questGiver = questGiver;
        foreach(GameObject go in quests)
        {
            Destroy(go);
        }


        foreach(Quest quest in questGiver.MyQuests)
        {
            if(quest != null){
            //Debug.Log(quest.MyTitle);
            GameObject go = Instantiate(QuestPrefab, questArea);
            go.GetComponent<QGQuestScript>().MyQuest = quest;
            go.GetComponent<Text>().text = quest.MyTitle;
            if(go.GetComponent<QGQuestScript>().MyQuest.MarkedSeen != true)
            {
                go.GetComponent<QGQuestScript>().MyQuest.MarkedSeen = true;
            }
            if(quest.TakenByPlayer)
            {
                acceptBtn.SetActive(false);
                if(!quest.IsComplete){
                    go.GetComponent<Text>().text += " (Your Quest)";
                }
                else{
                    go.GetComponent<Text>().text = quest.MyTitle + "Complete";
                }
            }
            else if (quest.IsTaken)
            {
                go.GetComponent<Text>().text += " (Taken By Others)";
                acceptBtn.SetActive(false);
            }
            else{
                acceptBtn.SetActive(true);
            }
            quests.Add(go);
            }
            
        }
    }

    public void ShowQuestInfo(Quest quest)
    {
        this.selectedQuest = quest;
        if(quest.IsTaken || quest.TakenByPlayer)
        {
            acceptBtn.SetActive(false);
            if(quest.TakenByPlayer && quest.IsComplete)
            {
                completeBtn.SetActive(true);
                acceptBtn.SetActive(false);
            }
        }
        else{
            acceptBtn.SetActive(true);
        }
        
        string title = quest.MyTitle;
        string description = quest.MyDescription;
        string objectives = string.Empty;
        
        foreach(Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType+ ": "+ obj.MyCurrentAmount + " / " + obj.MyTotalAmount+"\n";
        }
        foreach(Objective obj in quest.MyKillObjectives)
        {
            objectives += obj.MyType+ ": "+ obj.MyCurrentAmount + " / " + obj.MyTotalAmount+"\n";
        }
        string gold= quest.MyGold.ToString();
        string experience = quest.MyExperience.ToString();

        questDescription.text = string.Format("<b>{0}</b>\n{1}\n\nObjectives\n{2}<b>REWARDS</b>\n{3} Gold\n{4} Exp",title,description, objectives,gold,experience);
    }

    public void Accept()
    {
        QuestLog.MyInstance.AcceptQuest(selectedQuest);
        acceptBtn.SetActive(false);
        ShowQuests(this.questGiver);

    }

    public void Complete()
    {
        if(selectedQuest.IsComplete)
        {
            foreach (Quest quest in questGiver.MyQuests)
            {
                if(selectedQuest == quest)
                {
                    if(selectedQuest.MyGold != 0)
                    {
                        PlayerStats.MyInstance.PlayerGold += selectedQuest.MyGold;
                        PlayerStats.MyInstance.UpdatePlayerGold();
                    }
                    if(selectedQuest.MyExperience != 0)
                    {
                        PlayerStats.MyInstance.MyXp += selectedQuest.MyExperience;
                    }
                    questGiver.MyQuests.Remove(selectedQuest);
                    completeBtn.SetActive(false);
                    ShowQuests(this.questGiver);
                    break;
                }
            }
        }
    }

    

    // public void CheckTaken()
    // {
    //     foreach(GameObject go in quests)
    //     {
    //         QGQuestScript qs = go.GetComponent<QGQuestScript>();
    //         qs.IsTaken();
    //     }
    // }

    // public void TurnIn()
    // {
    //     if(QuestLog.MyInstance.MySelected != null)
    //     {
    //         if(QuestLog.MyInstance.MySelected.IsComplete)
    //         {
    //            // QuestLog.MyInstance.questScripts
    //         }
    //     }
    // }
}
