using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelAnnoyanceHeartRate : LevelAnnoyances
{
    public GameObject heartRateUI;
    public TextMeshProUGUI pressTxt;
    public TextMeshProUGUI curPressTxt;

    public float[] pressTimes = new float[3];

    public int totPress;
    [SerializeField] int curPress = 0;

    bool started = false;
    [SerializeField]bool shouldPress = false;

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.HEART_RATE:
                    if (!started)
                    {
                        DisablePlayerControls();
                        heartRateUI.SetActive(true);
                        pressTxt.text = "";
                        StartCoroutine(StartHeartRate(true));
                        started = true;
                    }
                    break;
            }
        }

        if(started && shouldPress)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(curPress < totPress)
                {
                    curPress++;
                }
                curPressTxt.text = "Total Press: " + curPress;
            }
        }
    }

    IEnumerator StartHeartRate(bool first)
    {
        curPress = 0;
        curPressTxt.text = "Total Press: " + curPress;
        if (first)
        {
            yield return new WaitForSeconds(pressTimes[0] - .2f);
        }
        else
        {
            yield return new WaitForSeconds(pressTimes[0] - .5f);
        }
        shouldPress = true;
        pressTxt.text = "Press \"LCtrl\" Now!";
        yield return new WaitForSeconds(.3f);
        pressTxt.text = "";
        shouldPress = false;
        yield return new WaitForSeconds(pressTimes[1] - .5f);
        shouldPress = true;
        pressTxt.text = "Press \"LCtrl\" Now!";
        yield return new WaitForSeconds(.3f);
        pressTxt.text = "";
        shouldPress = false;
        yield return new WaitForSeconds(pressTimes[2] - .5f);
        shouldPress = true;
        pressTxt.text = "Press \"LCtrl\" Now!";
        yield return new WaitForSeconds(.3f);
        pressTxt.text = "";
        shouldPress = false;
        if (!Check())
        {
            StartCoroutine(StartHeartRate(false));
        }
    }

    bool Check()
    {
        if (curPress >= totPress)
        {
            heartRateUI.SetActive(false);
            EnablePlayerControls();
            annoyanceType = Annoyance.NONE;
            started = false;
            return true;
        }
        else
        {
            return false;
        }
    }
}
