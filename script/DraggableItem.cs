using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentAfterDrag;
    private CanvasGroup canvasGroup;
    private string itemName;
    private TextMeshProUGUI itemText;

    private void Awake()
    {


        canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)  
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        itemText = GetComponentInChildren<TextMeshProUGUI>();  // 获取物品文本
        itemName = itemText.text;
    }

    public void SetItem(string name)
    {
        itemName = name;
        itemText.text = name;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject hoveredObject = eventData.pointerCurrentRaycast.gameObject;
        if (hoveredObject != null && hoveredObject.GetComponent<InventorySlot>())
        {
            InventorySlot slot = hoveredObject.GetComponent<InventorySlot>();
            if (slot.storedItemName == "")
            {
                slot.StoreItem(itemName);
                Destroy(gameObject);  // 物品被放入格子后删除原始 `Scroll View` 里的物品
            }
        }
        else if(hoveredObject != null && hoveredObject.GetComponentInParent<InventorySlot>())
        {
            InventorySlot slot = hoveredObject.GetComponentInParent<InventorySlot>();
            if (slot.storedItemName == "")
            {
                slot.StoreItem(itemName);
                Destroy(gameObject);  // 物品被放入格子后删除原始 `Scroll View` 里的物品
            }
        }
        else
        {
            transform.SetParent(parentAfterDrag);  // 如果没有放入格子，回到原来的位置
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}