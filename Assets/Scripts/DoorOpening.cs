using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpening : MonoBehaviour
{
    public AudioSource openDoors;
    public AudioSource closeDoors;

    private bool doorsOpen = false;
    private bool doorsClose = false;

    [SerializeField] private Animator animatorL = null;
    [SerializeField] private Animator animatorR = null;

    [SerializeField] private Transform cameraTransform; // Reference to the camera's Transform
    public float shakeDuration = 0.3f; // Duration of the camera shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake

    private Vector3 originalCameraPosition; // To store the camera's original position

    public Ltestscript ltestscript;

    void Start()
    {
        if (cameraTransform != null)
        {
            originalCameraPosition = cameraTransform.localPosition;
        }
    }

    void Update()
    {
        if (ltestscript.tScene == true)
        {
            // Scene transition logic (if needed)
        }
    }

    public void OpenDoor()
    {
        openDoors.Play();
        animatorL.SetBool("test", true);
        animatorL.ResetTrigger("doorsClose");
        animatorR.ResetTrigger("doorsClose");
        animatorL.SetTrigger("doorsOpen");
        animatorR.SetTrigger("doorsOpen");
        doorsOpen = true;
        doorsClose = false;
    }

    public void CloseDoor()
    {
        closeDoors.Play();
        Debug.Log("ClosingDoors");

        animatorL.SetBool("test", false);
        animatorL.SetTrigger("doorsClose");
        animatorR.SetTrigger("doorsClose");
        animatorL.ResetTrigger("doorsOpen");
        animatorR.ResetTrigger("doorsOpen");
        doorsClose = true;
        doorsOpen = false;

        // Trigger camera shake
        if (cameraTransform != null)
        {
            StartCoroutine(ShakeCamera());
        }
    }

    private IEnumerator ShakeCamera()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Apply random shake within the specified magnitude
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = originalCameraPosition + new Vector3(randomOffset.x, randomOffset.y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset camera to its original position
        cameraTransform.localPosition = originalCameraPosition;
    }
}
