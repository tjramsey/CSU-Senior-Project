using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;



public class PlayerControls : MonoBehaviour
{
	private bool isRunning = false;
	private bool jumping = false;

	[SerializeField]
	private GameObject[] mainHandWeapons;
	[SerializeField]
	private GameObject[] offHandWeapons;

	public CharacterButton mainHand;

	public CharacterButton offHand;

	private bool unsheathed;

	private static PlayerControls instance;

    public static PlayerControls MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerControls>();
            }
            return instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
		unsheathed = false;
    }
    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown(KeyCode.F))
		{
			if(unsheathed == false)
			{
				unsheathed = true;
				Equip();
			}
			else
			{
				unsheathed = false;
				Dequip();
			}
		}
		// if(Input.GetKey(KeyCode.O))
		// {
		// 	PlayerStats.MyInstance.TakeDamage(10);
		// }
		// if(Input.GetKey(KeyCode.I))
		// {
		// 	PlayerStats.MyInstance.TakeManaDamage(10);
		// }
		// if(Input.GetKey(KeyCode.U))
		// {
		// 	PlayerStats.MyInstance.TakeStaminaDamage(10);
		// }

		if(Input.GetKey(KeyCode.LeftShift) && PlayerStats.MyInstance.currentStamina > 0 &&(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.D)))
		{
			PlayerStats.MyInstance.regenStamina = 0;
			PlayerStats.MyInstance.currentStamina = Mathf.Clamp(PlayerStats.MyInstance.currentStamina - (10.0f * Time.deltaTime), 0.0f, PlayerStats.MyInstance.maxStamina);
		}
		if (Input.GetKeyDown(KeyCode.Space) && jumping == false && PlayerStats.MyInstance.currentStamina >= 20)
		{
			jumping = true;
			PlayerStats.MyInstance.TakeStaminaDamage(20);
			PlayerStats.MyInstance.regenStamina = 0;
			StartCoroutine(WaitS());
		}
		if(jumping == false && isRunning == false){
			PlayerStats.MyInstance.regenStamina = 1;
		}
		else{
			PlayerStats.MyInstance.regenStamina = 0;
		}
    }

	
	IEnumerator WaitS()
	{
		yield return new WaitForSeconds(1);
		jumping = false;
	}
	
	private void OnApplicationQuit()
	{
		// Inventory.Clear();
		// Equipment.Clear();
	}

	public void Equip()
	{
		if(mainHand.MyItem != null){
		foreach(GameObject go in mainHandWeapons)
		{
			Debug.Log(go.name+" : "+mainHand.MyItem.MyObject.name);
			if(go.name == mainHand.MyItem.MyObject.name)
			{
				go.SetActive(true);
				break;
			}
			else{
				go.SetActive(false);
			}
		}
		}
		if(offHand.MyItem != null){
		foreach(GameObject go in offHandWeapons)
		{
			Debug.Log(go.name+" : "+offHand.MyItem.MyObject.name);
			if(go.name == offHand.MyItem.MyObject.name)
			{
				go.SetActive(true);
				break;
			}
			else{
				go.SetActive(false);
			}
		}
		}
		unsheathed = true;
	}
	public void Dequip()
	{
		foreach(GameObject go in mainHandWeapons)
		{
			go.SetActive(false);
		}
		foreach(GameObject go in offHandWeapons)
		{
			go.SetActive(false);
		}
		unsheathed = false;
	}


}