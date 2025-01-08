using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pillar : MonoBehaviour
{
    public GameObject hellOrHeavenTxt;
    public GameObject playerCanvas;
    public GameObject hellCanvas;

    public GameObject player;

    public enum PillarType
    {
        NONE,
        HEAVEN,
        HELL
    };

    public PillarType curPillar;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hellOrHeavenTxt.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hellOrHeavenTxt.SetActive(false);
        }
    }

    public void Update()
    {
        if(hellOrHeavenTxt.activeInHierarchy && Input.GetKeyDown(KeyCode.E))
        {
            if(curPillar == PillarType.HEAVEN)
            {
                SceneManager.LoadScene(1);
            }

            if(curPillar == PillarType.HELL)
            {
                hellCanvas.SetActive(true);
                StartCoroutine(DelayedQuit());
            }
        }
    }

    IEnumerator DelayedQuit()
    {
        DisablePlayerControls();
        yield return new WaitForSeconds(3);
        Application.Quit();
    }

    public void EnablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
    }

    public void DisablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
    }
}
