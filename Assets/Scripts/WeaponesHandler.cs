using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponesHandler : MonoBehaviour
{
    public GameObject[] weapons;

    public static WeaponesHandler instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ActivateWeapon(int index)
    {
        foreach (var weapon in weapons) {
            if (weapon.activeInHierarchy)
            {
                weapon.SetActive(false);
            }
        }
        weapons[index].SetActive(true);
    }
}
