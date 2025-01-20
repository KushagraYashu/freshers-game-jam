using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;

    public int gobalAnnoyanceIndex = -1;

    public GameObject[] spawnPoints;

    public struct ZombieInfo
    {
        public int magnitude;
        public float speed;
        public int health;

        public float randomRange;

        public void setValues(int magnitude, float speed, int health)
        {
            this.magnitude = magnitude;
            this.speed = speed;
            this.health = health;
            this.randomRange = 5.0f;
        }
    }

    public ZombieInfo[] zombieInfos = new ZombieInfo[21]; //21 total number of levels

    // Start is called before the first frame update
    private void Start()
    {

    }
    void OnEnable()
    {
        for(int i=0; i < zombieInfos.Length; i++) { 
            if (i == 0)
            {
                zombieInfos[i].setValues(2, 1f, 100);
            }
            else if(i == 1)
            {
                zombieInfos[i].setValues(4, 1f + 0.25f, 120);
            }
            else
            {
                zombieInfos[i].setValues(zombieInfos[i - 1].magnitude + 2, zombieInfos[i - 1].speed + 0.25f, zombieInfos[i - 1].health + 20);
            }
        }

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        GlobalAnnoyanceManager.Instance.SpawnGlobalAnnoyance(gobalAnnoyanceIndex);

        // dev purposes only, remove later
        //LevelManager.instance.BakeNavMesh();
        //SpawnZombies(2);
        //LevelManager.instance.GetZomies();
    }

    public void SpawnZombies(int index)
    {
        for(int i = 0; i < zombieInfos[index].magnitude; i++)
        {
            int spawnPtIndex = Random.Range(0, spawnPoints.Length);
            var zombie = Instantiate(zombiePrefab, spawnPoints[spawnPtIndex].GetComponent<Transform>().position, Quaternion.identity);
            zombie.GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 0.75f); //hardcoding it to be .75 in size because doing so makes navmesh works, i know its stupid, its either this or make the agent go to the player's feet instead of the player, lets see what we pick
            zombie.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            zombie.GetComponent<NavMeshAgent>().enabled = true;
            zombie.GetComponent<NavMeshAgent>().Warp(spawnPoints[spawnPtIndex].GetComponent<Transform>().position);
            zombie.GetComponent<NavMeshAgent>().speed = Random.Range(zombieInfos[index].speed, zombieInfos[index].speed - .5f);
            zombie.GetComponent<EnemyBehaviour>().health = (int)Random.Range(zombieInfos[index].health, zombieInfos[index].health - 10f);
            zombie.GetComponentInChildren<Slider>().maxValue = zombie.GetComponent<EnemyBehaviour>().health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}