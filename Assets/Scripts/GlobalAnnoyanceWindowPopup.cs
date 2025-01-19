using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAnnoyanceWindowPopup : GlobalAnnoyance
{
    public static GlobalAnnoyanceWindowPopup instance;

    public GameObject playerGO;

    public GameObject windowPopupPrefab;

    public GameObject spawnPointGO;

    List<RectTransform> spawnPts = new();

    [SerializeField]int totPopup;

    public int curClosed;

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        StartCoroutine(WindowPopups());
    }

    IEnumerator WindowPopups()
    {
        yield return new WaitForSeconds(1f);
        playerGO = GameObject.FindGameObjectWithTag("Player");
        DisablePlayerControls();
        foreach (RectTransform t in spawnPointGO.GetComponentsInChildren<RectTransform>())
        {
            spawnPts.Add(t);
        }

        totPopup = Random.Range(10, 25);

        for (int i = 0; i < totPopup; i++)
        {
            int index = Random.Range(0, spawnPts.Count);
            Instantiate(windowPopupPrefab, spawnPts[index]);
            yield return new WaitForSeconds(.2f);
        }
    }

    private void Update()
    {
        if (curClosed == totPopup) { 
            EnablePlayerControls();
        }
    }

    public void EnablePlayerControls()
    {
        playerGO.GetComponent<Playermovement>().enabled = true;
        playerGO.GetComponentInChildren<FPSCameraScript>().enabled = true;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        playerGO.GetComponentInChildren<Gun>().enabled = true;
    }

    public void DisablePlayerControls()
    {
        playerGO.GetComponent<Playermovement>().enabled = false;
        playerGO.GetComponentInChildren<FPSCameraScript>().enabled = false;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        playerGO.GetComponentInChildren<Gun>().enabled = false;
    }
}
