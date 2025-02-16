using UnityEngine;
using UnityEngine.UI;
public class BackpackUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject backpackPanel;  // 背包 UI 面板
    public Button bagButton;          // 打开背包按钮

    private void Start()
    {
        bagButton.onClick.AddListener(ToggleBackpack);
        backpackPanel.SetActive(false); // 默认隐藏背包
    }

    void ToggleBackpack()
    {
        backpackPanel.SetActive(!backpackPanel.activeSelf);
    }


}
