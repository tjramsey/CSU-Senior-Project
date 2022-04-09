using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictDamage : MonoBehaviour
{
    public int DamageAmount = 1;
    public float TargetDistance;
    public float AllowedRange = 2.7f;
    public int DealingDamage;
    public RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && UiManagerScript.MyInstance.MenuOpen == false)
        {
            if(Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out hit)) {
                TargetDistance = hit.distance;
                if (TargetDistance <= AllowedRange) {
                    if(DealingDamage == 0){
                    StartCoroutine (weaponAttack());
                    }
                }
            }
        }
    }

    IEnumerator weaponAttack() {
        DealingDamage = 2;
        yield return new WaitForSeconds (0.5f);
        hit.transform.SendMessage ("DeductPoints", DamageAmount, SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds (0.5f);
        DealingDamage = 0;
    }
   
}
