using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavedGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateTime;
    [SerializeField]
    private TextMeshProUGUI charName;
    [SerializeField]
    private TextMeshProUGUI charLevel;
    [SerializeField]
    private GameObject visuals;

    [SerializeField]
    private int index;

    public int MyIndex
    {
        get{ return index;}
    }

    private void Awake()
    {
        visuals.SetActive(true);
    }

    public void ShowInfo(SaveData saveData)
    {
        //visuals.SetActive(true);
        dateTime.text = "Date: " + saveData.MyDateTime.ToString("dd/MM/yyy") + " - Time: " + saveData.MyDateTime.ToString("HH:mm");
        charLevel.text = saveData.MyPlayerData.MyLevel.ToString();
        charName.text = "Name: "+saveData.MyPlayerData.MyName;
    }

    public void HideInfo()
    {
        Debug.Log("HideInfo");
        dateTime.text = "Date: ";
        charLevel.text = "0";
        charName.text = "Name: ";
    }
}
