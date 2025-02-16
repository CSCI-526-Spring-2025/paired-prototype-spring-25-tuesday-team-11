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
        itemText = GetComponentInChildren<TextMeshProUGUI>();  // ��ȡ��Ʒ�ı�
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
                Destroy(gameObject);  // ��Ʒ��������Ӻ�ɾ��ԭʼ `Scroll View` �����Ʒ
            }
        }
        else if(hoveredObject != null && hoveredObject.GetComponentInParent<InventorySlot>())
        {
            InventorySlot slot = hoveredObject.GetComponentInParent<InventorySlot>();
            if (slot.storedItemName == "")
            {
                slot.StoreItem(itemName);
                Destroy(gameObject);  // ��Ʒ��������Ӻ�ɾ��ԭʼ `Scroll View` �����Ʒ
            }
        }
        else
        {
            transform.SetParent(parentAfterDrag);  // ���û�з�����ӣ��ص�ԭ����λ��
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}