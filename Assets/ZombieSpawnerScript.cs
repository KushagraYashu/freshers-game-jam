using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject zombie;
    
    public Vector3 origin = Vector3.zero;
    public float radius = 10;

    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            SpawnObject();
        }

        // Finds a position in a sphere with a radius of 10 units.
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        Instantiate(zombie, randomPosition, Quaternion.identity);
    }

    void SpawnObject()
    {
        // Finds a position in a sphere with a radius of 10 units.
        Vector3 randomPosition = origin + Random.insideUnitSphere * radius;
        Instantiate(zombie, randomPosition, Quaternion.identity);
    }

}
