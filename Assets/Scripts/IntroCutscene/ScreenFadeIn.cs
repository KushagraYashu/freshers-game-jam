using UnityEngine;
using UnityEngine.UI;  // Required for accessing UI elements

public class ScreenFadeIn : MonoBehaviour
{
    public Image fadeImage;         // Reference to the UI Image covering the screen
    public float fadeStartDelay = 2f; // Delay before the fade-in starts (in seconds)
    public float fadeDuration = 3f;  // Time in seconds for the fade-in effect
    public bool enableBlackout = false; // Toggle for blackout logic
    public float blackoutTime = 50f; // Time when the screen instantly turns black
    private float fadeTimer = 0f;    // Timer to track how long the fade has been happening
    private bool isFading = false;  // Tracks if the fade-in has started

    void Start()
    {
        // Ensure the fade image is assigned
        if (fadeImage == null)
        {
            fadeImage = GetComponent<Image>();  // Automatically find the Image if it's on the same GameObject
        }

        // Set the fade image to fully opaque at the start
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);

        // Start the fade-in process after the specified delay
        Invoke("StartFadeIn", fadeStartDelay);
    }

    void Update()
    {
        // Gradually reduce the alpha value over time to create the fade-in effect
        if (isFading && (!enableBlackout || fadeTimer < blackoutTime))
        {
            fadeTimer += Time.deltaTime;  // Increase the timer

            if (fadeTimer <= fadeDuration) // Fade in only within the duration
            {
                float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);  // Calculate the alpha value
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            }
        }

        // Instantly set the screen to black after blackoutTime if enabled
        if (enableBlackout && fadeTimer >= blackoutTime)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f); // Fully black
        }
    }

    // Starts the fade-in process
    private void StartFadeIn()
    {
        isFading = true;
        fadeTimer = 0f; // Reset the timer for a clean fade-in start
    }
}
