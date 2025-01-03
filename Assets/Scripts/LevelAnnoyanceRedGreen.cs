using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyanceRedGreen : LevelAnnoyances
{
    public GameObject redGreen;
    bool red = false;
    bool green = false;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.RED_GREEN:
                    if (!red)
                    {
                        StartCoroutine(RedGreen());
                        red = true;
                    }
                    break;
            }
        }

        if (green)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                EnablePlayerControls();
                redGreen.SetActive(false);
                annoyanceType = Annoyance.NONE;
                green = false;
            }
        }
    }

    IEnumerator RedGreen()
    {
        DisablePlayerControls();

        redGreen.SetActive(true);
        redGreen.GetComponentInChildren<Image>().color = Color.red;

        yield return new WaitForSeconds(Random.Range(1, 3));

        green = true;
        redGreen.GetComponentInChildren<Image>().color = Color.green;
    }
}
