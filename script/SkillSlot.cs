using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SkillSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public TextMeshProUGUI skillText;
    private Vector3 originalPosition;
    private Transform originalParent;
    public Button skillButton;
    private float lastClickTime;
    private float doubleClickThreshold = 0.3f; // 双击时间间隔
    private playerController player;
    private bool isDraging = false;
    private void Awake()
    {
        if (skillText == null)
            skillText = GetComponentInChildren<TextMeshProUGUI>();

        originalPosition = transform.position;
        originalParent = transform.parent;
        player = FindObjectOfType<playerController>(); // 自动找到 `Player` 组件
        if (player != null)
        {
            skillButton.onClick.AddListener(() => ExecuteSkill());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // **双击清空技能栏**
            ClearSkill();
        }
        else
        {
            // **单击执行技能**
            ExecuteSkill();
        }

        lastClickTime = Time.time;
    }

    private void ExecuteSkill()
    {
        if (!isDraging && !string.IsNullOrEmpty(skillText.text))
        {
            player.UseSkill(skillText.text);
            skillText.text = "";  // **执行技能后清空技能栏**
        }
    }

    private void ClearSkill()
    {
        Debug.Log("技能已清空");
        skillText.text = "";  // **双击清空技能**
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(skillText.text))
        {
            isDraging = true;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(skillText.text))
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        SkillSlot firstSlot = null;
        SkillSlot secondSlot = null;
        TextMeshProUGUI firstTxt = null;
        TextMeshProUGUI secondTxt = null;
        bool res = false;
        // 遍历所有检测到的对象，找到 SkillSlot
        foreach (var result in raycastResults)
        {
            SkillSlot slot = result.gameObject.GetComponent<SkillSlot>();
           // TextMeshProUGUI t = result.gameObject.GetComponent<TextMeshProUGUI>();
            if (slot != null)
            {
                if (firstSlot == null)
                {
                    firstSlot = slot; // 记录第一个 Slot
                   // firstTxt = t;
                }
                else if (secondSlot == null)
                {
                    secondSlot = slot; // 记录第二个 Slot
                  //  secondTxt = t;
                    break; // 找到两个就退出循环
                }
            }
        }

        // 确保两个 SkillSlot 都被检测到
        if (firstSlot != null && secondSlot != null)
        {
            string text1 = firstSlot.skillText.text;
            string text2 = secondSlot.skillText.text;

            if (!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
            {
                res = player.performCombo(text1, text2);
            }
        }

        // 恢复原始位置
        if (res)
        {
            skillText.text = "";
            secondSlot.skillText.text = "";
        }
        transform.position = originalPosition;
        transform.SetParent(originalParent);
        isDraging = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //SkillSlot droppedSlot = eventData.pointerDrag.GetComponent<SkillSlot>();

        //if (droppedSlot != null && droppedSlot != this && !string.IsNullOrEmpty(droppedSlot.skillText.text))
        //{
        //    Debug.Log($"触发组合技能: {droppedSlot.skillText.text} + {skillText.text}");
        //    // **这里可以加入触发新技能的逻辑**
        //}
    }
}