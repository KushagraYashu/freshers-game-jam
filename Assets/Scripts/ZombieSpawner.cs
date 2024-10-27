using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;

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
        for(int i=0; i<zombieInfos.Length;i++) {
            if (i == 0)
            {
                zombieInfos[i].setValues(2, 0.5f, 100);
            }
            else
            {
                zombieInfos[i].setValues(zombieInfos[i-1].magnitude + 2, zombieInfos[i-1].speed + 0.25f, zombieInfos[i-1].health + 20);
            }
        }

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");
        SpawnZombies(0);
    }

    public void SpawnZombies(int index)
    {
        for(int i = 0; i < zombieInfos[index].magnitude; i++)
        {
            int spawnPtIndex = Random.Range(0, spawnPoints.Length);
            var zombie = Instantiate(zombiePrefab, spawnPoints[spawnPtIndex].gameObject.GetComponent<Transform>().position, Quaternion.identity);
            //zombie.gameObject.GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 0.75f); //hardcoding it to be .75 in size because doing so makes navmesh works, i know its stupid, its either this or make the agent go to the player's feet instead of the player, lets see what we pick
            zombie.gameObject.GetComponent<NavMeshAgent>().enabled = true;
            zombie.gameObject.GetComponent<NavMeshAgent>().Warp(spawnPoints[spawnPtIndex].gameObject.GetComponent<Transform>().position);
            zombie.gameObject.GetComponent<NavMeshAgent>().speed = zombieInfos[index].speed;
            zombie.gameObject.GetComponent<EnemyBehaviour>().health = zombieInfos[index].health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}