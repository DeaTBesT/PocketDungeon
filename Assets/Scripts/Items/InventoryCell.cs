using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IDropHandler
{
    #region [PrivateVars]

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

        eventData.pointerDrag.TryGetComponent(out RectTransform pointerTransform);
        pointerTransform.anchoredPosition = rectTransform.anchoredPosition;
    }
}
