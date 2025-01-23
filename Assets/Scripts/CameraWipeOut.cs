using System.Collections.Generic;
using UnityEngine;

public class CameraWipeOut : MonoBehaviour
{
    public GameObject spriteMaskPrefab; // Prefab for the SpriteMask
    public Transform steam; // The parent steam object
    public float clearThreshold = 0.95f; // Percentage of steam to be cleared for completion

    private List<GameObject> masks = new List<GameObject>(); // Track all spawned masks

    public float ratio;
    float steamArea;
    float clearedArea = 0f;

    public bool wiped = false;
    bool isPressed;

    void OnEnable()
    {
        // Initialize the bounds of the steam object
        var steamSprite = steam.GetComponent<SpriteRenderer>();
        if (steamSprite != null)
        {
            var size = steamSprite.bounds.size;
            var center = steamSprite.bounds.center;
            steamArea = steam.GetComponent<BoxCollider>().bounds.size.y * steam.GetComponent<BoxCollider>().bounds.size.y;
        }
    }

    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            isPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            isPressed = false;
        }

        if (Physics.Raycast(ray, out hit) && isPressed)
        {
            if (hit.transform.CompareTag("CameraOverlay"))
            {
                var mask = Instantiate(spriteMaskPrefab, hit.point, Quaternion.identity);
                mask.transform.SetParent(steam, true);
                mask.transform.localPosition = new Vector3(mask.transform.localPosition.x, mask.transform.localPosition.y, -0.2f);
                mask.transform.rotation = steam.rotation;

                masks.Add(mask);

                if (CheckCleared())
                {
                    wiped = true;
                    foreach(var maskItem in masks)
                    {
                        maskItem.GetComponent<BoxCollider>().enabled = false;
                        Destroy(maskItem, 3);
                    }
                }
            }
        }
    }

    bool CheckCleared()
    {
        clearedArea = 0;

        // Calculate cleared area
        foreach (var mask in masks)
        {
            if (mask != null)
            {
                if (mask.TryGetComponent<BoxCollider>(out var maskCollider))
                {
                    // Approximate the area of the mask
                    clearedArea += maskCollider.bounds.size.x * maskCollider.bounds.size.y;
                }
            }
        }

        ratio = clearedArea / steamArea;

        // Check if the cleared area meets the threshold
        return (clearedArea / steamArea) >= clearThreshold;
    }
}
