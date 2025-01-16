using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyanceToxicGas : LevelAnnoyances
{
    public GameObject toxicGasUI;

    public TextMeshProUGUI gasLevel;
    public Slider gasSlider;

    [SerializeField]float maxLevel = 100;
    [SerializeField]float curLevel = 50;

    public float increaseRate = 4f;
    public float decreaseRate = 5f;

    bool started = false;

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.TOXIC_GAS:
                    if (!started)
                    {
                        gasSlider.maxValue = maxLevel;
                        gasSlider.minValue = 0;
                        toxicGasUI.SetActive(true);
                        gasLevel.text = curLevel.ToString("#.") + "%";
                        gasSlider.value = curLevel;
                        started = true;
                    }
                    break;
            }
        }

        if (started)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if(curLevel > 0)
                {
                    curLevel -= decreaseRate * Time.deltaTime;
                }
            }
            
            else if (curLevel < maxLevel)
            {
                curLevel += increaseRate * Time.deltaTime;
            }
            gasLevel.text = curLevel.ToString("#.") + "%";
            gasSlider.value = curLevel;
        }

        if (curLevel >= 0.75 * maxLevel)
        {
            //do camera stuff
        }
    }
}
