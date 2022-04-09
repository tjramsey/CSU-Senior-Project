using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeyBindManager : MonoBehaviour
{
    private static KeyBindManager instance;

    public static KeyBindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<KeyBindManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, KeyCode> Keybinds {get; private set;}
    public Dictionary<string, KeyCode> Actionbinds {get; private set;}

    private string bindName;
    // Start is called before the first frame update
    void Start()
    {
        Keybinds = new Dictionary<string, KeyCode>();
        Actionbinds = new Dictionary<string, KeyCode>();

        BindKey("Inventory", KeyCode.Tab);
        BindKey("Game Menu", KeyCode.Escape);

        BindKey("ACT1", KeyCode.Alpha1);
        BindKey("ACT2", KeyCode.Alpha2);
        BindKey("ACT3", KeyCode.Alpha3);


    }

    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = Keybinds;

        if(key.Contains("ACT"))
        {
            currentDictionary = Actionbinds;
        }

        if(!currentDictionary.ContainsValue(keyBind))
        {
            currentDictionary.Add(key,keyBind);
            UiManagerScript.MyInstance.UpdateKeyText(key, keyBind);
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
        
            currentDictionary[myKey]  = KeyCode.None;
            UiManagerScript.MyInstance.UpdateKeyText(key, KeyCode.None);

        }

        currentDictionary[key] = keyBind;
         UiManagerScript.MyInstance.UpdateKeyText(key, keyBind);
        bindName = string.Empty;

    }
}
