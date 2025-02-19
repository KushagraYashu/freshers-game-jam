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


    [Header("Popup Range")]
    [SerializeField] int minNoOfPopups = 10;
    [Tooltip("Random.Range is max exclusive, to have \"x\" as max, put \"x+1\"")]
    [SerializeField] int maxNoOfPopups = 21;    // Random.Range is max exclusive, to have "x" as max, put "x+1"
    [Space(10)]

    [SerializeField] int totPopup;
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

        totPopup = Random.Range(minNoOfPopups, maxNoOfPopups);

        for (int i = 0; i < totPopup; i++)
        {
            yield return new WaitForSeconds(.2f);
            int index = Random.Range(0, spawnPts.Count);
            Instantiate(windowPopupPrefab, spawnPts[index]);
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
