using UnityEngine;

public class AudioStart : MonoBehaviour
{
    public AudioSource audioSource;   // Reference to the AudioSource component
    public AudioClip audioClip;       // Reference to the audio clip you want to play
    public float startTime = 32f;     // Time (in seconds) after which the audio will start playing

    private float timer = 0f;         // Timer to track the time
    private bool hasPlayed = false;   // Flag to check if the audio has already played

    void Start()
    {
        // Ensure the audio source is set
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        // Ensure the audio clip is set
        if (audioClip == null)
        {
            Debug.LogError("AudioClip is not assigned!");
        }
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has reached the start time and the audio hasn't already played
        if (timer >= startTime && !hasPlayed)
        {
            audioSource.clip = audioClip;   // Assign the audio clip if not already set
            audioSource.Play();             // Start playing the audio
            hasPlayed = true;               // Mark the audio as played
        }
    }
}
