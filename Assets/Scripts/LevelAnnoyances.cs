using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyances : MonoBehaviour
{
    public GameObject player;

    public enum Annoyance
    {
        NONE,
        RED_GREEN,
        PUMPKINS,
        BALLOONS,
        SHIT,
        TOXIC_GAS,
        KEYS_MATCH,
        CUPCAKE,
        HACK
    }

    public Annoyance annoyanceType;

    public GameObject redGreen;
    bool red = false;
    bool green = false;

    public GameObject pumpkinPrefab;
    public GameObject pumpkinTxt;
    GameObject[] spawnPts;
    List<GameObject> pumpkins = new List<GameObject>();
    bool pumpkinAdded = false;

    public GameObject hackText;
    public GameObject hackInputField;
    int hackInput;
    bool hackStarted = false;
    int hackNo;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

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

                case Annoyance.PUMPKINS:
                    if (!pumpkinAdded)
                    {
                        spawnPts = GetComponent<ZombieSpawner>().spawnPoints;
                        foreach (var spawnPoint in spawnPts)
                        {
                            pumpkins.Add(Instantiate(pumpkinPrefab, new Vector3(spawnPoint.transform.position.x, 2, spawnPoint.transform.position.z), pumpkinPrefab.transform.rotation));
                        }
                        pumpkinTxt.SetActive(true);
                        pumpkinAdded = true;
                    }
                    break;

                case Annoyance.HACK:
                    if (!hackStarted)
                    {
                        DisablePlayerControls();

                        Cursor.lockState = CursorLockMode.None;
                        hackNo = Random.Range(0000, 9999);

                        hackInputField.SetActive(true);
                        hackText.SetActive(true);
                        hackText.GetComponentInChildren<TextMeshProUGUI>().text = "Enter the code " + hackNo + "!\nthen Press \"LCtrl\", you imbecile!";

                        hackStarted = true;
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

        if (pumpkinAdded)
        {
            foreach(var pumpkin in pumpkins)
            {
                if (pumpkin != null)
                {
                    return;
                }
            }
            pumpkinTxt.SetActive(false);
            annoyanceType = Annoyance.NONE;
            pumpkinAdded = false;
        }

        if (hackStarted)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                int.TryParse(hackInputField.GetComponent<TMP_InputField>().text, out hackInput);
                if (hackInput == hackNo)
                {
                    annoyanceType = Annoyance.NONE;

                    hackInputField.SetActive(false);
                    hackText.SetActive(false);

                    EnablePlayerControls();

                    hackStarted = false;
                }
            }
        }
    }

    void EnablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
    }

    void DisablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
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
