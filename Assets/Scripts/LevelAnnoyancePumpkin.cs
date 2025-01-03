using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnnoyancePumpkin : LevelAnnoyances
{
    public GameObject pumpkinPrefab;
    public GameObject pumpkinTxt;
    GameObject[] spawnPts;
    List<GameObject> pumpkins = new List<GameObject>();
    bool pumpkinAdded = false;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
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

            }
        }

        if (pumpkinAdded)
        {
            foreach (var pumpkin in pumpkins)
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
    }
}
