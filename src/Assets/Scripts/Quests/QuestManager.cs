using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static int ActiveQuestNumber;
    public int InternalQuestNumber; 
	public List<Quest> QuestList = new List<Quest>();
    public List<Quest> ActiveQuest = new List<Quest>();

    void Update () {
        InternalQuestNumber = ActiveQuestNumber;
    }

    
}

public class QuestGoal
{
    public GoalType goalType;
    
    public int requiredAmount;
    public string requiredItem;
    public string requiredEnemy;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled(string enemy)
    {
        if(goalType == GoalType.Kill)
            if(enemy == requiredEnemy)
                currentAmount++;
    }

    public void ItemCollected(string item)
    {
        if(goalType == GoalType.Gathering)
            if(item == requiredItem)
                currentAmount++;
    }

}

public enum GoalType
{
    Kill,
    Gathering
}
