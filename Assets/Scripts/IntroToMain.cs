using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class LoadSceneAfterDelay : MonoBehaviour
{
    public float delay = 10f; // Delay in seconds

    public TextMeshProUGUI loadingTxt;
    public Image loadingTxtBG;

    void Start()
    {
        // Start the coroutine to load the scene after the delay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(LoadSceneWithDelay());

    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(1);
        loadingTxtBG.enabled = true;
        while (!loadingOperation.isDone)
        {
            loadingTxt.text = "Loading: " + loadingOperation.progress * 100 + "%";
            yield return null;
        }
    }
}