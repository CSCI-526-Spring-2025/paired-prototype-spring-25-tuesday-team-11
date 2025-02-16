using UnityEngine;
using UnityEngine.UI;
public class BackpackUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject backpackPanel;  // ���� UI ���
    public Button bagButton;          // �򿪱�����ť

    private void Start()
    {
        bagButton.onClick.AddListener(ToggleBackpack);
        backpackPanel.SetActive(false); // Ĭ�����ر���
    }

    void ToggleBackpack()
    {
        backpackPanel.SetActive(!backpackPanel.activeSelf);
    }


}
