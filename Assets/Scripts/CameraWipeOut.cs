using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWipeOut : MonoBehaviour
{
    public GameObject spriteMaskPrefab;
    public Transform steam;
    bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        mousePos.z = 1;
        RaycastHit hit;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isPressed = true;
        }else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            isPressed = false;
        }

        if (Physics.Raycast(ray, out hit) && isPressed)
        {
            Debug.Log(hit.transform.name);
            var mask = Instantiate(spriteMaskPrefab, hit.point, Quaternion.identity);
            mask.transform.SetParent(steam, true);
            mask.transform.rotation = steam.rotation;
        }
    }
}
