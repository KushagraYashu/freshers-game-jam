using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelAnnoyanceKeyDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform keyRectTransform;
    public RectTransform parent;
    public Canvas parentCanvas;
    public LevelAnnoyanceKeyHole levelAnnoyanceKeyHole;
    public Image keyHoleImage;
    public Sprite unlocked;
    private Vector2 originalPosition;
    private bool isDragging = false;

    void Awake()
    {
        keyRectTransform = GetComponent<RectTransform>();
        originalPosition = keyRectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parent.GetComponent<RectTransform>(),
                eventData.position,
                parentCanvas.worldCamera,
                out Vector2 localPoint
            );
            keyRectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (levelAnnoyanceKeyHole != null && levelAnnoyanceKeyHole.IsKeyInHole(keyRectTransform)) {
            keyRectTransform.anchoredPosition = levelAnnoyanceKeyHole.GetComponent<RectTransform>().anchoredPosition;
            keyHoleImage.sprite = unlocked;
            LevelAnnoyanceKey.instance.UnLocked();
        }
        else
        {
            keyRectTransform.anchoredPosition = originalPosition;
        }
    }
}