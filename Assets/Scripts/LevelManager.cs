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
    public static LevelManager instance;

    [Header("Player Related")]
    [Space]
    public GameObject player;
    public GameObject playerCanvas;
    public bool isDead = false;
    [Space(15f)]

    [Header("Audio Sources and Clips")]
    [Space]
    public AudioSource elevatorDing;
    public AudioSource elevatorSound;
    public AudioSource globalMusic;
    public AudioSource liftPanel;
    public AudioClip hangUpCallClip;
    [Space(15f)]

    [Header("Level Related Variables")]
    [Space]
    public int totKilled = 0;
    public GameObject[] zombies;
    public GameObject floor0;
    public GameObject[] floor;
    HashSet<int> playedIndex = new();
    public bool[] played;
    [Space(15f)]

    [Header("UI Gameobjects")]
    [Space]
    public GameObject[] deadScreen;
    public GameObject loadScreen;
    public GameObject pauseScreen;
    public GameObject nextFloorScreen;
    public GameObject winScreen;
    public GameObject skipDialogueUI;
    [Space(15f)]

    [Header("Win Screen")]
    [Space]
    public GameObject liftWall;
    public GameObject winScene;
    [Space(15f)]

    [Header("Ability (Wind)")]
    [Space]
    public float time = 0;
    public bool windCalled = false;
    public float windTime;

    int level = 0;
    int curLevelIndex;

    bool cursorLocked = false;

    private void Awake()
    {
        instance = this;    
    }

    public void EnablePlayerControls()
    {
        Debug.LogError(LevelAnnoyances.status);
        if (LevelAnnoyances.status == false)
        {
            Debug.LogError("From LevelManager's Enable Function\t" + LevelAnnoyances.status);
            player.GetComponent<Playermovement>().enabled = true;
            player.GetComponentInChildren<FPSCameraScript>().enabled = true;
            //player.GetComponentInChildren<WeaponesHandler>().enabled = true;
            player.GetComponentInChildren<Gun>().enabled = true;
        }

        //player.GetComponent<Playermovement>().enabled = true;
        //player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        //player.GetComponentInChildren<Gun>().enabled = true;
    }

    public void DisablePlayerControls()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
    }

    public void BakeNavMesh()
    {
        GetComponent<NavMeshSurface>().RemoveData();
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void GetZomies()
    {
        Array.Clear(zombies, 0, zombies.Length);
        zombies = new GameObject[0];
        zombies = GameObject.FindGameObjectsWithTag("zombies");
    }

    public void SkipLevel()
    {
        // Uncomment the line below if need to disable global annoyance upon skip
        //GlobalAnnoyanceManager.Instance.DisableGlobalAnnoyances();

        level++;
        foreach (GameObject go in zombies)
        {
            Destroy(go);
        }
        Array.Clear(zombies, 0, zombies.Length);
        zombies = new GameObject[0];
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
            if (go != null) {
                go.SetActive(false);
                Destroy(go);
            }
        }
        //yield return new WaitForEndOfFrame();
        Array.Clear(zombies, 0, zombies.Length);
        zombies = new GameObject[0];
        totKilled = 0;

        GetComponent<NavMeshSurface>().RemoveData();

        UnLoadDeadScreen(0);

        floor[curLevelIndex].SetActive(true);
        GetComponent<NavMeshSurface>().BuildNavMesh();
        level--;
        GlobalAnnoyanceManager.Instance.StatLevelUpdate(-1);
        floor[curLevelIndex].GetComponent<ZombieSpawner>().SpawnZombies(level);
        zombies = GameObject.FindGameObjectsWithTag("zombies");
    }

    public void Reset()
    {
        // Ensure the game is not paused when reloading the scene
        Time.timeScale = 1f;

        // Load the first level or main menu (scene 0)
        SceneManager.LoadScene(1);
    }

    public void StartTimer()
    {
        TimerController.instance.BeginTimer();
    }

    public void RandomStart()
    {
        if (playedIndex.Count < played.Length)
        {
            bool added;
            do
            {
                curLevelIndex = UnityEngine.Random.Range(0, played.Length);
                added = playedIndex.Add(curLevelIndex);

            } while (!added);

            if (added)
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
                GlobalAnnoyanceManager.Instance.StatLevelUpdate();
                foreach (GameObject go in zombies)
                {
                    if(go != null)
                    {
                        Destroy(go);
                    }
                }
                Array.Clear(zombies, 0, zombies.Length);
                zombies = new GameObject[0];
                zombies = GameObject.FindGameObjectsWithTag("zombies");
                played[curLevelIndex] = true;
                return;
            }
        }
        else
        {
            GetComponent<NavMeshSurface>().RemoveData();
            AbilityManager.instance.AbilityBoxClear();
            TimerController.instance.timerGoing = false;
            globalMusic.Stop();
            elevatorDing.Play();
            StartCoroutine(WinCoroutine());
        }
    }

    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(1);
        elevatorSound.Play();
        Win();
    }

    void Win()
    {
        //winScreen.SetActive(true);
        //player.GetComponent<Playermovement>().enabled = false;
        //player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        //player.GetComponentInChildren<Gun>().enabled = false;
        //playerCanvas.SetActive(false);
        //StartCoroutine(FadeImage(true));
        GlobalAnnoyanceManager.Instance.DisableGlobalAnnoyances();  
        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().OpenDoor();
        liftWall.SetActive(false);
        winScene.SetActive(true);
        //winScreen.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Programmer: Kushagra\nArt: Jeremy\n Programmer: Kevin\n Programmer: Luca\n Emotional Support: Flynn, McKenzie, Matt";
        //winScreen.gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        if (fadeAway)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime / 3)
            {
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
        floor[curLevelIndex].SetActive(false);
        RandomStart();
    }

    public void LoadDeadScreen(int index)
    {
        var masterCanvas = deadScreen[index].GetComponentInParent<Canvas>();
        masterCanvas.sortingOrder = 999;
        isDead = true;
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
        deadScreen[index].SetActive(true);
    }

    public void UnLoadDeadScreen(int index)
    {
        var masterCanvas = deadScreen[index].GetComponentInParent<Canvas>();
        masterCanvas.sortingOrder = 100;
        isDead = false;
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
        deadScreen[index].SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        globalMusic.Play();
        liftPanel.Play();
        StartCoroutine(DisableHangupCallText());
        skipDialogueUI.SetActive(true);
        StartCoroutine(SetSubtitle());
        StartCoroutine(SceneSetup());
    }

    IEnumerator DisableHangupCallText()
    {
        yield return new WaitForSeconds(liftPanel.clip.length);
        skipDialogueUI.SetActive(false);
    }

    IEnumerator SetSubtitle()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(SubtitleManager.Instance.StartSubtitle());
    }

    IEnumerator SceneSetup()
    {
        player.GetComponent<Playermovement>().enabled = false;
        player.GetComponentInChildren<FPSCameraScript>().enabled = false;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = false;
        player.GetComponentInChildren<Gun>().enabled = false;
        yield return new WaitForSeconds(0);
        loadScreen.SetActive(false);
        player.GetComponent<Playermovement>().enabled = true;
        player.GetComponentInChildren<FPSCameraScript>().enabled = true;
        //player.GetComponentInChildren<WeaponesHandler>().enabled = true;
        player.GetComponentInChildren<Gun>().enabled = true;
        playerCanvas.SetActive(true);
        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().OpenDoor();
    }

    void Update()
    {
        if(skipDialogueUI.activeInHierarchy && Input.GetKeyDown(KeyCode.V))
        {
            liftPanel.Stop();
            liftPanel.clip = hangUpCallClip;
            liftPanel.Play();
            SubtitleManager.Instance.StopSubtitles();
            skipDialogueUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cursorLocked = !cursorLocked;
            Cursor.visible = !cursorLocked;
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.Confined;
        }

        // Check if the dead screen is active and Q is pressed
        if (isDead && Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }

        // Check if the pause screen is active and Q is pressed
        if (pauseScreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
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

