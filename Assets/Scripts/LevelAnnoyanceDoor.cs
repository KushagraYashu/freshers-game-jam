using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelAnnoyanceDoor : LevelAnnoyances
{
    public GameObject doorOpenUI;

    public Animator doorAnimator;

    public NavMeshObstacle doorObstacle;

    public BoxCollider doorCollider;

    bool started = false;

    // Update is called once per frame
    void Update()
    {
        switch (annoyanceType) {
            case Annoyance.NONE:
                break;

            case Annoyance.DOOR:
                if (!started)
                {
                    doorOpenUI.SetActive(true);
                    started = true;
                    status = started;

                }
                break;
        }

        if(started && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OpenDoor());
        }
    }
    private void OnDisable()
    {
        status = false;
    }

    IEnumerator OpenDoor()
    {
        doorAnimator.SetBool("Open", true);
        yield return new WaitForSeconds(2f);
        doorAnimator.speed = 0;
        doorObstacle.enabled = false;
        doorCollider.enabled = false;
        doorOpenUI.SetActive(false);
        annoyanceType = Annoyance.NONE;
        started = false;
        status = started;

    }
}
