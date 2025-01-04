using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnnoyanceTicketPrefab : MonoBehaviour
{
    public int curTicketNo;

    public void TicketStamp(GameObject ticket)
    {
        curTicketNo += 1;

        ColorBlock colorBlock = new()
        {
            pressedColor = new Color(72 / 255f, 72 / 255f, 72 / 255f, 1),
            highlightedColor = new Color(72 / 255f, 72 / 255f, 72 / 255f, 1),
            normalColor = new Color(72 / 255f, 72 / 255f, 72 / 255f, 1),
            disabledColor = new Color(72 / 255f, 72 / 255f, 72 / 255f, 1)
        };
        ticket.GetComponent<Button>().colors = colorBlock;
        ticket.GetComponent<Button>().interactable = false;

        var ticketTMP = ticket.GetComponentInChildren<TextMeshProUGUI>();
        ticketTMP.transform.Rotate(new Vector3(0, 0, 30));
        ticketTMP.fontSize = 20;
        ticketTMP.color = Color.red;
        ticketTMP.text = "STAMPED";
    }
}
