using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowPopupClose : MonoBehaviour
{
    public void ClosePopup(GameObject windowPopup)
    {
        GlobalAnnoyanceWindowPopup.instance.curClosed++;
        Destroy(windowPopup);
    }
}