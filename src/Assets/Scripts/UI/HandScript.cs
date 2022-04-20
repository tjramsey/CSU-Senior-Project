using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }
    }
    
    [SerializeField]
    public IMoveable MyMoveable { get; set;}

    [SerializeField]
    private Image icon;

    [SerializeField]
    private GameObject dropArea;


    // public Image MyIcon
    // {
    //     get {return icon;}
    // }
    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = Input.mousePosition;

        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            Debug.Log("Drop Item");
            DropItem();
        }
    }

    public void TakeMoveable(IMoveable moveable)
    {
        if(this.MyMoveable == null){
            this.MyMoveable = moveable;
            icon.sprite = moveable.MyIcon;
            icon.color = Color.white; 
        }   
        
    }
    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0,0,0,0);
        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0,0,0,0);
        InventoryScript.MyInstance.MyFromSlot = null;
    }

    public void DeleteItem()
    {
        
        if(MyMoveable is ItemObject && InventoryScript.MyInstance.MyFromSlot != null)
            {
                (MyMoveable as ItemObject).MySlot.Clear();
                

            }
            Drop();
            InventoryScript.MyInstance.MyFromSlot = null;
    }
    public void DropItem()
    {
        
        if(MyMoveable is ItemObject && InventoryScript.MyInstance.MyFromSlot != null)
        {
                for(int i = 0; i < InventoryScript.MyInstance.MyFromSlot.MyItems.Count; i++)
                {
                    ((MyMoveable as ItemObject)).dropItem(dropArea);
                }
                (MyMoveable as ItemObject).MySlot.Clear();
                Drop();
                InventoryScript.MyInstance.MyFromSlot = null;    
        }
   
    }

}
