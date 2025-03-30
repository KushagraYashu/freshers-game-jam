using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSCameraScript : MonoBehaviour
{
    //mouse sensitivity
    public float mouseS = 100f;

    public Transform cameraBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        
        //Cursor.lockState = CursorLockMode.Locked;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseS;
            float mouseY = Input.GetAxis("Mouse Y") * mouseS;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //mouseX = Mathf.Clamp(mouseX, -9f, 9f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            cameraBody.Rotate(Vector3.up * mouseX);
        }
    }
}
