using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.5f; // Duration of the shake
    public float magnitude = 0.3f; // Magnitude of the shake

    private Vector3 originalPosition;
    private float shakeTimeRemaining;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * magnitude;
            shakeTimeRemaining -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }

    public void TriggerShake()
    {
        shakeTimeRemaining = duration;
    }
}
