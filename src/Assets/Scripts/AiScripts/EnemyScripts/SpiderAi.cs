using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderAi : MonoBehaviour
{
    public GameObject ThePlayer;
    public float TargetDistance;
    public float AllowedRange = 40;
    public GameObject TheEnemy;
    public float EnemySpeed;
    public int AttackTrigger;
    public RaycastHit hit;
    public int EnemyHealth = 10;
    public int EnemyStatus;
    public int BaseXP = 10;
    public int CalculatedXP;
    public static int GlobalSpider;
    public int DealingDamage;
    public Slider HealthBar;
    

    void DeductPoints( int DamageAmount) {
        if(EnemyHealth > 0){
        EnemyHealth -= DamageAmount;
        }
    }

    void Update()
    {

        GlobalSpider = EnemyStatus;
        if(EnemyHealth > 0){
            transform.LookAt (ThePlayer.transform);
        
            AllowedRange = 40;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                TargetDistance = hit.distance;
                if(TargetDistance <= AllowedRange)
                {
                    EnemySpeed = 0.05f;
                    if(AttackTrigger == 0)
                    {
                        TheEnemy.GetComponent<Animation>().Play("walk");
                        transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, EnemySpeed);
                    }
                }
                else{
                    EnemySpeed = 0;
                    TheEnemy.GetComponent<Animation>().Play("idle");
                }
            }
            if(AttackTrigger == 1)
            {
                 
                
            }
        }

        if(EnemyHealth <= 0) {
            if(EnemyStatus == 0)
            {
                StartCoroutine(DeathEnemy ());
            }
        }

        IEnumerator DeathEnemy () {
            EnemyStatus = 6;
            CalculatedXP = BaseXP * GlobalLevel.CurrentLevel;
            GlobalExp.CurrentExp += CalculatedXP;
            yield return new WaitForSeconds (0f);
            TheEnemy.GetComponent<Animation>().Play("die");
        }

        IEnumerator TakingDamage() {
            DealingDamage = 2;
            yield return new WaitForSeconds(0.5f);
            if(EnemyStatus != 6)
            {
                HealthBar.value -= 5;
            }
            yield return new WaitForSeconds(0.5f);
            DealingDamage = 0;
        }
    }

    void OnTriggerEnter()
    {
        AttackTrigger = 1;
    }

    void OnTriggerExit()
    {
        AttackTrigger = 0;
    }
}
