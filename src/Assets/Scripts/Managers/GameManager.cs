using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void KillConfirmed(Enemy enemy);
public class GameManager : MonoBehaviour
{
    public List<GameObject> WorldObjects = new List<GameObject>();
    public event KillConfirmed killConfirmedEvent;
    private static GameManager instance;

    public static GameManager MyInstance
    {
        get {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    public void OnKillConfirmed(Enemy enemy)
    {
        if(killConfirmedEvent != null)
        {
            killConfirmedEvent(enemy);
        }
    }
}
