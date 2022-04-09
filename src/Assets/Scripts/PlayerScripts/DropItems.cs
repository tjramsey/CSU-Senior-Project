using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    private static DropItems instance;

    public static DropItems MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<DropItems>();
            }
            return instance;
        }
    }
    public GameObject dropArea;
    private Vector3 spawnObjectPostion;

    void Update()
    {

    }
    
    // public void dropItem(Item item)
    // {
    //     Debug.Log("Player Position: x = " + dropArea.transform.position.x + " y = "+ dropArea.transform.position.y + " z = " + dropArea.transform.position.z);
    //     GameObject newItem = Instantiate(item.MyPrefab);
    //     newItem.transform.position = new Vector3(dropArea.transform.position.x,dropArea.transform.position.y, dropArea.transform.position.z);
    // }
}
