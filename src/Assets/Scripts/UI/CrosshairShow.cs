using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairShow : MonoBehaviour
{
    // Update is called once per frame
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Cross1;
    public GameObject Cross2;
    
    void Update()
    {
        if(ActionDisplay.GetComponent<Text>().text != string.Empty)
        {
            Cross1.SetActive(false);
            Cross2.SetActive(false);
        }
        else{
            Cross1.SetActive(true);
            Cross2.SetActive(true);
        }
    }
}
