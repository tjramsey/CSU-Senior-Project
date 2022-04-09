using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    [SerializeField]
    private Quest selected;

    public Quest MySelected
    {
        get
        {
            return selected;
        }
    }

    private List<QuestScript> questScripts = new List<QuestScript>();

    public List<QuestScript> MyQuestScripts
    {
        get { return questScripts;}
    }

    private List<Quest> quests = new List<Quest>();

    public List<Quest> MyQuests
    {
        get { return quests;}
    }
    
    [SerializeField]
    private Text questDescription;

    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;

        }
    }
    public void Awake()
    {
        selected = null;
    }

    public void AcceptQuest(Quest quest)
    {
        foreach (CollectObjective CO in quest.MyCollectObjectives)
        {
            InventoryScript.MyInstance.ItemCountChangedEvent += new ItemCountChanged(CO.UpdateItemCount);
            CO.UpdateItemCount();
        }
        foreach (KillObjective KO in quest.MyKillObjectives)
        {
            GameManager.MyInstance.killConfirmedEvent += new KillConfirmed(KO.UpdateKillCount);
        }

        GameObject go = Instantiate(questPrefab,questParent);
        //Debug.Log("go instantiated");
        QuestScript qs = go.GetComponent<QuestScript>();
        //Debug.Log("qs created");
        qs.MyQuest = quest;
        //Debug.Log(qs.MyQuest.MyTitle + " : qs" + quest.MyTitle); 
        qs.MyQuest.TakenByPlayer = true;
        quest.MyQuestScript = qs;
        go.GetComponent<Text>().text = quest.MyTitle;
        questScripts.Add(qs);
        quests.Add(quest);
        CheckCompletion();
    }

    public void ShowDescription(Quest quest)
    {
        if(selected != null)
        {
            selected.MyQuestScript.Deselect();
        }
        
        
        selected = quest;

        if(quest == null)
        {
            Debug.Log("Quest is not being set");
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

        // for future use
        // if(quest.IsComplete == true)
        // {
        //     CompletionMark.SetActive(true);
        // }
        // else
        // {
        //     CompletionMakr.SetActive(false);
        // }

    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    

    public void CheckCompletion()
    {
        foreach(QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    
}
