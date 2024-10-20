using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    int maxSteps = 20;
    int curStep = 0;
    int level = 0;

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
            int index = Random.Range(0, played.Length);
            if (played[index])
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
                floor[index].SetActive(true);
                floor[index].GetComponent<ZombieSpawner>().SpawnZombies(level);
                level++;
                zombies = GameObject.FindGameObjectsWithTag("zombies");
                played[index] = true;
                return;
            }
        }
        winScreen.SetActive(true);
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

    IEnumerator LoadDelay()
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
        Cursor.lockState = CursorLockMode.Locked;
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
        // Check if the dead screen is active and Q is pressed
        if (deadScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            Reset();
        }

        // Check if the win screen is active and Q is pressed
        if (winScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            Reset();
        }

        // Check if the pause screen is active and Q is pressed
        if (pauseScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Q))
        {
            Reset();
        }

        // Optionally, check for Q key press when the game is not over
        /*if (Input.GetKeyDown(KeyCode.Q))
        {
            Reset(); // This will reset the game to the main menu
        }*/
    }
}

