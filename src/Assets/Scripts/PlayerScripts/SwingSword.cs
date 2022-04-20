using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour
{
    //public GameObject TheSword;
    public int SwordStatus;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && SwordStatus == 0 && UiManagerScript.MyInstance.MenuOpen == false){
            StartCoroutine (SwingSwordFunction());
        } 
 
    }
    
    IEnumerator SwingSwordFunction() {
        SwordStatus = 1;
        this.GetComponent<Animation>().Play("SwordSwing");
        SwordStatus = 2;
        yield return new WaitForSeconds (1.0f);
        SwordStatus = 0;

    }
}
