using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyances : MonoBehaviour
{
    private GameObject player;

    public enum Annoyance
    {
        NONE,
        RED_GREEN,
        PUMPKINS,
        BALLOONS,
        SHIT,
        TOXIC_GAS,
        KEYS_MATCH,
        CUPCAKE,
        HACK,
        HAND_SCAN,
        TV,
        WIRE_MATCH,
        TICKET,
        BEAR_TRAP,
        CAR_KEYS
    }

    public Annoyance annoyanceType;

    protected virtual void Start()
    {
        SetPlayer();
    }

    public void SetPlayer()
    {
        if(player == null || !player.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
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
