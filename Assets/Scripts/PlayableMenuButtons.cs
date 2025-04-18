using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableMenuButtons : MonoBehaviour
{
    public bool hit = false;

    public GameObject optionsUI;

    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().CloseDoor();
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(3);
        LevelManager.instance.StartTimer();
        LevelManager.instance.RandomStart();
    }

    public void Options()
    {
        LevelManager.instance.LockCursor(1);
        LevelManager.instance.liftPanel.Pause();
        LevelManager.instance.DisablePlayerControls();
        Time.timeScale = 0f;
        optionsUI.SetActive(true);
    }

    public void BackFromOptions()
    {
        LevelManager.instance.LockCursor(0);
        LevelManager.instance.liftPanel.Play();
        Time.timeScale = 1f;
        LevelManager.instance.EnablePlayerControls();
        optionsUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            string name = gameObject.name;
            switch (name)
            {
                case "START":
                    StartGame();
                    hit = false;
                    break;
                case "OPTIONS":
                    Options();
                    hit = false;
                    break;
                case "QUIT":
                    Quit();
                    hit = false;
                    break;
            }
        }
    }
}
