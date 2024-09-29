using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayableMenuButtons : MonoBehaviour
{
    public Button button;

    public LevelManager levelManager;

    public bool hit = false;

    public void StartGame()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        GameObject.FindGameObjectWithTag("liftDoors").GetComponent<DoorOpening>().CloseDoor();
        StartCoroutine(LoadDelay());
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(3);
        levelManager.RandomStart();
    }

    public void Options()
    {
        Debug.Log("Options");
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
