using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemObject MyItem{get; set;}

    [SerializeField]
    private GameObject highlight;

    [SerializeField]
    private Transform componentArea;

    [SerializeField]
    private GameObject componentPrefab;

    [SerializeField]
    private TextMeshProUGUI Title;

    [SerializeField]
    private bool selected;

    [SerializeField]
    private Image itemImage;

    public void Start()
    {
        itemImage.sprite = MyItem.MyIcon;
        if(MyItem.MyTitle == "Iron Ingot")
        {
            itemImage.color = Color.grey;
        }
        Title.text = MyItem.MyTitle;
    }

    
    public void Select()
    {
        if(selected == true)
        {
            Deselect();
        }
        else{
        CraftingWindow.MyInstance.SelectRecipe(this);
            highlight.GetComponent<Image>().color = Color.red;
        selected = true;
        }

    }
    public void Deselect()
    {
        highlight.GetComponent<Image>().color = Color.white;
        selected = false;
    }
    public void ShowComponents()
    {
        foreach(Component c in MyItem.MyComponents)
        {
            GameObject go = Instantiate(componentPrefab, componentArea);
            Debug.Log(c.MyItem.MyTitle);
            go.GetComponentInChildren<Image>().sprite = c.MyItem.MyIcon;
            go.GetComponentInChildren<TextMeshProUGUI>().text = c.MyAmount.ToString();
        }
    }
    
}
