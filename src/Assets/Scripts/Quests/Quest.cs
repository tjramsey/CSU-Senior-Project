using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Quest 
{    

    [SerializeField]
    private string title;
    [SerializeField]
    private string description;
    [SerializeField]
    private int goldReward;
    [SerializeField]
    private int experienceReward;
    [SerializeField]
    private int id;

    public int MyId
    {
        get { return id;}
        set { id = value;}
    }
    [SerializeField]
    private int questGiverId;

    public int MyQuestGiverId
    {
        get { return questGiverId;}
        set { questGiverId = value;}
    }

    private bool taken;
    private bool takenByPlayer;

    private bool markedSeen;

    public bool MarkedSeen 
    {
        get { return markedSeen;}
        set {markedSeen = value;}
    }

    private static System.DateTime startDate;

    public System.DateTime MyStartDate
    {
        get {return startDate;}
        set {startDate = value;}
    }

    private static System.DateTime creationDate;

    public System.DateTime MyCreationDate
    {
        get {return creationDate;}
        set {creationDate = value;}
    }

    public System.DateTime checkedDate;


    

    // [SerializeField]
    // private CollectObjective[] collectObjectives;

    [SerializeField]
    private List<CollectObjective>collectObjectives = new List<CollectObjective>();

    [SerializeField]
    private List<KillObjective>killObjectives = new List<KillObjective>();

    public Quest (string qtitle, string qdescription, int qgold, int qexp)
    {
        MyTitle = qtitle;
        MyDescription = qdescription;
        MyGold = qgold;
        MyExperience = qexp;
    }

    public QuestScript MyQuestScript {get; set;}

    public string MyTitle
    {
        get{return title;}
        set{title = value;}
    }
    public string MyDescription
    {
        get{return description;}
        set{description = value;}
    }
    public int MyGold
    {
        get{return goldReward;}
        set{goldReward = value;}
    }
    public int MyExperience
    {
        get{return experienceReward;}
        set{experienceReward = value;}
    }
    // public string MyItem
    // {
        //get{return otherReward;}
    //  set{otherReward = value;}
    //}
    // public CollectObjective[] MyCollectObjectives
    // {
    //     get{return collectObjectives;}
    // }
    public List<CollectObjective> MyCollectObjectives
    {
        get { return collectObjectives;}
        set { collectObjectives = value;}
    }
    public List<KillObjective> MyKillObjectives
    {
        get { return killObjectives;}
        set { killObjectives = value;}
    }


    public bool IsComplete
    {
        get
        {
            foreach (Objective o in MyCollectObjectives)
            {
                if(!o.IsComplete)
                {
                    return false;
                }
            }
            foreach (Objective o in MyKillObjectives)
            {
                if(!o.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public bool IsTaken
    {
        get
        {
            return taken;
        }
        set
        {
            taken = value;
        }
    }
    public bool TakenByPlayer
    {
        get
        {
            return takenByPlayer;
        }
        set{
            takenByPlayer = value;
        }
    }


    public void AddObjectives(Objective o, string typeOfObjective)
    {
        if(typeOfObjective == "Collect")
            MyCollectObjectives.Add((o as CollectObjective));
        else if (typeOfObjective == "Kill")
            MyKillObjectives.Add((o as KillObjective));
        else{
            Debug.Log("Objective type not found");
        }
    }
    


}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount; 
    public int MyTotalAmount
    {
        get{return amount;}
        set{amount = value;}
    }

    private int currentAmount;
    public int MyCurrentAmount
    {
        get{return currentAmount;}
        set{currentAmount = value;}
    }

    [SerializeField]
    private string type;
    public string MyType
    {
        get{return type;}
        set{type = value;}
    }

    public bool IsComplete
    {
        get {
            return MyCurrentAmount >= MyTotalAmount;
        }
    }




}

[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount(ItemObject item)
    {
        Debug.Log(MyType.ToLower()+ "=" + item.MyTitle.ToLower());
        if(MyType.ToLower() == item.MyTitle.ToLower())
        {
            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(item.MyTitle);
            Debug.Log(MyCurrentAmount);
            if(QuestLog.MyInstance.MySelected != null)
            {
                QuestLog.MyInstance.UpdateSelected();
            }

            QuestLog.MyInstance.CheckCompletion();
        }
    }
    public CollectObjective(string DesiredType, int DesiredAmount)
    {
        MyType = DesiredType;
        MyTotalAmount = DesiredAmount;
    }
    public void UpdateItemCount()
    {
        MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(MyType);

        QuestLog.MyInstance.CheckCompletion();
        //QuestLog.MyInstance.UpdateSelected();   
    }
}

[System.Serializable]
public class KillObjective : Objective
{
    
    public KillObjective(string DesiredType, int DesiredAmount)
    {
        MyType = DesiredType;
        MyTotalAmount = DesiredAmount;
    }
    public void UpdateKillCount(Enemy enemy)
    {
        if(MyType == enemy.MyType.ToString())
        {
            MyCurrentAmount++;

            QuestLog.MyInstance.CheckCompletion();
            //QuestLog.MyInstance.UpdateSelected();  
        }
    }
    
}

