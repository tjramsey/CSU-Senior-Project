using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;


[System.Serializable]
public class PlayerStats : MonoBehaviour
{

    private static PlayerStats instance;

    public static PlayerStats MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerStats>();
            }
            return instance;
        }
    }

    [SerializeField]
    private FirstPersonController FPC;

    [SerializeField]
    private string name;
    private float baseMaxHealth = 100.0f;
    public float maxHealth;
    public float currentHealth;
    public float healthRegen = 0.0f;
    public int regenHealth;

    private float baseMaxStamina = 100.0f;
    public float maxStamina;
    public float currentStamina;
    public float staminaRegen = 0.1f;
    public int regenStamina;

    private float baseMaxMana = 100.0f;
    public float maxMana;
    public float currentMana;
    public float manaRegen = 0.0f;
    public int regenMana;

    public HealthBar healthBar;
    public ManaBar manaBar;
    public StaminaBar staminaBar;

    public Attribute[] attributes;

    public GameObject LevelledUP;

    // public InventoryObject Inventory;
    // public InventoryObject Equipment;

    public GameObject PlayerStatValues;

    private int gold;
    
    public int PlayerGold
    {
        get {return gold;}
        set {
            gold = value;
            PlayerGoldText.text = gold + "gold";
        }
    }
    public Text PlayerGoldText;

    public TextMeshProUGUI PlayerNameText;

    public bool shopping;

    [SerializeField]
    private int SkillPoints;

    [SerializeField]
    private TextMeshProUGUI RemainingPoints;
     
    [SerializeField]
    private int level;

    public int MyLevel{
        get {return level;}
        set {
            level = value;
            LevelIndicator.text = "Level " +MyLevel.ToString();
            }
    }

    public TextMeshProUGUI LevelIndicator;

    [SerializeField]
    private float currentExp;

    public float MyXp 
    {
        get { return currentExp;}
        set { currentExp = value;
                ExpPercentage.text = ((currentExp/MyMaxXp)*100).ToString() + "%";}
    }

    [SerializeField]
    private TextMeshProUGUI ExpPercentage;

    [SerializeField]
    private float neededExp;
    public float MyMaxXp 
    {
        get { return neededExp;}
        set {neededExp = value;}
    }

    public string MyName
    {
        get { return name;}
        set {name = value;
                PlayerNameText.text = name;}
    }

    [SerializeField]
    private GameObject ConfirmButton;

    private int DamageFromStrength;

    public string MyClass;

    private bool upLevel;

    public void UpdateEquipmentStats(Equipment prev, Equipment current)
    {
        //Debug.Log("UpdateEquipment");
        if(prev == null && current != null)
        {
            //Debug.Log("Equipped" +current.MyTitle);
            for(int i = 0; i < current.data.buffs.Length; i++)
            {
                for(int j = 0; j < attributes.Length; j++)
                {
                    if(attributes[j].type == current.data.buffs[i].attribute){
                        attributes[j].AddModifier(current.data.buffs[i].value);
                        updateStatNums(attributes[j]);
                    }
                }
            }
        }
        else if(prev != null && current != null)
        {
            for(int i = 0; i < prev.data.buffs.Length; i++)
            {
                for(int j = 0; j < attributes.Length; j++)
                {
                    if(attributes[j].type == current.data.buffs[i].attribute){
                        attributes[j].RemoveModifier(current.data.buffs[i].value);
                        updateStatNums(attributes[j]);
                    }
                }
            }
            
            for(int i = 0; i < current.data.buffs.Length; i++)
            {
                for(int j = 0; j < attributes.Length; j++)
                {
                    if(attributes[j].type == current.data.buffs[i].attribute){
                        attributes[j].AddModifier(current.data.buffs[i].value);
                        updateStatNums(attributes[j]);
                    }
                }
            }
        }
        else if(prev != null && current == null)
        {
            for(int i = 0; i < prev.data.buffs.Length; i++)
            {
                for(int j = 0; j < attributes.Length; j++)
                {
                    if(attributes[j].type == prev.data.buffs[i].attribute){
                        attributes[j].RemoveModifier(prev.data.buffs[i].value);
                        updateStatNums(attributes[j]);
                    }
                }
            }
        }
        else{
            Debug.Log("ERROR: SOMETHING HAS WENT WRONG");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            AddExp(10);
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            currentHealth = 0;
        }
        if(currentStamina <= 0)
        {
            FPC.LowStaminaRUN = true;
        }
        else{
            FPC.LowStaminaRUN = false;
        }
        if(currentStamina < 20)
        {
            FPC.LowStaminaJMP = true;
        }
        else{
            FPC.LowStaminaJMP = false;
        }
        Regen();
        updateSliders();
    }

    public void SetStartStats()
    {
        MyName = PlayerPrefs.GetString("CharacterName");
        MyClass = PlayerPrefs.GetString("Class");
        PlayerPrefs.DeleteKey("Class");
        PlayerPrefs.DeleteKey("CharacterName");
        PlayerNameText.text = MyName;
        switch(MyClass)
        {
            case ("Knight"):
                PlayerGold = 140;
                break;

            case ("Rogue"):
                PlayerGold = 30;
                break;

            case ("Adventurer"):
                PlayerGold = 75;
                break;

            case ("Mercharnt"):
                PlayerGold = 200;
                break;

            case ("Farmer"):
                PlayerGold = 10;
                break;

            case ("Barbarian"):
                PlayerGold = 15;
                break;
            
            default:
                PlayerGold = 1;
                MyClass = "No Class";
                break;

        }
        int strengthIndex = 0;
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
            string key = attributes[i].type.ToString();
            if(PlayerPrefs.HasKey(key))
            {
                attributes[i].value = PlayerPrefs.GetInt(key);
                PlayerPrefs.DeleteKey(key);
                updateStatNums(attributes[i]);

            }
            if(attributes[i].type == Attributes.Endurance)
            {
                baseMaxHealth = baseMaxHealth + (attributes[i].value * 5f);
                baseMaxStamina = baseMaxStamina + (attributes[i].value * 2.5f);
            }
            else if(attributes[i].type == Attributes.Intellect)
            {
                baseMaxMana = baseMaxMana + (attributes[i].value * 5f);
            }
            else if(attributes[i].type == Attributes.Agility)
            {
                baseMaxStamina = baseMaxStamina + (attributes[i].value * 5f);
            }
            else if(attributes[i].type == Attributes.Strength)
            {
                baseMaxHealth = baseMaxHealth + (attributes[i].value * 5f);
                DamageFromStrength = attributes[i].value / 3;

            }
            else if(attributes[i].type == Attributes.Damage)
            {
                attributes[i].value = 1;
                attributes[i].value += DamageFromStrength;
                updateStatNums(attributes[i]);

            }
            //print(string.Concat("Attribute ", attributes[i].type, " parent", attributes[i].parent, "value = ", attributes[i].value));
        }

        currentHealth = maxHealth = baseMaxHealth;
        currentStamina = maxStamina = baseMaxStamina;
        currentMana = maxMana = baseMaxMana;

        healthBar.SetMaxHealth(maxHealth);
        staminaBar.SetMaxStamina(maxStamina);
        manaBar.SetMaxMana(maxMana);

        
        UpdatePlayerGold();

    }
    void updateSliders()
    {
        healthBar.SetHealth(currentHealth);
        staminaBar.SetStamina(currentStamina);
        manaBar.SetMana(currentMana);
    }

    public void updateStatNums(Attribute attribute)
    {
        string statName = attribute.type.ToString() + "Stat";
        Debug.Log("Updating" + statName);
        GameObject temp = GetChildWithName(PlayerStatValues, statName);
        print(string.Concat("Stat: ", statName," has changed and is now ", attribute.value));
        int oldvalue;
        int.TryParse(temp.GetComponent<TextMeshProUGUI>().text, out oldvalue);

        temp.GetComponent<TextMeshProUGUI>().text = attribute.value.ToString();

        if(attribute.type == Attributes.Strength)
        {
           attributes[6].value -= DamageFromStrength;
           DamageFromStrength = attribute.value/3;
           attributes[6].value += DamageFromStrength;
           attributes[6].tempvalue = attributes[6].value;
            updateStatNums(attributes[6]);
        }
    }

    public void updateStats()
    {
        foreach(Attribute a in attributes)
        {
            updateStatNums(a);

        }
    }

    public void UpdatePlayerGold()
    {
        PlayerGoldText.GetComponent<Text>().text = PlayerGold.ToString();
    }
    public GameObject GetChildWithName(GameObject obj, string name) {
        print(string.Concat("Looking for", name));
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if(childTrans != null)
        {
            print(string.Concat("Text in childTrans is", childTrans.gameObject.GetComponent<TextMeshProUGUI>().text));
            return childTrans.gameObject;
        }
        else{
            return null;
        }
    }

    void checkDead()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }

    void Regen()
    {
        if (regenHealth == 1 && currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + (healthRegen * Time.deltaTime), 0.0f, maxHealth);
            //healthBar.SetHealth(currentHealth);
        }
        if (regenStamina == 1 && currentStamina < maxStamina)
        {
            currentStamina = Mathf.Clamp(currentStamina + (staminaRegen * Time.deltaTime), 0.0f, maxStamina);
            //staminaBar.SetStamina(currentStamina);
        }
        if (regenMana == 1 && currentMana < maxMana)
        {
            currentMana = Mathf.Clamp(currentMana + (manaRegen * Time.deltaTime), 0.0f, maxMana);
            //manaBar.SetMana(currentMana);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        checkDead();

    }
    public void RestoreHealth(float value)
    {
        currentHealth += value;
        healthBar.SetHealth(currentHealth);
    }
    public bool NeedsHealth()
    {
        if(currentHealth < maxHealth)
        {
            return true;
        }
        return false;
    }

    public void TakeStaminaDamage(float damage)
    {
        currentStamina -= damage;
        staminaBar.SetStamina(currentStamina);
    }
    public void RestoreStamina(float value)
    {
        currentStamina += value;
        staminaBar.SetStamina(currentStamina);
    }
    public bool NeedsStamina()
    {
        if(currentStamina < maxStamina)
        {
            return true;
        }
        return false;
    }
    public void TakeManaDamage(float damage)
    {
        currentMana -= damage;
        manaBar.SetMana(currentMana);
    }
    public void RestoreMana(float value)
    {
        currentMana += value;
        manaBar.SetMana(currentMana);
    }
    public bool NeedsMana()
    {
        if(currentMana < maxMana)
        {
            return true;
        }
        return false;
    }

    // public void Running(){
    // 		if(isRunning){
    //             regenStamina = 0;
    //         }

    // }
    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value));
    }

    public void AddExp(float exp)
    {
        MyXp += exp;
        if(currentExp >= neededExp)
        {
            MyLevel+=1;
            MyXp = currentExp - neededExp;
            neededExp = neededExp * 1.5f;
            SkillPoints += MyLevel;
            RemainingPoints.text = SkillPoints.ToString();
            ConfirmButton.SetActive(true);
            upLevel = true;
            StartCoroutine(LevelledUp());
        }
    }

    IEnumerator LevelledUp()
    {
        yield return new WaitForSeconds (0.5f);
        LevelledUP.SetActive(true);
        yield return new WaitForSeconds (2.0f);
        LevelledUP.SetActive(false);

    }

    public void SpendSkillPoint(string skill)
    {
        if(SkillPoints >0 && upLevel == true){
            foreach(Attribute a in attributes)
            {
                if(a.type.ToString() == skill)
                {
                    a.tempvalue += 1;
                    SkillPoints --;
                    RemainingPoints.text = SkillPoints.ToString();
                    string statName = a.type.ToString() + "Stat";
                    GameObject temp = GetChildWithName(PlayerStatValues, statName);
                    temp.GetComponent<TextMeshProUGUI>().text = a.tempvalue.ToString();
                    break;
                }
            }
        }
    }
    public void RemoveSkillPoint(string skill)
    {
        if(upLevel == true){
        foreach(Attribute a in attributes)
        {
            if(a.value == a.tempvalue)
            {
                break;
            }
            
            if(a.type.ToString() == skill)
            {
                a.tempvalue -= 1;
                SkillPoints ++;
                RemainingPoints.text = SkillPoints.ToString();
                string statName = a.type.ToString() + "Stat";
                GameObject temp = GetChildWithName(PlayerStatValues, statName);
                temp.GetComponent<TextMeshProUGUI>().text = a.tempvalue.ToString();
                break;
            }
        }
        }
    }
    public void LevelUp()
    {

        foreach(Attribute a in attributes)
        {
            a.value = a.tempvalue;
            updateStatNums(a);
        }

        if(SkillPoints == 0)
        {
            ConfirmButton.SetActive(false);
            upLevel = false;
        }
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerStats parent;
    public Attributes type;
    public int value;
    public int tempvalue;
    public bool changed;

    public void SetParent(PlayerStats _parent)
    {
        parent = _parent;
        tempvalue = value;
        
    }
    // public void AttributeModified()
    // {
    //     parent.AttributeModified(this);
    // }
    public void AddModifier(int modvalue)
    {
        value += modvalue;
        tempvalue = value;
    }
    public void RemoveModifier(int modvalue)
    {
        value -= modvalue;
        tempvalue = value;
    }
}