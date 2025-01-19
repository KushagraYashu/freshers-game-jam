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
        STATS,
        WINDOW_POPUP
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
}
