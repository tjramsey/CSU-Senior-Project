using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadManager : MonoBehaviour
{

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private TextMeshProUGUI dialogueText;
    private string Action;

    [SerializeField]
    private SavedGame[] saveSlots;

    private SavedGame current;


    private void Awake()
    {
        foreach(SavedGame saved in saveSlots)
        {
            ShowSavedFiles(saved);
        } 
    }

    private void ShowSavedFiles(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat"))
        {
            Debug.Log(savedGame+ " found");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
            savedGame.ShowInfo(data);
        }
        else
        {
            savedGame.HideInfo();
        }
    }


    public void ShowDialogue(GameObject clickButton)
    {
        Action = clickButton.name;

        switch(Action)
        {
            case "Load":
                dialogueText.text = "Load " + clickButton.GetComponentInParent<SavedGame>().name +"?";
                break;
            case "Delete":
                dialogueText.text = "Delete "+ clickButton.GetComponentInParent<SavedGame>().name +"?";
                break;
        }
        current = clickButton.GetComponentInParent<SavedGame>();
        dialogue.SetActive(true);

    }
    public void ExecuteAction()
    {
        switch(Action)
        {
            case "Load":
                LoadScene(current);
                break;
            case "Delete":
                Delete(current);
                break;
        }
        CloseDialogue();
    }

    private void LoadScene(SavedGame savedGame)
    {
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat", FileMode.Open);
                SaveData data = (SaveData)bf.Deserialize(file);
                file.Close();
                PlayerPrefs.SetInt("Load", savedGame.MyIndex);
                SceneManager.LoadScene(data.MyScene);
            }
    }
    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    private void Delete(SavedGame savedGame)
    {
        Debug.Log("Delete" + savedGame.name);
        if(File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat")){
            File.Delete(Application.persistentDataPath + "/" + savedGame.gameObject.name+".dat");
            savedGame.HideInfo();
        }
    }
}
