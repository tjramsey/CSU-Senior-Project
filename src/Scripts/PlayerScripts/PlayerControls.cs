using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControls : MonoBehaviour
{
	public InventoryObject Inventory;
	
	public float maxHealth = 100.0f;
	public float maxStamina = 100.0f;
	public float maxMana = 100.0f;

	public float currentStamina;
	public float currentMana;
	public float currentHealth;

	public float staminaRegen = 1.0f;
	public float staminaDecrease = 1.0f;
	public float StimeToRegen = 3.0f;
	public float realSregen = 1.0f;
	private float StaminaRegenTimer = 0.0f;

	public float healthRegen = 1.0f;
	public float manaRegen = 1.0f;

	public Slider healthBarSlider;
	public HealthBar healthBar;
	public ManaBar manaBar;
	public StaminaBar staminaBar;

	private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		
		currentStamina = maxStamina;
		staminaBar.SetMaxStamina(maxStamina);
		
		currentMana = maxMana;
		manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
		bool isRunning = Input.GetKey(KeyCode.LeftShift);
		if(isRunning)
		{
			currentStamina = Mathf.Clamp(currentStamina - (staminaDecrease * Time.deltaTime), 0.0f, maxStamina);
			StaminaRegenTimer = 0.0f;
		}
		if (Input.GetKeyDown(KeyCode.Space) && jumping ==false)
		{
			jumping = true;
			TakeStaminaDamage(20);
			staminaRegen = 0;
			StartCoroutine(WaitS());
		}
		// if (Input.GetKeyDown(KeyCode.LeftShift))
		// {
		// 	staminaRegen = 0;
		// 	TakeStaminaDamage(1);
		// }
		if(currentStamina < maxStamina && jumping == false && isRunning == false){
			if(StaminaRegenTimer >= StimeToRegen){
			currentStamina = Mathf.Clamp(currentStamina + (staminaRegen * Time.deltaTime), 0.0f, maxStamina);
			staminaBar.SetStamina(currentStamina);
			}
			else{
				StaminaRegenTimer += Time.deltaTime;
			}
		}

		if(healthBarSlider.value <= 0)
		{
			SceneManager.LoadScene(1);
		}
    }

	
	IEnumerator WaitS()
	{
		yield return new WaitForSeconds(1);
		jumping = false;
		yield return new WaitForSeconds(5);
		staminaRegen = realSregen;
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);

	}

	void TakeStaminaDamage(int damage)
	{
		currentStamina -= damage;
		staminaBar.SetStamina(currentStamina);
	}

	void TakeManaDamage(int damage)
	{
		currentMana -= damage;
		manaBar.SetMana(currentMana);
	}
}