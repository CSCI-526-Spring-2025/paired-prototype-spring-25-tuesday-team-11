using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SkillSelectionPanel : MonoBehaviour
{
    public GameObject skillButtonPrefab; // 技能按钮预制体
    public Transform skillButtonContainer; // 技能按钮容器
    //public Button closeButton; // 关闭面板按钮
    public SpecialSkillSlot specialSkillSlot; // 引用特殊技能槽

    private List<string> availableSkills = new List<string> { "left", "right", "up","down" };

    private void Start()
    {
        GameManager.Instance.OnTurnStart += PopulateSkills;
        PopulateSkills();
        //closeButton.onClick.AddListener(ClosePanel);
    }

    // ✅ 动态生成技能按钮
    private void PopulateSkills()
    {
        foreach (Transform child in skillButtonContainer)
        {
            Destroy(child.gameObject);
        }
        foreach (var skill in availableSkills)
        {
            GameObject buttonObj = Instantiate(skillButtonPrefab, skillButtonContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = skill;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => SelectSkill(skill));
        }
    }

    // ✅ 选择技能
    private void SelectSkill(string skillName)
    {
        Debug.Log($"已选择技能: {skillName}");
        specialSkillSlot.OnSkillSelected(); // 通知技能槽已选择
        ClosePanel();
    }

    // ✅ 关闭技能面板
    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
