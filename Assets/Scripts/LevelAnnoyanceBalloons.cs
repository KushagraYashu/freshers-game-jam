using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelAnnoyances;

public class LevelAnnoyanceBalloons : LevelAnnoyances
{
    public GameObject[] balloonsPrefabs;
    public GameObject balloonsTxt;
    public GameObject spawnPointPrefab;
    List<GameObject> balloons = new();
    bool balloonsAdded = false;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.BALLOONS:
                    if (!balloonsAdded)
                    {
                        spawnPointPrefab = Instantiate(spawnPointPrefab, this.gameObject.transform);
                        var spawnPts = spawnPointPrefab.GetComponentsInChildren<Transform>();
                        foreach (var spawnPoint in spawnPts)
                        {
                            if(!spawnPoint.transform.name.Contains("BalloonSpawnPts"))
                            {
                                int index = Random.Range(0, balloonsPrefabs.Length);
                                balloons.Add(Instantiate(balloonsPrefabs[index], new Vector3(spawnPoint.transform.position.x, 10.5f, spawnPoint.transform.position.z), balloonsPrefabs[index].transform.rotation));
                            }
                        }
                        balloonsTxt.SetActive(true);
                        balloonsAdded = true;
                    }
                    break;

            }
        }

        if (balloonsAdded)
        {
            foreach (var balloon in balloons)
            {
                if (balloon.GetComponentInChildren<SphereCollider>() != null)
                {
                    return;
                }
            }

            foreach (var balloon in balloons)
            {
                Destroy(balloon);
            }
            balloonsTxt.SetActive(false);
            annoyanceType = Annoyance.NONE;
            balloonsAdded = false;
        }
    }
}
