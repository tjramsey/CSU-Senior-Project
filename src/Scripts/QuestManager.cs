using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static int ActiveQuestNumber;
    public int InternalQuestNumber;

    void update () {
        InternalQuestNumber = ActiveQuestNumber;
    }
}
