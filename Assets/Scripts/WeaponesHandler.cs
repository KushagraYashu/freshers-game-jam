using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponesHandler : MonoBehaviour
{
    public GameObject[] weapons;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWeapon(2);
        }
    }
    void ActivateWeapon(int index)
    {
        foreach(GameObject w in weapons)
        {
            if (w.gameObject.GetComponent<Gun>().isReloading)
            {
                return;
            }
        }

        if (weapons[index].gameObject.activeInHierarchy)
        {
            weapons[index].SetActive(!weapons[index].gameObject.activeInHierarchy);
        }
        else
        {
            foreach (var weapon in weapons) { 
                weapon.SetActive(false);
            }
            weapons[index].SetActive(true);
        }
    }

}
