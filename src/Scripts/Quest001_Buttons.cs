﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest001_Buttons : MonoBehaviour
{
    public GameObject ThePlayer;
    public GameObject NoticeCam;
    public GameObject UIQuest;
    public GameObject ActiveQuestBox;
    public GameObject Objective01;
    public GameObject Objective02;
    public GameObject Objective03;
    public GameObject ExMark;
    public GameObject TheNotice;
    public GameObject NoticeTrigger;

    public void AcceptQuest () {
        ThePlayer.SetActive(true);
        NoticeCam.SetActive(false);
        UIQuest.SetActive(false);
        StartCoroutine(AcceptQuestUI());
    }

    IEnumerator AcceptQuestUI() {
        ExMark.SetActive(false);
        TheNotice.SetActive(false);
        NoticeTrigger.SetActive(false);
        ActiveQuestBox.GetComponent<Text>().text = "My First Weapon";
        Objective01.GetComponent<Text>().text = "Reach the clearing in the woods";
        Objective02.GetComponent<Text>().text = "Investigate the chest";
        Objective03.GetComponent<Text>().text = "Retrieve the weapon";
        QuestManager.ActiveQuestNumber = 1;
        yield return new WaitForSeconds(0.5f);
        ActiveQuestBox.SetActive(true);
        yield return new WaitForSeconds(1);
        Objective01.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective02.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Objective03.SetActive(true);
        yield return new WaitForSeconds(9);
        ActiveQuestBox.SetActive(false);
        Objective01.SetActive(false);
        Objective02.SetActive(false);
        Objective03.SetActive(false);

    }

    public void DeclineQuest() {
        ThePlayer.SetActive(true);
        NoticeCam.SetActive(false);
        UIQuest.SetActive(false);
    }
}
