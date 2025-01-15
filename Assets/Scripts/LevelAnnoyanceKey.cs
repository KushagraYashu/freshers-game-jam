using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnnoyanceKey : LevelAnnoyances
{
    public static LevelAnnoyanceKey instance;

    public GameObject keyHoleUI;

    bool started = false;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.KEYS_MATCH:
                    if (!started)
                    {
                        DisablePlayerControls();
                        keyHoleUI.SetActive(true);
                        started = true;
                    }
                    break;
            }
        }
    }

    public void UnLocked()
    {
        EnablePlayerControls();
        keyHoleUI.SetActive(false);
        annoyanceType = Annoyance.NONE;
    }
}
