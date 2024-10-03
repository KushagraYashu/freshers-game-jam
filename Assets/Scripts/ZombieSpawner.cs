using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    

    public GameObject[] zombies;

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
        zombieInfos[9].setValues(11, 105, 145);
        zombieInfos[10].setValues(11, 105, 145);
        zombieInfos[11].setValues(11, 105, 145);
        zombieInfos[12].setValues(11, 105, 145);
        zombieInfos[13].setValues(11, 105, 145);
        zombieInfos[14].setValues(11, 105, 145);
        zombieInfos[15].setValues(11, 105, 145);

        
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

        
    }

    public void SpawnZombies(int index)
    {
        zombies = GameObject.FindGameObjectsWithTag("zombies");
        foreach (var zombie in zombies)
        {
            zombie.SetActive(false);
        }
        for (int i = 0; i < zombieInfos[index].magnitude; i++)
        {
            zombies[i].SetActive(true);
            zombies[i].GetComponent<EnemyBehaviour>().health = zombieInfos[index].health;
            zombies[i].GetComponent<EnemyBehaviour>().healthSlider.maxValue = zombieInfos[index].health;
            zombies[i].GetComponent<EnemyFollow>().speed = zombieInfos[index].speed + Random.Range(0, zombieInfos[index].randomRange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}