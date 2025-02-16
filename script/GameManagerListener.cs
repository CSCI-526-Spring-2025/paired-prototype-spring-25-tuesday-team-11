using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerListener : MonoBehaviour
{
    public static GameManagerListener Instance;

    public TextMeshProUGUI rollCounterText; // Roll 次数显示组件
    public Button rollButton; // Roll 按钮

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rollButton.onClick.AddListener(RollTheDice);
        UpdateRollCounter(GameManager.Instance.rollCount);
    }
    public void RollTheDice()
    {
        if (GameManager.Instance.TryRoll())
        {
            Debug.Log("Roll 成功！");
            // 这里可以添加执行 Roll 逻辑（如随机技能、更新UI等）
        }
        else
        {
            Debug.Log("没有剩余 Roll 次数！");
        }
        UpdateRollCounter(GameManager.Instance.rollCount);
    }

    // 更新 Roll 次数 UI
    public void UpdateRollCounter(int rollsLeft)
    {
        rollCounterText.text = $"Rolls Left: {rollsLeft}";
        rollButton.interactable = rollsLeft > 0; // 无次数禁用按钮
    }
}