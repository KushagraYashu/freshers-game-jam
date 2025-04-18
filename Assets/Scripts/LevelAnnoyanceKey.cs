using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyanceKey : LevelAnnoyances
{
    public static LevelAnnoyanceKey instance;

    public GameObject keyHoleUI;

    public GameObject keyHoleSpawnPts;

    public GameObject keyPrefab;
    GameObject key;
    public GameObject keyHolePrefab;
    GameObject keyHole;

    public Canvas annoyanceCanvas;

    List<RectTransform> spawnPts = new();

    bool started = false;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.KEYS_MATCH:
                    if (!started)
                    {
                        keyHoleUI.SetActive(true);

                        keyHoleSpawnPts = Instantiate(keyHoleSpawnPts, keyHoleUI.transform);
                        foreach (RectTransform t in keyHoleSpawnPts.GetComponentsInChildren<RectTransform>()) { 
                            spawnPts.Add(t);
                        }

                        int index1 = Random.Range(0, spawnPts.Count);
                        key = Instantiate(keyPrefab, keyHoleUI.transform);
                        key.GetComponent<RectTransform>().anchoredPosition = spawnPts[index1].anchoredPosition;
                        var keyDragScript = key.GetComponent<LevelAnnoyanceKeyDrag>();
                        keyDragScript.parent = keyHoleUI.GetComponent<RectTransform>();
                        keyDragScript.parentCanvas = annoyanceCanvas;

                        int index2;
                        do
                        {
                            index2 = Random.Range(0, spawnPts.Count);
                        } while (index1 == index2);
                        keyHole = Instantiate(keyHolePrefab, keyHoleUI.transform);
                        keyHole.GetComponent<RectTransform>().anchoredPosition = spawnPts[index2].anchoredPosition;
                        keyDragScript.levelAnnoyanceKeyHole = keyHole.GetComponent<LevelAnnoyanceKeyHole>();

                        DisablePlayerControls();
                        started = true;
                        status = started;
                        //Debug.LogError(status);

                    }
                    break;
            }
        }
    }

    public void UnLocked()
    {
        EnablePlayerControls();
        Destroy(key);
        Destroy(keyHole);
        keyHoleUI.SetActive(false);
        annoyanceType = Annoyance.NONE;
        started = false;
        status = started;

    }

    private void OnDisable()
    {
        status = false;
    }
}
