using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdController;
    public GameObject FirstController;

    public int CamMode;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Camera"))
        {
            if(CamMode == 1)
            {
                CamMode = 0;
            }
            else
            {
                CamMode += 1;
            }
            StartCoroutine(CamChange());
        }

    }
    
    IEnumerator CamChange() {
        yield return new WaitForSeconds(0.01f);
        if(CamMode == 0)
        {
            ThirdController.SetActive(true);
            FirstController.SetActive(false);
        }
        if(CamMode == 1)
        {
            ThirdController.SetActive(false);
            FirstController.SetActive(true);
        }
    }
}
