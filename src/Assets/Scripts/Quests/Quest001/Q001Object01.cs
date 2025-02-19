﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Q001Object01 : MonoBehaviour
{
    public GameObject TheObjective;
    public int CloseObjective;

    void Update()
    {
        if(CloseObjective == 1){
            if(TheObjective.transform.localScale.y <= 0.0f)
            {
                CloseObjective = 0;
                TheObjective.SetActive(false);
            }
            else{
            TheObjective.transform.localScale -= new Vector3(0.0f, 0.1f, 0.0f);
            }
        }
        
    }

    void OnTriggerEnter(){
        StartCoroutine(FinishObjective());
        
    }

    IEnumerator FinishObjective() {
        TheObjective.SetActive(true);
        yield return new WaitForSeconds(1f);
        CloseObjective = 1;
    }
}
