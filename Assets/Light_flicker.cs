using System.Collections;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource;        // Reference to the light component
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 1.5f; // Maximum light intensity
    public float flickerSpeed = 0.1f; // Speed of flickering

    private void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>(); // If not assigned, find Light component
        }

        // Start the flicker coroutine
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            // Randomly choose a new intensity within the range
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);

            // Wait for a short random time before changing the intensity again
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}