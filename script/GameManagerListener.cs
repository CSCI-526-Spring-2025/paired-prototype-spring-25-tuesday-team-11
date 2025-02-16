using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerListener : MonoBehaviour
{
    public static GameManagerListener Instance;

    public TextMeshProUGUI rollCounterText; // Roll ������ʾ���
    public Button rollButton; // Roll ��ť

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
            Debug.Log("Roll �ɹ���");
            // ����������ִ�� Roll �߼�����������ܡ�����UI�ȣ�
        }
        else
        {
            Debug.Log("û��ʣ�� Roll ������");
        }
        UpdateRollCounter(GameManager.Instance.rollCount);
    }

    // ���� Roll ���� UI
    public void UpdateRollCounter(int rollsLeft)
    {
        rollCounterText.text = $"Rolls Left: {rollsLeft}";
        rollButton.interactable = rollsLeft > 0; // �޴������ð�ť
    }
}