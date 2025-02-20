using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelAnnoyanceCupcake : LevelAnnoyances
{
    public GameObject cupcakePrefab;
    public GameObject cupcakeTxt;
    public GameObject cupcakeSpawnPts;
    List<Transform> spawnPts = new();
    List<GameObject> cupcakes = new();
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
                        var children = cupcakeSpawnPts.GetComponentsInChildren<SpawnPoint>();
                        foreach (var child in children) {
                            spawnPts.Add(child.transform);
                        }
                        foreach (var spawnPoint in spawnPts)
                        {
                            cupcakes.Add(Instantiate(cupcakePrefab, new Vector3(spawnPoint.position.x, 5.0f, spawnPoint.position.z), cupcakePrefab.transform.rotation, spawnPoint));
                        }
                        cupcakeTxt.SetActive(true);
                        cupcakeAdded = true;
                        status = cupcakeAdded;

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
            status = cupcakeAdded;

        }
    }
    private void OnDisable()
    {
        status = false;
    }
}
