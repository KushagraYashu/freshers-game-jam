using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnnoyanceCameraOverlay : LevelAnnoyances
{
    public CameraWipeOut camWipeOutScript;

    public GameObject overlayObject;

    public GameObject camOverlayUI;

    bool started = false;

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0, 100) == 0)
        {
            switch (annoyanceType)
            {
                case Annoyance.NONE:
                    break;

                case Annoyance.CAMERA_OVERLAY:
                    if (!started)
                    {
                        DisablePlayerControls();
                        camOverlayUI.SetActive(true);
                        overlayObject.SetActive(true);
                        camWipeOutScript.steam = overlayObject.transform;
                        camWipeOutScript.enabled = true;
                        started = true;
                    }
                    break;
            }
        }

        if(started && camWipeOutScript.enabled && camWipeOutScript.wiped)
        {
            EnablePlayerControls();
            camOverlayUI.SetActive(false);
            started = false;
            annoyanceType = Annoyance.NONE;
            Invoke(nameof(DisableOverlayItem), 3);
            camWipeOutScript.enabled = false;
        }
    }

    void DisableOverlayItem()
    {
        overlayObject.SetActive(false);
    }
}
