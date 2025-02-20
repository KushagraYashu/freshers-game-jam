using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class LevelAnnoyanceTicketManager : LevelAnnoyances
{
    public GameObject ticketStampUI;
    public GameObject tickets;
    bool started;
    int totTicketNo;
    Button[] ticketTransforms;

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.TICKET:
                    if (!started)
                    {
                        DisablePlayerControls();

                        tickets = Instantiate(tickets, ticketStampUI.transform);
                        ticketTransforms = tickets.GetComponentsInChildren<Button>();
                        foreach (Button button in ticketTransforms) {
                            button.gameObject.SetActive(false);    
                        }

                        totTicketNo = Random.Range(1, ticketTransforms.Length + 1);

                        HashSet<int> activated = new();
                        for (int i = 0; i < totTicketNo; i++)
                        {
                            int j;
                            do
                            {
                                j = Random.Range(0, ticketTransforms.Length);
                            } while (!activated.Add(j));

                            ticketTransforms[j].gameObject.SetActive(true);
                        }

                        ticketStampUI.SetActive(true);

                        started = true;
                        status = started;

                    }
                    break;

            }
        }

        if (started)
        {
            if(tickets.GetComponent<LevelAnnoyanceTicketPrefab>().curTicketNo == totTicketNo)
            {
                StartCoroutine(TicketsStamped());
            }
        }
    }

    IEnumerator TicketsStamped()
    {
        EnablePlayerControls();
        annoyanceType = Annoyance.NONE;

        yield return new WaitForSeconds(1);

        ticketStampUI.SetActive(false);
        started = false;
        status = started;

    }
    private void OnDisable()
    {
        status = false;
    }
}
