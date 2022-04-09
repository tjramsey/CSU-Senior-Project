using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene001Skip : MonoBehaviour
{
    public GameObject FadeIn;

    void Start()
    {
        StartCoroutine (FadeQuit ());
    }

    IEnumerator FadeQuit () {
        yield return new WaitForSeconds (1);
        FadeIn.SetActive (false);
    }

    
}
