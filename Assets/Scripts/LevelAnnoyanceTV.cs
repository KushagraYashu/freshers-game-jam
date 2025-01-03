using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelAnnoyanceTV : LevelAnnoyances
{
    public GameObject TVTxt;
    public TextMeshProUGUI channelTxt;
    bool tuneStarted;
    int tuneNo;
    int curNo;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.TV:
                    if (!tuneStarted)
                    {
                        tuneNo = Random.Range(100, 200);
                        curNo = Random.Range(100, 200);
                        channelTxt.text = "Channel: " + curNo;

                        DisablePlayerControls();

                        TVTxt.GetComponentInChildren<TextMeshProUGUI>().text = "Tune to Channel " + tuneNo + "\n\"LCtrl\" to increase\n\"LShift\" to decrease";
                        TVTxt.SetActive(true);

                        tuneStarted = true;
                    }
                    break;

            }
        }

        if (tuneStarted) {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                curNo++;
                channelTxt.text = "Channel: " + curNo;
                channelTxt.color = Color.red;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                curNo--;
                channelTxt.color = Color.red;
                channelTxt.text = "Channel: " + curNo;
            }
            if (curNo == tuneNo)
            {
                StartCoroutine(VerifyChannel());
            }
        }
    }

    IEnumerator VerifyChannel()
    {
        tuneStarted = false;
        annoyanceType = Annoyance.NONE;
        channelTxt.color = Color.green;
        EnablePlayerControls();
        yield return new WaitForSeconds(1f);
        TVTxt.SetActive(false);
    }
}
