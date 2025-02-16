using UnityEngine;
using UnityEngine.UI;

public class SpecialSkillSlot : MonoBehaviour
{
    public GameObject skillSelectionPanel; // 技能选择面板
    public Button specialSkillButton; // 特殊技能按钮

    private bool isUsedThisTurn = false;
    private bool hasOpenedPanelThisTurn = false; // 是否本回合已打开面板

    private void Start()
    {
        specialSkillButton.onClick.AddListener(OnSpecialSkillClicked);
        GameManager.Instance.OnTurnStart += ResetSpecialSkill; // 监听回合开始
        CloseSkillPanel();
    }

    // ✅ 点击特殊技能按钮
    private void OnSpecialSkillClicked()
    {
        if (!isUsedThisTurn && !hasOpenedPanelThisTurn)
        {
            OpenSkillPanel();
            hasOpenedPanelThisTurn = true; // 标记已使用面板
        }
        else
        {
            Debug.Log("该技能每回合只能使用一次！");
        }
    }

    // ✅ 由面板调用此方法，表示已选择技能
    public void OnSkillSelected()
    {
        isUsedThisTurn = true;
        specialSkillButton.interactable = false; // 禁用技能格子
        CloseSkillPanel();
    }

    // ✅ 回合开始时重置状态
    private void ResetSpecialSkill()
    {
        isUsedThisTurn = false;
        hasOpenedPanelThisTurn = false;
        specialSkillButton.interactable = true; // 恢复可用状态
    }

    // ✅ 打开技能面板
    public void OpenSkillPanel()
    {
        skillSelectionPanel.SetActive(true);
    }

    // ✅ 关闭技能面板
    public void CloseSkillPanel()
    {
        skillSelectionPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnTurnStart -= ResetSpecialSkill; // 移除监听
    }
}