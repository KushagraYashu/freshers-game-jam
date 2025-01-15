using UnityEngine;

public class LevelAnnoyanceKeyHole : MonoBehaviour
{
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool IsKeyInHole(RectTransform keyRect)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            keyRect.position
        );
    }
}