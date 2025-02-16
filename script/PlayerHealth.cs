using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 40;
    private int currentHealth;
    public Slider healthSlider; // Ѫ����
    public GameManager gameManager; // ���� GameManager
    private Camera mainCamera;
    public Vector3 healthBarOffset = new Vector3(0, 0.3f, 0);
    private void Awake()
    {
        currentHealth = maxHealth;
        mainCamera = Camera.main;
        UpdateHealthUI();
    }
    private void Update()
    {
      
        if (healthSlider != null)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position + healthBarOffset);
            healthSlider.transform.position = screenPosition;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        Debug.Log($"����ܵ��� {damage} ���˺���ʣ��Ѫ����{currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("���������");
        gameManager.EndGame(false); // ������Ϸʧ���߼�
    }
}