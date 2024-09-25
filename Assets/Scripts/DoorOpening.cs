using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpening : MonoBehaviour
{
    private bool doorsOpen = false;

    [SerializeField] private Animator animator = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            doorsOpen = true;
        }

        if (doorsOpen == true)
        {
            animator.SetTrigger("doorsOpen");
        }
    }
}
