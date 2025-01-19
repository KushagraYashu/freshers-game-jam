using UnityEngine;
using TMPro;

public class WarningTextFade : MonoBehaviour
{
    TextMeshProUGUI textMesh; // Assign your TextMeshPro component here
    public float fadeSpeed = 2f; // Controls the speed of the fade

    void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (textMesh != null)
        {
            // Calculate the alpha value using a sine wave
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * fadeSpeed));

            // Apply the alpha value to the text's color
            Color color = textMesh.color;
            color.a = alpha;
            textMesh.color = color;
        }
    }
}
