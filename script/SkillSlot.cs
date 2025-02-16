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
    private float doubleClickThreshold = 0.3f; // ˫��ʱ����
    private playerController player;
    private bool isDraging = false;
    private void Awake()
    {
        if (skillText == null)
            skillText = GetComponentInChildren<TextMeshProUGUI>();

        originalPosition = transform.position;
        originalParent = transform.parent;
        player = FindObjectOfType<playerController>(); // �Զ��ҵ� `Player` ���
        if (player != null)
        {
            skillButton.onClick.AddListener(() => ExecuteSkill());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // **˫����ռ�����**
            ClearSkill();
        }
        else
        {
            // **����ִ�м���**
            ExecuteSkill();
        }

        lastClickTime = Time.time;
    }

    private void ExecuteSkill()
    {
        if (!isDraging && !string.IsNullOrEmpty(skillText.text))
        {
            player.UseSkill(skillText.text);
            skillText.text = "";  // **ִ�м��ܺ���ռ�����**
        }
    }

    private void ClearSkill()
    {
        Debug.Log("���������");
        skillText.text = "";  // **˫����ռ���**
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
        // �������м�⵽�Ķ����ҵ� SkillSlot
        foreach (var result in raycastResults)
        {
            SkillSlot slot = result.gameObject.GetComponent<SkillSlot>();
           // TextMeshProUGUI t = result.gameObject.GetComponent<TextMeshProUGUI>();
            if (slot != null)
            {
                if (firstSlot == null)
                {
                    firstSlot = slot; // ��¼��һ�� Slot
                   // firstTxt = t;
                }
                else if (secondSlot == null)
                {
                    secondSlot = slot; // ��¼�ڶ��� Slot
                  //  secondTxt = t;
                    break; // �ҵ��������˳�ѭ��
                }
            }
        }

        // ȷ������ SkillSlot ������⵽
        if (firstSlot != null && secondSlot != null)
        {
            string text1 = firstSlot.skillText.text;
            string text2 = secondSlot.skillText.text;

            if (!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
            {
                res = player.performCombo(text1, text2);
            }
        }

        // �ָ�ԭʼλ��
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
        //    Debug.Log($"������ϼ���: {droppedSlot.skillText.text} + {skillText.text}");
        //    // **������Լ��봥���¼��ܵ��߼�**
        //}
    }
}