using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;


[System.Serializable]
public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private List<Quest> quests = new List<Quest>();

    public List<Quest> MyQuests
    {
        get {return quests;}
    }

    [SerializeField]
    private int MaxQuests;

    public float TheDistance;
    [SerializeField]
    private GameObject ActionDisplay;

    [SerializeField]
    private GameObject ActionText;

    
    public TimeController time;
    public double questDay;

    [SerializeField]
    private int questGiverID;

    public int MyId
    {
        get{ return questGiverID;}
    }

    [SerializeField]
    private bool questGiverChecked;

    public bool MyChecked 
    {
        get {return questGiverChecked;}
        set {questGiverChecked = value;}
    }

    private System.DateTime internalTime;

    public System.DateTime MyTime 
    {
        get { return internalTime;}
        set { internalTime = value;}
    }

    public bool loadedGame;

    
    void Start()
    {
        ActionDisplay = GameObject.Find("ActionDisplay");
        ActionText = GameObject.Find("ActionText");
        questGiverChecked = false;
        internalTime = TimeController.MyInstance.getCurrentDay();
        MaxQuests = 5;
        //GenerateQuests();
    }

    void Update()
    {
        
        TheDistance = PlayerCasting.DistanceFromTarget; 
        if(quests.Count < MaxQuests)
        {
            CheckQuestGeneration();
        } 
       // Debug.Log(internalTime.ToString() + " : " +TimeController.MyInstance.getCurrentDay().ToString());
    }

    void OnMouseOver () {
        if(TheDistance <= 3)
            {
                if(UiManagerScript.MyInstance.MyCursorLocked == true){
                    ActionText.GetComponent<Text>().text = "Look at Quests";
                    ActionDisplay.GetComponent<Text>().text = "[E]";
                }
                if(Input.GetButtonDown("Action"))
                {
                    double temp = getDaysPassed();
                    CheckQuest();
                    if(questGiverChecked == false){
                        CheckQuestsToBeTaken();
                        CheckQuestCompletion();
                        internalTime = TimeController.MyInstance.getCurrentDay();
                    }

                    UiManagerScript.MyInstance.OpenCloseQuestGiver();
                    UiManagerScript.MyInstance.OpenCloseQuestLog();
                    QuestGiverWindow.MyInstance.ShowQuests(this);
                    ActionText.GetComponent<Text>().text = "";
                    ActionDisplay.GetComponent<Text>().text = "";
                    
                }
            }
        if(TheDistance > 3)
            {
                ActionText.GetComponent<Text>().text = "";
                ActionDisplay.GetComponent<Text>().text = "";
            }
    }

    void OnMouseExit() {
        ActionDisplay.GetComponent<Text>().text = "";
        ActionText.GetComponent<Text>().text = "";
    }

    private void Awake()
    {
        //internalTime = TimeController.MyInstance.getCurrentDay();

    }

    private double getDaysPassed()
    {
        System.TimeSpan elapsed= TimeController.MyInstance.getCurrentDay().Subtract(internalTime);
        double daysPassed = elapsed.TotalDays;
        questDay = daysPassed;
        return daysPassed;
    }



    private void GenerateQuests()
    {
        int numOfQuest = 5 - MyQuests.Count;


        for(int i = 0; i < numOfQuest; i++)
        {
            string QuestType = "";
            string ObjectiveType = "";
            string QuestName = "";
            string QuestDesc = "";
            Quest newQuest;
            int select = UnityEngine.Random.Range(1,3);
            switch(select){
                case 1:
                    QuestType = "Collect";
                    ObjectiveType = GenerateCollectObjective();
                    QuestName = QuestType + " " + ObjectiveType;
                    QuestDesc = GenerateDescription(QuestType, ObjectiveType);
            
                    newQuest = new Quest(QuestName, QuestDesc, 10,15);
                    newQuest.MyQuestGiverId = this.MyId;
                    CollectObjective newCollectObjective = new CollectObjective(ObjectiveType, 5);
                    newQuest.AddObjectives(newCollectObjective, QuestType);
                    MyQuests.Add(newQuest);
                    newQuest.MyId = MyQuests.IndexOf(newQuest);
                    newQuest.MyCreationDate = TimeController.MyInstance.getCurrentDay();
                    Debug.Log("Creation date: " + newQuest.MyCreationDate.ToString());
                    break;
                case 2:
                    QuestType = "Kill";
                    ObjectiveType = GenerateKillObjective();
                    QuestName = QuestType + " " + ObjectiveType;
                    QuestDesc = GenerateDescription(QuestType, ObjectiveType);
            
                    newQuest = new Quest(QuestName, QuestDesc, 10,15);
                    newQuest.MyQuestGiverId = this.MyId;
                    KillObjective newKillObjective = new KillObjective(ObjectiveType, 5);
                    newQuest.AddObjectives(newKillObjective, QuestType);
                    MyQuests.Add(newQuest);
                    newQuest.MyId = MyQuests.IndexOf(newQuest);
                    newQuest.MyCreationDate = TimeController.MyInstance.getCurrentDay();
                    Debug.Log("Creation date: " + newQuest.MyCreationDate.ToString());
                    break;
                
            }
            
                
        }
    }
    private string GenerateCollectObjective()
    {
        int typeNum = UnityEngine.Random.Range(1,4);

        switch(typeNum){
            case 1:
                return "Bread";
            case 2:
                return "Apple";
            case 3:
                return "Healthpotion";
            return "";
        }
        return "";
    }

    private string GenerateKillObjective()
    {
        int typeNum = UnityEngine.Random.Range(1,4);

        switch(typeNum){
            case 1:
                return "Spider";
            case 2:
                return "Goblin";
            case 3:
                return "Wolf";
            return "";
        }
        return "";
    }

    private string GenerateDescription(string qType, string oType)
    {
        string description = "I humbly ask that someone " + qType + " 5 " + oType + " for me";
        return description;
    }



    public void CheckQuestsToBeTaken()
    {
        Debug.Log("Check to be taken:");
        foreach (Quest quest in quests)
        {
            if(quest.MarkedSeen == true && quest.IsTaken != true && quest.TakenByPlayer != true)
            {
                System.TimeSpan elapsed= TimeController.MyInstance.getCurrentDay().Subtract(quest.MyCreationDate);
                Double days = elapsed.TotalDays;
                // Debug.Log(days);

                if(days >= 1 &&  questGiverChecked == false)
                {
                    int randAct = UnityEngine.Random.Range(1,10);
                    if(randAct > 6)
                    {
                        quest.IsTaken = true;
                        quest.MyStartDate = TimeController.MyInstance.getCurrentDay();
                        Debug.Log(quest.MyStartDate.ToString() + " start date : " + quest.MyTitle);
                    }
                }
            }
        }
    }

    public void CheckQuestGeneration()
    {
        if(getDaysPassed() >= 1 && UiManagerScript.MyInstance.MyQuestGiverOpen == false)
        {
            if(quests.Count < MaxQuests)
            {
                GenerateQuests();
            }
        }
        else if(loadedGame != true)
        {
            GenerateQuests();
            loadedGame = true;
        }
    }

    private void CheckQuestCompletion()
    {
        foreach (Quest quest in quests)
        {
            if(quest.IsTaken == true &&quest.TakenByPlayer != true)
            {
                System.TimeSpan elapsed= TimeController.MyInstance.getCurrentDay().Subtract(quest.MyStartDate);
                Double days = elapsed.TotalDays;
                Debug.Log(elapsed.ToString() + " : " + quest.MyTitle);

                if(days > .25)
                {
                    int randAct = UnityEngine.Random.Range(1,100);
                    if(randAct > 60)
                    {
                        QuestGiverWindow.MyInstance.NPCCompleteQuest(quest);
                        quests.Remove(quest);
                        Debug.Log(quest.MyTitle + " Completed by NPC");
                    }
                    else if(randAct > 30)
                    {
                        quest.IsTaken = false;
                        Debug.Log(quest.MyTitle + " Failed by NPC");
                    }
                    else{
                        Debug.Log(quest.MyTitle + " in progress by NPC");

                    }

                }
            }
        }
    }

    private void CheckQuest()
    {
        if(getDaysPassed() >=1)
        {
            questGiverChecked = false;
        }
        else{
            questGiverChecked = true;
        }
    }
    
}
    
    // public GameObject player;

    // public QuestManager manager;
    

    // public GameObject questWindow;
    // private Text titleText;
    // public Text rewardsText;
    // public Text locationText;
    // public Text objectiveText;
    // private int rand;
    // public int questAmount;
    // private int difficulty = 1;
    // private int questOpen;
    // public string Time;
    // public int checkQuest = 0;
    // public int count;
    // public GameObject ExMark;




     
    
    // public List<Quest> QuestGiverList = new List<Quest>();
    // public Quest current;
    // private int currIndex = 0;
    // 

   

    // public void CreateQuest(string title, int gR, int eR, string Objective, string location, GoalType questGoal, int Amount, string enemy, string item)
    // {
    //     Quest newQuest = new Quest();
    //     newQuest.title = title;
    //     newQuest.goldReward = gR;
    //     newQuest.experienceReward = eR;
    //     newQuest.Objective = Objective;
    //     newQuest.Location = location;
    //     newQuest.questGoal.goalType = questGoal;
    //     newQuest.questGoal.requiredAmount = Amount;
    //     newQuest.questGoal.requiredEnemy = enemy;
    //     newQuest.questGoal.requiredItem = item;
    //     QuestGiverList.Add(newQuest);
        
    // }

    // public void CreateRandomQuest()
    // {
    //     Quest newQuest = new Quest();
    //     newQuest.title = "Temporary";
    //     int goal = UnityEngine.Random.Range(1,2);
    //     GoalType questGoal;
    //     string qG;
    //     if(goal == 1)
    //     {
    //         questGoal = GoalType.Kill;
    //         qG = "Kill";
    //     }
    //     else{
    //         questGoal = GoalType.Gathering;
    //         qG = "Gather";
    //     }
        
    //     if(difficulty == 1)
    //     {
    //         newQuest.goldReward = UnityEngine.Random.Range(10,35);
    //         newQuest.experienceReward = UnityEngine.Random.Range(50,100);
    //        // newQuest.questGoal.requiredAmount = UnityEngine.Random.Range(3,7);
    //         if(goal == 1)
    //             newQuest.questGoal.requiredEnemy = "Spiders";
    //         if(goal == 2)
    //             newQuest.questGoal.requiredItem = "Apples";

    //     }
    //     newQuest.Objective = qG + /*" " + newQuest.questGoal.requiredAmount + " " +*/newQuest.questGoal.requiredEnemy;
    //     newQuest.questGoal.goalType = questGoal;

    //     QuestGiverList.Add(newQuest);
    //     print("created random quest");

        
    // }

    // public void OpenQuestWindow()
    // {

    //     questWindow.SetActive(true);
    //     current = QuestGiverList[currIndex];
    //    // titleText.text = current.title;
    //     rewardsText.text = current.goldReward.ToString() + " Gold\n" + current.experienceReward.ToString() + " Experience\n" + current.extraReward;
    //     objectiveText.text = current.Objective;
    //     locationText.text = current.Location;
    // }

    // public void NextQuest()
    // {
    //     questWindow.SetActive(false);
    //     if(currIndex+1 < QuestGiverList.Count)
    //         currIndex = currIndex+1;
    //     questWindow.SetActive(true);
    //     OpenQuestWindow();
    // }

    // public void PrevQuest()
    // {
    //     questWindow.SetActive(false);
    //     if(currIndex-1 >= 0)
    //         currIndex = currIndex-1;
    //     OpenQuestWindow();
    // }

    // public void AcceptQuest()
    // {
    //     if (current.taken == false)
    //     {
    //         questWindow.SetActive(false);
    //         current.takenByPlayer = true;
	// 		Cursor.lockState = CursorLockMode.Locked;
    //         Cursor.visible = false;
    //         player.GetComponent<FirstPersonController>().enabled = true;
    //         questOpen = 0;
    //         manager.QuestList.Add(current);
    //         QuestGiverList.Remove(current);

    //     }
    // }

// }
