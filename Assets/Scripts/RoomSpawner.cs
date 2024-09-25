using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] rooms;
    public int totLvl;


    // Start is called before the first frame update
    void Start()
    {
        rooms = new GameObject[totLvl];
        rooms = GameObject.FindGameObjectsWithTag("rooms");
        foreach (GameObject room in rooms)
        {
            room.SetActive(false);
        }
        rooms[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ActivateRoom(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ActivateRoom(1);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ActivateRoom(2);

        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ActivateRoom(3);

        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ActivateRoom(4);

        }
    }

    void ActivateRoom(int index)
    {
        if (NoOtherActive(index))
        {
            rooms[index].SetActive(!rooms[index].gameObject.activeInHierarchy);
        }
    }

    bool NoOtherActive(int index)
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (i != index)
            {
                if (rooms[i].gameObject.activeInHierarchy)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
