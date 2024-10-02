using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;

    public GameObject[] spawnPoints = new GameObject[10];
    public Transform[] spawnPointsTrans = new Transform[10];

    public struct zombieInfo
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

    [SerializeField] public zombieInfo[] zombieInfos = new zombieInfo[10];

    // Start is called before the first frame update
    private void Start()
    {
        zombieInfos[0].setValues(2, 60, 100);
        zombieInfos[1].setValues(3, 65, 105);
        zombieInfos[2].setValues(4, 70, 110);
        zombieInfos[3].setValues(5, 75, 115);
        zombieInfos[4].setValues(6, 80, 120);
        zombieInfos[5].setValues(7, 85, 125);
        zombieInfos[6].setValues(8, 90, 130);
        zombieInfos[7].setValues(9, 95, 135);
        zombieInfos[8].setValues(10, 100, 140);
        zombieInfos[9].setValues(11, 105, 145);

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

        for (int i = 0; i < spawnPoints.Length; ++i)
        {
            spawnPointsTrans[i] = spawnPoints[i].GetComponent<Transform>();
        }

        SpawnZombies(0);
    }
    void OnEnable()
    {
        zombieInfos[0].setValues(2, 60, 100);
        zombieInfos[1].setValues(3, 65, 105);
        zombieInfos[2].setValues(4, 70, 110);
        zombieInfos[3].setValues(5, 75, 115);
        zombieInfos[4].setValues(6, 80, 120);
        zombieInfos[5].setValues(7, 85, 125);
        zombieInfos[6].setValues(8, 90, 130);
        zombieInfos[7].setValues(9, 95, 135);
        zombieInfos[8].setValues(10, 100, 140);
        zombieInfos[9].setValues(11, 105, 145);

        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

        for (int i = 0; i < spawnPoints.Length; ++i)
        {
            spawnPointsTrans[i] = spawnPoints[i].GetComponent<Transform>();
        }
    }

    public void SpawnZombies(int index = 0)
    {
        for (int i = 0; i < zombieInfos[index].magnitude; i++)
        {
            Debug.Log("instantiate");
            GameObject go = Instantiate(zombiePrefab, spawnPointsTrans[i].position, zombiePrefab.transform.rotation, this.transform);
            go.GetComponent<EnemyBehaviour>().health = zombieInfos[index].health;
            go.GetComponent<EnemyBehaviour>().healthSlider.maxValue = zombieInfos[index].health;
            go.GetComponent<EnemyFollow>().speed = zombieInfos[index].speed + Random.Range(0, zombieInfos[index].randomRange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}