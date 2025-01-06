using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyanceCarKeys : LevelAnnoyances
{
    public GameObject carKeysUI;
    bool started;
    string keyColor;
    GameObject keyGO;
    string carColor;
    GameObject carGO;
    public int totKeys;
    int curKeys;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.CAR_KEYS:
                    if (!started)
                    {
                        DisablePlayerControls();
                        carKeysUI.SetActive(true);
                        started = true;
                    }
                    break;
            }
        }
    }

    public void AddKeyColor(string keyColor)
    {
        this.keyColor = keyColor;
    }

    public void AddKeyGO(GameObject key)
    {
        this.keyGO = key;
    }
    public void AddCarGO(GameObject car)
    {
        this.carGO = car;
    }

    public void Compare(string carColor)
    {
        this.carColor = carColor;
        if (started)
        {
            if (string.Equals(keyColor, this.carColor))
            {
                StartCoroutine(Verified());
            }
        }
    }

    IEnumerator Verified()
    {
        curKeys++;
        keyGO.SetActive(false);
        carGO.SetActive(false);
        if (curKeys == totKeys)
        {
            EnablePlayerControls();
            annoyanceType = Annoyance.NONE;
            yield return new WaitForSeconds(.5f);
            carKeysUI.SetActive(false);
        }
    }
}
