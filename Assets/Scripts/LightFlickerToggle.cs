using UnityEngine;

public class LightFlickerToggle : MonoBehaviour
{
    public Light myLight;            // Reference to the light component
    public GameObject glass;         // Reference to the glass object
    public float maxInterval = 1f;   // Maximum time the light stays on
    public float maxFlicker = 0.2f;  // Maximum time the light flickers off

    float defaultIntensity;          // Store the default intensity of the light
    bool isOn = true;                // Track if the light is currently on
    float timer = 0f;                // Timer to track time between toggles
    float delay = 0f;                // Time between each toggle (on/off)

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
        timer += Time.deltaTime;

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
            delay = Random.Range(0, maxInterval);  // Set the delay to a longer interval
        }
        else
        {
            // If the light is off, flicker at a lower intensity
            myLight.intensity = Random.Range(0.6f, defaultIntensity);
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
