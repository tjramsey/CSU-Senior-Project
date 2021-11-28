using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLevel : MonoBehaviour
{
    public static int CurrentLevel = 1;
    public int InternalLevel;

    void Update()
    {
        if(GlobalExp.CurrentExp >= GlobalExp.NextLevelExp)
        {
            CurrentLevel += 1;
        }
        InternalLevel = CurrentLevel;
    }
}

