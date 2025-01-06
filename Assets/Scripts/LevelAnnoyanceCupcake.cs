using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelAnnoyanceCupcake : LevelAnnoyances
{
    public GameObject cupcakePrefab;
    public GameObject cupcakeTxt;
    public GameObject cupcakeSpawnPts;
    List<GameObject> cupcakes = new List<GameObject>();
    bool cupcakeAdded = false;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.CUPCAKE:
                    if (!cupcakeAdded)
                    {
                        cupcakeSpawnPts = Instantiate(cupcakeSpawnPts, this.gameObject.transform);
                        var spawnPts = cupcakeSpawnPts.GetComponentsInChildren<Transform>();
                        foreach (var spawnPoint in spawnPts)
                        {
                            cupcakes.Add(Instantiate(cupcakePrefab, new Vector3(spawnPoint.position.x, 5.0f, spawnPoint.position.z), cupcakePrefab.transform.rotation));
                        }
                        cupcakeTxt.SetActive(true);
                        cupcakeAdded = true;
                    }
                    break;
            }
        }

        if (cupcakeAdded)
        {
            foreach (var cupcake in cupcakes)
            {
                if (cupcake != null)
                {
                    return;
                }
            }
            cupcakeTxt.SetActive(false);
            annoyanceType = Annoyance.NONE;
            cupcakeAdded = false;
        }
    }
}
