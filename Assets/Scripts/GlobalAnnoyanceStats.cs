using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAnnoyanceStats : GlobalAnnoyance
{
    public static GlobalAnnoyanceStats instance;

    public GameObject statsUI;

    public TextMeshProUGUI o2Txt;
    public Slider o2Slider;

    public TextMeshProUGUI stressTxt;
    public Slider stressSlider;
    
    public TextMeshProUGUI tempText;
    public Slider tempSlider;

    public GameObject warning;

    float O2Lvl = 100, stressLvl = 100, tempLevel = 50;
    float O2Limit = 50, stressLimit = 75, tempLimit = 38;
    [SerializeField]float curO2 = 100, curStress = 20, curTemp = 15;

    float decreaseRate = 5f;
    float increaseRate = 4f;

    int levelPlayed = 0;

    public void IncreaseLevelPlayed()
    {
        levelPlayed++;
    }

    public void DecreaseLevelPlayed()
    {
        levelPlayed--;
    }

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        o2Slider.maxValue = O2Lvl;
        o2Slider.minValue = 0;
        o2Slider.value = curO2;

        stressSlider.maxValue = stressLvl;
        stressSlider.minValue = 0;
        stressSlider.value = curStress;

        tempSlider.maxValue = tempLevel;
        tempSlider.minValue = 0;
        tempSlider.value = curTemp;
    }

    // Update is called once per frame
    private void Update()
    {
        if(levelPlayed >= 2)
        {
            Destroy(this.gameObject);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (curO2 < O2Lvl)
            {
                curO2 += increaseRate * Time.deltaTime;
            }
        }
        else if (curO2 >= 0)
        {
            curO2 -= decreaseRate * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (curStress > 0)
            {
                curStress -= decreaseRate * Time.deltaTime;
            }
        }
        else if (curStress < stressLvl)
        {
            curStress += increaseRate * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (curTemp > 0)
            {
                curTemp -= decreaseRate/2 * Time.deltaTime;
            }
        }
        else if (curTemp < tempLevel)
        {
            curTemp += increaseRate/2 * Time.deltaTime;
        }

        if (curO2 < O2Limit || curStress >= stressLimit || curTemp >= tempLimit)
        {
            //UI related changes
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
        }

        if (curO2 <= 0 || curTemp >= tempLevel || curStress >= 95)
        {
            //UI related changes
            if(curO2 <= 0 && !LevelManager.instance.isDead)
            {
                StartCoroutine(KillPlayer(1));
            }

            if (curStress >= 95 && !LevelManager.instance.isDead)
            {
                StartCoroutine(KillPlayer(2));
            }

            if (curTemp >= tempLevel && !LevelManager.instance.isDead)
            {
                StartCoroutine(KillPlayer(3));
            }
        }

        o2Txt.text = curO2.ToString("#.") + "%";
        o2Slider.value = curO2;

        stressTxt.text = curStress.ToString("#.") + "%";
        stressSlider.value = curStress;

        tempText.text = curTemp.ToString("#.") + " °C";
        tempSlider.value = curTemp;
    }

    IEnumerator KillPlayer(int index)
    {
        yield return new WaitForSeconds(1);
        LevelManager.instance.LoadDeadScreen(index);
        Destroy(gameObject);
    }
}
