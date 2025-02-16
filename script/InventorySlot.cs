using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string storedItemName;  // 存储物品名称
    public Button slotButton;
    public TextMeshProUGUI slotText;  // 显示当前格子存放的物品名称
    private void Awake()
    {
        if (slotButton == null)
            slotButton = GetComponent<Button>();  // 自动获取 Button

        if (slotText == null)
            slotText = GetComponentInChildren<TextMeshProUGUI>();  // 自动获取 Text
    }
    private void Start()
    {
        slotButton.onClick.AddListener(ReturnItemToBackpack);
        slotText.text = "";  // 初始状态为空
    }

    public void StoreItem(string itemName)
    {
        if (storedItemName == "")
        {
            storedItemName = itemName;
            slotText.text = itemName;
        }
    }

    void ReturnItemToBackpack()
    {
        if (!string.IsNullOrEmpty(storedItemName))
        {
            InventoryManager.Instance.AddItem(storedItemName);
            storedItemName = "";
            slotText.text = "";  // 清空格子
        }
    }
}