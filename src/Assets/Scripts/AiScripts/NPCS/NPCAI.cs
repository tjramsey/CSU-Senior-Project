using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public GameObject TheNPC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
      TheNPC.GetComponent<Animator>().Play("Female Idle");
      TheNPC.GetComponent<VillagerWalkAi>().enabled = false; 
      TheNPC.GetComponent<NavMeshAgent>().enabled = false;
    }


}
