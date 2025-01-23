using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackButton : MonoBehaviour
{
    public void OpenFeedbackForm(string link)
    {
        Application.OpenURL(link);
    }
}
