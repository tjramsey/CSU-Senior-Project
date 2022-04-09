using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class VillagerWalkAi : MonoBehaviour
{
    public GameObject[] PossibleDestinations;
    public GameObject TheDestination;
    //NavMeshAgent theAgent;
    //public NPCAI AISCRIPT;

    void Start() 
    {
        //theAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //theAgent.SetDestination(TheDestination.transform.position);
        // if(this.gameObject.transform.position == TheDestination.transform.position)
        // {
        //     //AISCRIPT.ReachedPosition();
        //     //StartCoroutine(moveWait());

        // }
    }

    // public IEnumerator moveWait()
    // {
    //     yield return new WaitForSeconds(10);
    //     AISCRIPT.LeftPosition();
    // }
}
