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
        
        
        Cursor.lockState = CursorLockMode.Locked;
        

        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseS * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseS * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,  90f);

        //mouseX = Mathf.Clamp(mouseX, -9f, 9f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        cameraBody.Rotate(Vector3.up * mouseX);

        /*mouseX = Mathf.Clamp(mouseX, -10f, 10f);
        if(cameraBody.transform.localEulerAngles.y < 10f || cameraBody.transform.localEulerAngles.y > 350f)
            cameraBody.Rotate(Vector3.up * mouseX);
        if (cameraBody.transform.localEulerAngles.y <= 350 && cameraBody.transform.localEulerAngles.y > 330 && mouseX >= 0f)
            cameraBody.Rotate(Vector3.up * mouseX);
        if (cameraBody.transform.localEulerAngles.y >= 10f && cameraBody.transform.localEulerAngles.y < 30 && mouseX <= 0f)
            cameraBody.Rotate(Vector3.up * mouseX);
        //Debug.Log(cameraBody.transform.localEulerAngles.y);*/
    }
}
