using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int totKilled = 0;

    public GameObject[] zombies;

    public GameObject floor0;

    public GameObject[] floor = new GameObject[4];
    public bool[] played = new bool[4];

    public GameObject deadScreen;
    public GameObject nextFloorScreen;
    public GameObject winScreen;

    public GameObject player;

    int maxSteps;
    int curStep = 0;


    public void RandomStart()
    {
        curStep = 0;
        while (curStep <= maxSteps)
        {
            curStep++;
            int index = Random.Range(0, floor.Length);
            if (played[index])
            {
                continue;
            }
            else
            {
                floor0.SetActive(false);
                foreach(GameObject go in floor)
                {
                    go.SetActive(false);
                }
                GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().OpenDoor();
                floor[index].SetActive(true);
                zombies = GameObject.FindGameObjectsWithTag("zombies");
                played[index] = true;
                return;
            }
        }
        winScreen.SetActive(true);
    }

    public void ZombieCheck()
    {
        totKilled++;
        if(totKilled == zombies.Length)
        {
            StartCoroutine(LoadDelay());
            
        }
    }

    IEnumerator LoadDelay()
    {
        foreach (bool b in played) {
            if (!b)
            {
                nextFloorScreen.SetActive(true);
            }
            else
            {
                continue;
            }
        }

        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().CloseDoor();

        yield return new WaitForSeconds(3f);

        nextFloorScreen.SetActive(false);
        totKilled = 0;
        RandomStart();
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
        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().OpenDoor();
        maxSteps = floor.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
