using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelAnnoyanceHandScan : LevelAnnoyances
{
    public GameObject handScanTxt;
    bool scanStarted = false;
    float time;
    float curTime;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.HAND_SCAN:
                    if (!scanStarted)
                    {
                        time = Random.Range(1, 4);
                        curTime = time;

                        DisablePlayerControls();

                        handScanTxt.GetComponentInChildren<TextMeshProUGUI>().text = "Hold \"LCtrl\" for " + time.ToString("#.00") + " seconds, you cutiepie!";
                        handScanTxt.SetActive(true);

                        scanStarted = true;
                        status = scanStarted;

                    }
                    break;

            }
        }

        if (scanStarted)
        {
            if (Input.GetKey(KeyCode.LeftControl) && curTime > 0)
            {
                curTime -= Time.deltaTime;
                handScanTxt.GetComponentInChildren<TextMeshProUGUI>().text = "Hold \"LCtrl\" for " + curTime.ToString("#.00") + " seconds, you cutiepie!";
            }
            if (Input.GetKeyUp(KeyCode.LeftControl) && curTime > 0)
            {
                curTime = time;
                handScanTxt.GetComponentInChildren<TextMeshProUGUI>().text = "Hold \"LCtrl\" for " + curTime.ToString("#.00") + " seconds, you cutiepie!";
            }
            if(curTime <= 0)
            {
                annoyanceType = Annoyance.NONE;
                EnablePlayerControls();
                handScanTxt.SetActive(false);
                scanStarted = false;
                status = scanStarted;

            }
        }
    }
    private void OnDisable()
    {
        status = false;
    }
}
