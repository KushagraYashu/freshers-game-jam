using UnityEngine;

public class LightCutscene : MonoBehaviour
{
    public Light myLight;            // Reference to the light component
    public GameObject glass;         // Reference to the glass object
    public float maxInterval = 1f;   // Maximum time the light stays on
    public float maxFlicker = 0.2f;  // Maximum time the light flickers off
    public float increaseIntervalAfter = 30f; // Time after which flicker interval increases
    public float increaseRate = 0.1f;  // Rate at which the flicker interval increases

    float defaultIntensity;          // Store the default intensity of the light
    bool isOn = true;                // Track if the light is currently on
    float timer = 0f;                // Timer to track time between toggles
    float delay = 0f;                // Time between each toggle (on/off)
    float timeSinceStart = 0f;       // Timer to track how long the cutscene has been playing
    public float offIntensityMin = 0.1f; // Minimum intensity for the off state (darker)

    private void Start()
    {
        // Store the light's default intensity
        defaultIntensity = myLight.intensity;

        // Check if the glass object is assigned
        if (glass == null)
        {
            Debug.LogError("Glass object is not assigned!");
        }
    }

    void Update()
    {
        timeSinceStart += Time.deltaTime; // Update time since start of the cutscene
        timer += Time.deltaTime;          // Update the light flicker timer

        // Gradually increase the time the light stays off after the specified time
        if (timeSinceStart > increaseIntervalAfter)
        {
            maxFlicker += increaseRate * Time.deltaTime;
        }

        // Check if it's time to toggle the light and glass
        if (timer > delay)
        {
            ToggleLightAndGlass();
        }
    }

    void ToggleLightAndGlass()
    {
        // Toggle the light on/off state
        isOn = !isOn;

        // If the light is on
        if (isOn)
        {
            myLight.intensity = defaultIntensity;  // Reset light to default intensity
            delay = Random.Range(0, maxInterval);  // Set the delay to a consistent interval
        }
        else
        {
            // If the light is off, flicker at a much lower intensity (darker)
            myLight.intensity = Random.Range(offIntensityMin, defaultIntensity);
            delay = Random.Range(0, maxFlicker);   // Set the delay to a shorter flicker interval
        }

        // Toggle the glass object on/off if it exists
        if (glass != null)
        {
            glass.SetActive(!glass.activeSelf);
        }

        // Reset the timer for the next toggle
        timer = 0f;
    }
}
