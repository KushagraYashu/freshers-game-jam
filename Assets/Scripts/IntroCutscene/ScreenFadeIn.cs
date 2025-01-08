using UnityEngine;
using UnityEngine.UI;  // Required for accessing UI elements

public class ScreenFadeIn : MonoBehaviour
{
    public Image fadeImage;       // Reference to the UI Image covering the screen
    public float fadeDuration = 3f;  // Time in seconds for the fade-in effect
    private float fadeTimer = 0f;    // Timer to track how long the fade has been happening

    void Start()
    {
        // Ensure the fade image is assigned
        if (fadeImage == null)
        {
            fadeImage = GetComponent<Image>();  // Automatically find the Image if it's on the same GameObject
        }

        // Set the fade image to fully opaque at the start
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);
    }

    void Update()
    {
        // Gradually reduce the alpha value over time to create the fade-in effect
        if (fadeImage.color.a > 0)
        {
            fadeTimer += Time.deltaTime;  // Increase the timer
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);  // Calculate the alpha value

            // Apply the new alpha to the fade image color
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        }
    }
}
