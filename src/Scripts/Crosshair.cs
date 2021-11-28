using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairShow : MonoBehaviour
{
    // Update is called once per frame
    public GameObject ActionDisplay;
    public GameObject ActionText;
    public GameObject Crosshair;
    void Update()
    {
        if(ActionDisplay.activeSelf == true)
        {
            Crosshair.SetActive(false);
        }
        else{
            Crosshair.SetActive(true);
        }
    }
}
