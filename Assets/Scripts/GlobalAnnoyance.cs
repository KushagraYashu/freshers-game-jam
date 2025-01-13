using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAnnoyance : MonoBehaviour
{
    private GameObject player;

    public enum GlobalAnnoyanceType
    {
        NONE,
        HEARTRATE,
        STATS
    };

    public GlobalAnnoyanceType globalAnnoyanceType;

    protected virtual void Start()
    {
        SetPlayer();
    }

    public void SetPlayer()
    {
        if (player == null || !player.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void EnablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
    }

    public void DisablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
    }
}
