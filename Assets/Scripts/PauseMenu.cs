using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // Tracks if the game is paused
    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public GameObject settingsMenuUI;

    private FPSCameraScript playerController; // Reference to your player controller (or input handler)

    void Start()
    {
        playerController = FindObjectOfType<FPSCameraScript>(); // Find the player controller script
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.instance.isDead && LevelManager.instance.NameEntered)
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
        

        var masterCanvas = pauseMenuUI.GetComponentInParent<Canvas>();
        masterCanvas.sortingOrder = 100;
        pauseMenuUI.SetActive(false); // Hide the pause menu
        LevelManager.instance.ResumeZombieSound();
        LevelManager.instance.liftPanel.Play();
        LevelManager.instance.EnablePlayerControls();
        Time.timeScale = 1f; // Resume time
        LevelManager.instance.LockCursor(0);
        isPaused = false;

        // Enable player movement/input
        playerController.enabled = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Pauses the game
    public void Pause()
    {
        

        var masterCanvas = pauseMenuUI.GetComponentInParent<Canvas>();
        masterCanvas.sortingOrder = 999;
        pauseMenuUI.SetActive(true); // Show the pause menu
        LevelManager.instance.PauseZombieSound();
        LevelManager.instance.liftPanel.Pause();
        LevelManager.instance.DisablePlayerControls();
        Time.timeScale = 0f; // Stop time
        LevelManager.instance.LockCursor(1);
        isPaused = true;

        // Disable player movement/input
        playerController.enabled = false;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void SettingsMenu()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void BackToPause()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
