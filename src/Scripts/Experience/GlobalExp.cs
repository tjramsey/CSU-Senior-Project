using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalExp : MonoBehaviour
{
    public static int CurrentExp = 0;
    public static int NextLevelExp = 10;
    public int InternalExp;

    void Update()
    {
        NextLevelExp = (GlobalLevel.CurrentLevel * 10);
        InternalExp = CurrentExp;
    }
}
