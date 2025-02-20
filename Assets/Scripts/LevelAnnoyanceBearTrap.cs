using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelAnnoyanceBearTrap : LevelAnnoyances
{
    public GameObject bearTrapUI;
    public TextMeshProUGUI progressTxt;
    public UnityEngine.UI.Slider progressSlider;
    public GameObject bearTrapPrefab;
    bool started;
    float time;
    float curTime;
    int totPress;
    int curPress;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.BEAR_TRAP:
                    if (!started)
                    {
                        DisablePlayerControls();

                        bearTrapUI.SetActive(true);

                        time = Random.Range(3, 6);
                        totPress = Random.Range(5, 11);
                        progressSlider.maxValue = totPress;

                        //do bear prefab

                        started = true;
                        status = started;
                    }
                    break;

            }
        }

        if (started)
        {
            curTime += Time.deltaTime;
            if (curTime > time)
            {
                curTime = 0;
                curPress = 0;
                progressSlider.value = curPress;
                progressTxt.text = (curPress / (float)totPress * 100).ToString("#.00") + "%";
                progressTxt.color = Color.red;
                return;
            }
            if(curPress >= totPress)
            {
                progressTxt.color = Color.green;
                StartCoroutine(DisarmTrap());
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                curPress++;
                progressTxt.text = (curPress / (float)totPress * 100).ToString("#.00") + "%";
                progressSlider.value = curPress;
            }
        }
    }

    IEnumerator DisarmTrap()
    {
        EnablePlayerControls();
        annoyanceType = Annoyance.NONE;
        started = false;
        status = started;

        yield return new WaitForSeconds(1);

        bearTrapUI.SetActive(false);
    }
    private void OnDisable()
    {
        status = false;
    }
}
