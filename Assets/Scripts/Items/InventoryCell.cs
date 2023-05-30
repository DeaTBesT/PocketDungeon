using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IDropHandler, IBeginDragHandler
{
    #region [PrivateVars]

    private InventoryItem currentItem;
    private RectTransform rectTransform;

    #endregion

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        eventData.pointerDrag.TryGetComponent(out InventoryItem item);
        currentItem = item;

        eventData.pointerDrag.TryGetComponent(out RectTransform pointerTransform);
        pointerTransform.anchoredPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentItem = null;
    }
}
