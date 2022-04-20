using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChoices : MonoBehaviour
{
    private static NPCChoices instance;

    public static NPCChoices MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<NPCChoices>();
            }
            return instance;
        }
    }
    
    private NPC npc;

    public NPC MyNPC {
        get { return npc;}
        set {npc = value;}
    }
    public void Action1()
    {
        npc.Action1Execute();
    }
    public void Action2()
    {
        npc.Action2Execute();
    }
    public void ExitExecute()
    {
        UiManagerScript.MyInstance.OpenCloseNPC();
    }
}
