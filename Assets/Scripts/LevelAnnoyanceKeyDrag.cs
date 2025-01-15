using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelAnnoyanceKeyDrag : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public LevelAnnoyanceKeyDrag instance;

    private RectTransform keyRectTransform;
    public RectTransform parent;
    public Canvas parentCanvas;
    public LevelAnnoyanceKeyHole levelAnnoyanceKeyHole;
    private Vector2 originalPosition;
    private bool isDragging = false;

    void Awake()
    {
        instance = this;
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
            LevelAnnoyanceKey.instance.UnLocked();
        }
        else
        {
            keyRectTransform.anchoredPosition = originalPosition;
        }
    }
}