using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAnnoyanceStats : GlobalAnnoyance
{
    public GameObject statsUI;
    public GameObject warning;

    float O2Lvl = 100, stressLvl = 100, tempLevel = 100;
    float O2Limit, stressLimit, tempLimit;
    float curO2, curStress, curTemp;

    float decreaseRate = 0.5f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if(curO2 < O2Lvl)
            {
                curO2++;
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha1)) { 
            if(curO2 >= 0)
            {
                curO2 -= decreaseRate;
            }
        }

        if(curO2 < O2Limit)
        {
            //UI related changes
            warning.SetActive(true);
        }
        if (curO2 <= 0) {
            //UI related changes
            StartCoroutine(KillPlayer());
        }
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(1);
        LevelManager.instance.LoadDeadScreen();
    }
}
