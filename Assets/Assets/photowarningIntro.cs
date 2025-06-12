using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class photowarningIntro : MonoBehaviour
{
    public RawImage warningImage; // Assign in Inspector
    public float fadeDuration = 2f; // Duration of fade in seconds

    private float timer = 0f;
    private bool fading = false;

    void Start()
    {
        if (warningImage != null)
        {
            warningImage.enabled = true;
            Color c = warningImage.color;
            c.a = 1f;
            warningImage.color = c;
            fading = true;
        }
    }

    void Update()
    {
        if (fading && warningImage != null)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            Color c = warningImage.color;
            c.a = alpha;
            warningImage.color = c;

            if (timer >= fadeDuration)
            {
                warningImage.enabled = false;
                fading = false;
            }
        }
    }
}