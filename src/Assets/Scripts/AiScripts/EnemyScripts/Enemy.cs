using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Spider,
    Goblin,
    Wolf
}
public class Enemy : MonoBehaviour
{
    public GameObject PlayerAggroRange;
    public GameObject ThePlayer;
    public EnemyHealth enemyh;
    public float BaseXP;
    public float modXP;
    private int AllowedRange = 15;
    public float TargetDistance;
    public float AttackDistance;
    public float EnemyDamage;
    public float EnemySpeed;
    private float DamageDealt;

    private Container container;

    private bool Dead;

    public bool IsDead
    {
        get {return Dead;}
        set {Dead = value;}
    }

    [SerializeField]
    private bool staggered;

    [SerializeField]
    private bool attacking;
    [SerializeField]
    private bool InAggroRange = false;

    public RaycastHit hit;

    [SerializeField]
    private EnemyType type;

    public EnemyType MyType
    {
        get{ return type;}
    }
    public void Awake()
    {
        ThePlayer = GameObject.Find("FPSController");
        PlayerAggroRange = GameObject.Find("AggroRange");
    }
    public void Start()
    {

        enemyh.enemy = this;
        Dead = false;
        container = GetComponent<Container>();
        container.enabled = false;

    }

    private void Update()
    {
        if(!Dead && InAggroRange)
        {
            transform.LookAt(ThePlayer.transform);
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                if(hit.collider.gameObject.tag == "player"){
                    TargetDistance = hit.distance;
                    if(TargetDistance > AttackDistance  && attacking == false)
                    {
                        EnemySpeed = 0.05f;
                        GetComponentInChildren<Animation>().Play("walk");
                        transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, EnemySpeed);

                    }
                    else{
                        if(TargetDistance <= AttackDistance && attacking == false)
                        {
                            EnemySpeed = 0;
                            if(staggered == false){
                                GetComponentInChildren<Animation>().Play("attack");
                                StartCoroutine(Attack());
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == aggro)
            InAggroRange = true;
           
    }
    private void OnTriggerExit()
    {
        InAggroRange = false;

    }
    IEnumerator Attack() {
        print(TargetDistance + " : " + AttackDistance);
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        if(enemyh.health > 0)
            DamageDealt = EnemyDamage - PlayerStats.MyInstance.attributes[7].value;
            if(DamageDealt < 0)
            {
                DamageDealt = 0;
            }
            PlayerStats.MyInstance.TakeDamage(DamageDealt);
        yield return new WaitForSeconds(0.5f);
        attacking = false; 
    }
    public IEnumerator DestroyBody()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
        yield return new WaitForSeconds(5.0f);

    }
    public IEnumerator Staggered()
    {
        int stagChance = UnityEngine.Random.Range(0,100);
        if(stagChance >= 70)
        {
            staggered = true;
            yield return new WaitForSeconds(5.0f);
        }
        staggered = false;
    }
    public void DeathEnemy () {
       // modXP = BaseXP * GlobalLevel.CurrentLevel;
        //GlobalExp.CurrentExp += modXP;
        //yield return new WaitForSeconds (0f);

        PlayerStats.MyInstance.AddExp(BaseXP);
        GetComponentInChildren<Animation>().Play("die");
        Dead = true;
        container.enabled = true;
        GameManager.MyInstance.OnKillConfirmed(this);

    }

    void DeductPoints( int DamageAmount) {
        if(enemyh.health > 0){
        enemyh.health -= DamageAmount;
        }
        StartCoroutine(Staggered());
    }
}
