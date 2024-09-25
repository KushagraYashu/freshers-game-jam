using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpening : MonoBehaviour
{
    private bool doorsOpen = false;

    private bool doorsClose = false;

    [SerializeField] private Animator animatorL = null;
    [SerializeField] private Animator animatorR = null;

    public Ltestscript ltestscript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            OpenDoor();
        }

        if (Input.GetKeyDown("q"))
        {
            CloseDoor();
        }

        //if (doorsClose == true)
        //{
        //Debug.Log("doors close");
        if ( ltestscript.tScene == true)
        {
            Debug.Log("scene load");
            SceneManager.LoadScene("TransitionTest");
        }
            
       //}

        /*if (doorsOpen == false)
        {
            SceneManager.LoadScene("TransitionTest");
        }*/


        /*if (doorsOpen == true)
        {
            animator.SetTrigger("doorsOpen");

            doorsOpen = false;
        }*/


    }

    private void OpenDoor()
    {
        //if (doorsOpen) { return; }
        animatorL.SetBool("test", true);
        animatorL.ResetTrigger("doorsClose");
        animatorR.ResetTrigger("doorsClose");
        animatorL.SetTrigger("doorsOpen");
        animatorR.SetTrigger("doorsOpen");
        doorsOpen = true;
        doorsClose = false;
                                                            
    }

    private void CloseDoor()
    {
        //if (doorsClose) { return; }
        animatorL.SetBool("test", false);
        animatorL.SetTrigger("doorsClose");
        animatorR.SetTrigger("doorsClose");
        animatorL.ResetTrigger("doorsOpen");
        animatorR.ResetTrigger("doorsOpen");
        doorsClose = true;
        doorsOpen = false;

    }

    //public void FinishOpening() => doorsOpen = false;
    //public void FinishClosing() => doorsClose = false;
}
