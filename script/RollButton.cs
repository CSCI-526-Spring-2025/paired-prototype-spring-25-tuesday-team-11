using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RollButton : MonoBehaviour
{
    public Button rollButton;
    public TextMeshProUGUI numberText;  // 显示随机选择的物品名称
    public Transform skillBarContent;  // 技能栏 UI
    public GameObject skillPrefab;  // 技能按钮预制体
    public List<InventorySlot> inventorySlots; // 6 个格子
    public List<TextMeshProUGUI> skillSlots;
    private void Start()
    {
        rollButton.onClick.AddListener(RollForSkill);
    }

    void RollForSkill()
    {
        List<InventorySlot> filledSlots = new List<InventorySlot>();

        // 1️⃣ 找到所有 **非空的格子**
        foreach (var slot in inventorySlots)
        {
            if (!string.IsNullOrEmpty(slot.storedItemName))
            {
                filledSlots.Add(slot);
            }
        }

        // 2️⃣ 确保有至少一个物品可用
        if (filledSlots.Count == 0)
        {
            return;
        }

        // 3️⃣ 随机选择一个格子
        int randomIndex = Random.Range(0, filledSlots.Count);
        InventorySlot selectedSlot = filledSlots[randomIndex];

        // 4️⃣ 显示选中的物品名称到 `Number` UI
        numberText.text = selectedSlot.storedItemName;

        // 5️⃣ 把物品添加到技能栏
        AddToSkillBar(selectedSlot.storedItemName);
    }

    void AddToSkillBar(string skillName)
    {
        for (int i = 0; i < skillSlots.Count; i++)
        {
            if (string.IsNullOrEmpty(skillSlots[i].text)) // 找到第一个空的技能栏
            {
                skillSlots[i].text = skillName;
                return;
            }
        }
    }

    void UseSkill(string skillName)
    {
        Debug.Log($"sill: {skillName}");
        // 这里可以加入角色执行技能的逻辑
    }
}