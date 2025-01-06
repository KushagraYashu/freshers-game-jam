using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyanceWireMatching : LevelAnnoyances
{
    public GameObject wireMatchUI;
    public GameObject lights;
    bool started;
    string wire1Color;
    GameObject wire1;
    string wire2Color;
    GameObject wire2;
    public int totWire;
    int curWire;

    // Update is called once per frame
    void Update()
    {
        switch (annoyanceType)
        {
            case Annoyance.NONE:
                break;

            case Annoyance.WIRE_MATCH:
                if (!started)
                {
                    wireMatchUI.SetActive(true);
                    started = true;
                }
                break;
        }
    }

    public void AddWireColor(string wire1Color)
    {
        this.wire1Color = wire1Color;
    }

    public void AddWireGO(GameObject wire1)
    {
        this.wire1 = wire1;
    }
    public void AddWire2GO(GameObject wire2)
    {
        this.wire2 = wire2;
    }

    public void CompareWire(string wire2)
    {
        wire2Color = wire2;
        if (started)
        {
            if (string.Equals(wire1Color, wire2Color))
            {
                StartCoroutine(Verified());
                if(wire1Color == "yellow")
                {
                    this.wire1.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                    this.wire2.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                }
            }
        }
    }

    IEnumerator Verified()
    {
        curWire++;
        ColorBlock colorBlock = new()
        {
            pressedColor = Color.black,
            highlightedColor = Color.black,
            normalColor = Color.black,
            disabledColor = Color.black
        };
        wire1.GetComponent<Button>().colors = colorBlock;
        wire1.GetComponent<Button>().interactable = false;
        wire2.GetComponent<Button>().colors = colorBlock;
        wire2.GetComponent<Button>().interactable = false;
        if (curWire == totWire)
        {
            lights.SetActive(true);
            annoyanceType = Annoyance.NONE;
            yield return new WaitForSeconds(1f);
            wireMatchUI.SetActive(false);
        }
    }
}