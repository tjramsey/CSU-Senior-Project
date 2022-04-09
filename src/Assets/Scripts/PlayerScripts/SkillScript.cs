using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI SkillNameText;

    [SerializeField]
    private string SkillName;

    [SerializeField]
    private TextMeshProUGUI SkillPointText;

    [SerializeField]
    private int SkillPoints;

    public Attributes type;

    [SerializeField]
    private TextMeshProUGUI TotalSkillPointsText;

    public int TotalSkillPoints;

    public int ClassSkillPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        SkillName = SkillNameText.text;
    }

    public void IncreaseSkill()
    {
        if(CharacterCreationWindow.MyInstance.AvailablePoints > 0 && CharacterCreationWindow.MyInstance.AvailablePoints <= 10){
            SkillPoints += 1;
            CharacterCreationWindow.MyInstance.AvailablePoints -= 1;
            CharacterCreationWindow.MyInstance.UpdateAvailablePoints();  
            UpdateSkillText();
        }
    }
    public void DecreaseSkill()
    {
        if(CharacterCreationWindow.MyInstance.AvailablePoints < 10 && CharacterCreationWindow.MyInstance.AvailablePoints >= 0){
            SkillPoints -= 1;
            CharacterCreationWindow.MyInstance.AvailablePoints += 1;
            CharacterCreationWindow.MyInstance.UpdateAvailablePoints();
            UpdateSkillText();
        }
    }
    public void SetSkill(int value)
    {
        ClassSkillPoints = value;
        UpdateSkillText();
    }
    private void UpdateSkillText()
    {
        TotalSkillPoints = ClassSkillPoints + SkillPoints;
        SkillPointText.text = SkillPoints.ToString();
        TotalSkillPointsText.text = TotalSkillPoints.ToString();
    }
}
