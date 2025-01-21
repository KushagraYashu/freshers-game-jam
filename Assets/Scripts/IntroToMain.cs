using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneAfterDelay : MonoBehaviour
{
    public float delay = 10f; // Delay in seconds

    void Start()
    {
        // Start the coroutine to load the scene after the delay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Load the scene named "Combined Level"
        SceneManager.LoadScene("CombinedLevel");
    }
}