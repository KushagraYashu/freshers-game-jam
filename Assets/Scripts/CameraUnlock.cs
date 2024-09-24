using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraUnlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
