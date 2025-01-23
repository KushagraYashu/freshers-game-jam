using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterResetREMOVELATER : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(1);
        }
    }
}
