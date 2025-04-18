using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelAnnoyanceHack : LevelAnnoyances
{
    public GameObject hackText;
    public GameObject hackInputField;
    int hackInput;
    bool hackStarted = false;
    int hackNo;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.HACK:
                    if (!hackStarted)
                    {
                        DisablePlayerControls();

                        LevelManager.instance.LockCursor();
                        hackNo = Random.Range(0000, 9999);

                        hackInputField.SetActive(true);
                        hackText.SetActive(true);
                        hackText.GetComponentInChildren<TextMeshProUGUI>().text = "Enter the code " + hackNo + "!\nthen Press \"Enter\", you imbecile!";

                        hackStarted = true;
                        status = hackStarted;

                    }
                    break;

            }
        }

        if (hackStarted)
        {
            if (Input.GetKey(KeyCode.Return))
            {
                int.TryParse(hackInputField.GetComponent<TMP_InputField>().text, out hackInput);
                if (hackInput == hackNo)
                {
                    annoyanceType = Annoyance.NONE;

                    hackInputField.SetActive(false);
                    hackText.SetActive(false);

                    EnablePlayerControls();

                    hackStarted = false;
                    status = hackStarted;

                }
            }
        }
    }
    private void OnDisable()
    {
        status = false;
    }
}
