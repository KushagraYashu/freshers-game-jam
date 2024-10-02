using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponesHandler : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject akUION;
    public GameObject akUIOFF;
    public GameObject pistolUION;
    public GameObject pistolUIOFF;
    public GameObject m4UION;
    public GameObject m4UIOFF;

    void Start()
    {
        //akUI.SetActive(true);
        akUIOFF.SetActive(false);
        akUION.SetActive(true);
        pistolUION.SetActive(false);
        pistolUIOFF.SetActive(true);
        m4UION.SetActive(false);
        m4UIOFF.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateWeapon(0);
            //akUI.SetActive(true);
            akUIOFF.SetActive(false);
            akUION.SetActive(true);
            pistolUION.SetActive(false);
            pistolUIOFF.SetActive(true);
            m4UION.SetActive(false);
            m4UIOFF.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateWeapon(1);
            //akUI.SetActive(false);
            akUIOFF.SetActive(true);
            akUION.SetActive(false);
            pistolUION.SetActive(false);
            pistolUIOFF.SetActive(true);
            m4UION.SetActive(true);
            m4UIOFF.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateWeapon(2);
            //akUI.SetActive(false);
            akUIOFF.SetActive(true);
            akUION.SetActive(false);
            pistolUION.SetActive(true);
            pistolUIOFF.SetActive(false);
            m4UION.SetActive(false);
            m4UIOFF.SetActive(true);
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

        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[index].SetActive(true);
    }

}
