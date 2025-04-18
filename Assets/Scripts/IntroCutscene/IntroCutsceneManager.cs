using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroCutsceneManager : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pauseMenuParent;
    public GameObject pauseMenuScreen;
    public GameObject settingsMenuScreen;

    [Header("Loading UI")]
    public TextMeshProUGUI loadingTxt;
    public Image loadingTxtBG;

    //internal variables
    bool pause = false;
    CameraShakeCutscene cameraShakeScript;
    FPSCameraScript fpsCameraScript;
    AudioSource[] allAudioSources;
    List<AudioSource> playingAudioSources = new();

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraShakeScript = FindObjectOfType<CameraShakeCutscene>();
        fpsCameraScript = FindObjectOfType<FPSCameraScript>();

        allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach(AudioSource audio in allAudioSources)
        {
            if (audio.isPlaying)
                playingAudioSources.Add(audio);
        }

        Invoke(nameof(GoToMainMenu), 58f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { 
            PauseMenu();
        }
        if (pause)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void PauseMenu()
    {
        pause = !pause;
        if (pause)
        {
            pauseMenuParent.SetActive(true);
            cameraShakeScript.enabled = false;
            fpsCameraScript.enabled = false;
            foreach(AudioSource audio in allAudioSources)
            {
                if (audio.isPlaying)
                {
                    if(!playingAudioSources.Contains(audio))
                        playingAudioSources.Add(audio);
                    audio.Pause();
                }
            }
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuParent.SetActive(false);
            cameraShakeScript.enabled = true;
            fpsCameraScript.enabled = true;
            foreach (AudioSource audio in playingAudioSources)
            {
                audio.Play();
            }
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        pauseMenuParent.SetActive(false);
        loadingTxtBG.enabled = true;
        StartCoroutine(LoadMainScene());
    }

    IEnumerator LoadMainScene()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Time.timeScale = 1f;

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(1);
        while (!loadingOperation.isDone)
        {
            loadingTxt.text = "Loading: " + (loadingOperation.progress * 100).ToString("#.00") + "%";
            yield return null;
        }
    }

    public void Resume()
    {
        PauseMenu();
    }

    public void SettingsMenu()
    {
        pauseMenuScreen.SetActive(false);
        settingsMenuScreen.SetActive(true);
    }

    public void Back(int i) //0 for pause menu screen
    {
        if(i == 0)
        {
            settingsMenuScreen.SetActive(false);
            pauseMenuScreen.SetActive(true);
        }
    }
}
