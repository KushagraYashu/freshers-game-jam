using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject floor0;

    public GameObject[] floor = new GameObject[4];
    public bool[] played = new bool[4];

    public GameObject deadScreen;

    public GameObject player;

    public void RandomStart()
    {
        while (true)
        {
            int index = Random.Range(0, floor.Length);
            if (played[index])
            {
                continue;
            }
            else
            {

            }
        }
    }

    public void LoadDeadScreen()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
        deadScreen.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
