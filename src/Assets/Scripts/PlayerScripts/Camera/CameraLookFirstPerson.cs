using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookFirstPerson : MonoBehaviour
{
    public Transform playerRef;
    public float mouseSensitivity = 1f;
    float xrotation = 0f;
    float yrotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX  = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY  = Input.GetAxis("Mouse Y") * mouseSensitivity;
    
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);
        yrotation -= mouseX*-1;
        transform.localRotation = Quaternion.Euler(xrotation, yrotation, 0f);
        playerRef.Rotate(Vector3.up * mouseX);
        playerRef.Rotate(Vector3.right * mouseY);
       // yrotation = Mathf.Clamp()
    }
}
