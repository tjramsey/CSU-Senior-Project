using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClassScript : MonoBehaviour
{

    [SerializeField]
    private string ClassName;

    public string MyName 
    {
        get { return ClassName;}
    }

    [SerializeField]
    private TextMeshProUGUI ClassNameText;

    [SerializeField]
    public bool selected;

    public ClassAttribute[] Skills;

    // Start is called before the first frame update
    void Start()
    {
        ClassName = this.gameObject.name.ToString();
        ClassNameText.text = ClassName;
    }

    public void Select()
    {
        if(selected != true){
            selected = true;
            ClassNameText.color = Color.red;
            CharacterCreationWindow.MyInstance.UpdateStats(this);
        }
        else{
            Deselect();
        }
    }
    public void Deselect()
    {
        selected = false;
        ClassNameText.color = Color.white;
    }

}

[System.Serializable]
public class ClassAttribute
{
    public Attributes type;
    public int value;
}
