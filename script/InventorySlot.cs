using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public string storedItemName;  // �洢��Ʒ����
    public Button slotButton;
    public TextMeshProUGUI slotText;  // ��ʾ��ǰ���Ӵ�ŵ���Ʒ����
    private void Awake()
    {
        if (slotButton == null)
            slotButton = GetComponent<Button>();  // �Զ���ȡ Button

        if (slotText == null)
            slotText = GetComponentInChildren<TextMeshProUGUI>();  // �Զ���ȡ Text
    }
    private void Start()
    {
        slotButton.onClick.AddListener(ReturnItemToBackpack);
        slotText.text = "";  // ��ʼ״̬Ϊ��
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
            slotText.text = "";  // ��ո���
        }
    }
}