using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class CharacterCreationWindow : MonoBehaviour
{
    // Start is called before the first frame update
    private static CharacterCreationWindow instance;


    public static CharacterCreationWindow MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CharacterCreationWindow>();
            }
            return instance;

        }
    }

    [SerializeField]
    private GameObject dialogue;

    [SerializeField]
    private SkillScript[] skills;

    [SerializeField]
    private ClassScript selected;

    [SerializeField]
    private Text NameTextField;

    [SerializeField]
    private ClassScript[] classes;

    public int AvailablePoints;
    
    [SerializeField]
    private TextMeshProUGUI APointsText;

    public void Awake()
    {
        selected = null;
        AvailablePoints = 10;
        UpdateAvailablePoints();
    }

    public void UpdateStats(ClassScript charClass)
    {
        if(selected != null)
        {
            selected.Deselect();
        }
        selected = charClass;
        for(int i = 0; i < skills.Length; i++)
        {
            skills[i].SetSkill(selected.Skills[i].value);
        }
    }
    public void UpdateAvailablePoints()
    {
        APointsText.text = AvailablePoints.ToString();
    }

    public void Submit()
    {
        PlayerPrefs.SetString("CharacterName", NameTextField.text);
        PlayerPrefs.SetInt("Agility", skills[0].TotalSkillPoints);
        PlayerPrefs.SetInt("Charisma", skills[1].TotalSkillPoints);
        PlayerPrefs.SetInt("Endurance", skills[2].TotalSkillPoints);
        PlayerPrefs.SetInt("Intellect", skills[3].TotalSkillPoints);
        PlayerPrefs.SetInt("Strength", skills[4].TotalSkillPoints);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void OpenDialogue()
    {
        if(selected != null)
            dialogue.SetActive(true);
    }
    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }
}
