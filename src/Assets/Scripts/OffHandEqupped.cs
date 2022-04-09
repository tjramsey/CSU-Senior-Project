using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffHandEqupped : MonoBehaviour
{
    public float speed = 2;
void Update()
{
    Transform camTransform = Camera.main.transform;
    Vector3 forwardMovement = camTransform.forward * Input.GetAxis("Vertical");
    Vector3 horizontalMovement = camTransform.right * Input.GetAxis("Horizontal");
    //Vector3 movement = Vector3.ClampMagnitude(forwardMovement,1);
    //transform.Translate(movement * speed * Time.deltaTime, Space.World);
}
}
