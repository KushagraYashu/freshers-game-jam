using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public AudioSource elevatorDing;
    public AudioSource elevatorSound;
    public AudioSource globalMusic;

    public int totKilled = 0;

    public GameObject[] zombies;

    public GameObject floor0;

    public GameObject[] floor = new GameObject[4];
    public bool[] played;

    public GameObject deadScreen;
    public GameObject loadScreen;
    public GameObject pauseScreen;
    public GameObject nextFloorScreen;
    public GameObject winScreen;

    public GameObject player;
    public GameObject playerCanvas;

    int maxSteps = 60;
    int curStep = 0;
    int level = 0;
    int curLevelIndex;

    bool cursorLocked = false;

    public float time = 0;
    public bool windCalled = false;
    public float windTime;

    public void SkipLevel()
    {
        level++;
        foreach (GameObject go in zombies)
        {
            Destroy(go);
        }
        Array.Clear(zombies, 0, zombies.Length);
        zombies = null;
        totKilled = 0;

        for (int i = 0; i < played.Length; i++) {
            if (!played[i])
            {
                played[i] = true;
                break;
            }
        }
        
            StartCoroutine(LoadDelay());
    }

    public void Retry()
    {
        foreach (GameObject go in zombies)
        {
            Destroy(go);
        }
        Array.Clear(zombies, 0, zombies.Length);
        zombies = null;
        totKilled = 0;

        GetComponent<NavMeshSurface>().RemoveData();

        UnLoadDeadScreen();

        floor[curLevelIndex].SetActive(true);
        GetComponent<NavMeshSurface>().BuildNavMesh();
        floor[curLevelIndex].GetComponent<ZombieSpawner>().SpawnZombies(level);
        zombies = GameObject.FindGameObjectsWithTag("zombies");

    }

    public void Reset()
    {
        // Ensure the game is not paused when reloading the scene
        Time.timeScale = 1f;

        // Load the first level or main menu (scene 0)
        SceneManager.LoadScene(0);
    }

    public void StartTimer()
    {
        TimerController.instance.BeginTimer();
    }

    public void RandomStart()
    {
        curStep = 0;
        while (curStep <= maxSteps)
        {
            curStep++;
            curLevelIndex = UnityEngine.Random.Range(0, played.Length);
            if (played[curLevelIndex])
            {
                continue;
            }
            else
            {
                floor0.SetActive(false);
                foreach (GameObject go in floor)
                {
                    go.SetActive(false);
                }
                GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().OpenDoor();
                AbilityManager.instance.AbilityBoxClear();
                floor[curLevelIndex].SetActive(true);
                AbilityManager.instance.RandomAbilitySpawn();
                GetComponent<NavMeshSurface>().RemoveData();
                GetComponent<NavMeshSurface>().BuildNavMesh();
                floor[curLevelIndex].GetComponent<ZombieSpawner>().SpawnZombies(level);
                level++;
                foreach (GameObject go in zombies)
                {
                    Destroy(go);
                }
                Array.Clear(zombies, 0, zombies.Length);
                zombies = GameObject.FindGameObjectsWithTag("zombies");
                played[curLevelIndex] = true;
                return;
            }
        }
        winScreen.SetActive(true);
        TimerController.instance.timerGoing = false;
        globalMusic.Stop();
        elevatorDing.Play();
        StartCoroutine(SoundDelay());
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(1);
        elevatorSound.Play();
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
        playerCanvas.SetActive(false);
        StartCoroutine(FadeImage(true));
        winScreen.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Programmer: Kushagra\nArt: Jeremy\n Programmer: Kevin\n Programmer: Luca\n Emotional Support: Flynn, McKenzie, Matt";
        winScreen.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;


    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0; i <= 1; i += Time.deltaTime / 3)
            {
                // set color with i as alpha
                winScreen.GetComponent<Image>().color = new Color(1, 1, 1, i);
                yield return null;
            }
        }


    }

    public void ZombieCheck()
    {
        totKilled++;
        if (totKilled == zombies.Length)
        {
            StartCoroutine(LoadDelay());

        }
    }

    public IEnumerator LoadDelay()
    {
        foreach (bool b in played)
        {
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

        yield return new WaitForSeconds(1f);

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

    public void UnLoadDeadScreen()
    {
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
        deadScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(SceneSetup());
    }

    IEnumerator SceneSetup()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
        yield return new WaitForSeconds(3f);
        loadScreen.SetActive(false);
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
        playerCanvas.SetActive(true);
        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().OpenDoor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cursorLocked = !cursorLocked;
            Cursor.visible = !cursorLocked;
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
        }

        // Check if the dead screen is active and Q is pressed
        if (deadScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            //Reset();
        }

        // Check if the win screen is active and Q is pressed
        if (winScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            //Reset();
        }

        // Check if the pause screen is active and Q is pressed
        if (pauseScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            //Reset();
        }

        // Optionally, check for Q key press when the game is not over
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            Reset(); // This will reset the game to the main menu
        }*/
    }
}

