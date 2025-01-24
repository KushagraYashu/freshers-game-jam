using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // Tracks if the game is paused
    public GameObject pauseMenuUI; // Reference to the pause menu UI

    private FPSCameraScript playerController; // Reference to your player controller (or input handler)

    void Start()
    {
        playerController = FindObjectOfType<FPSCameraScript>(); // Find the player controller script
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.instance.isDead) // Toggle pause on pressing P (instead of ESC)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Resumes the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        LevelManager.instance.liftPanel.Play();
        LevelManager.instance.EnablePlayerControls();
        Time.timeScale = 1f; // Resume time
        isPaused = false;

        // Enable player movement/input
        playerController.enabled = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Pauses the game
    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu
        LevelManager.instance.liftPanel.Pause();
        LevelManager.instance.DisablePlayerControls();
        Time.timeScale = 0f; // Stop time
        isPaused = true;

        // Disable player movement/input
        playerController.enabled = false;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    // Optional: To use this on a button click to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
