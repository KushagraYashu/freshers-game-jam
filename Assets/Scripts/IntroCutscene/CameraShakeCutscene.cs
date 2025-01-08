using UnityEngine;

namespace Cutscene
{
    public class CameraShakeCutscene : MonoBehaviour
    {
        public float startShakeTime = 28f; // Time in seconds when the shaking starts
        public float maxDuration = 10f;   // Duration for the shake to intensify
        public float maxMagnitude = 1f;  // Maximum shake intensity

        private Vector3 originalPosition;
        private float shakeTimer = 0f;
        private bool isShaking = false;

        void Start()
        {
            originalPosition = transform.localPosition;
        }

        void Update()
        {
            // Start the shake timer after the specified time
            if (Time.time >= startShakeTime && !isShaking)
            {
                isShaking = true;
            }

            // Perform the shake if triggered
            if (isShaking)
            {
                ShakeCamera();
            }
        }

        private void ShakeCamera()
        {
            // Gradually increase the magnitude over time
            shakeTimer += Time.deltaTime;
            float currentMagnitude = Mathf.Lerp(0, maxMagnitude, shakeTimer / maxDuration);

            // Shake the camera
            transform.localPosition = originalPosition + Random.insideUnitSphere * currentMagnitude;

            // Stop shaking once max duration is reached
            if (shakeTimer >= maxDuration)
            {
                isShaking = false;
                transform.localPosition = originalPosition;
                shakeTimer = 0f;
            }
        }
    }
}
