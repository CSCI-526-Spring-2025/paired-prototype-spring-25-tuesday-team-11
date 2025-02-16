using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 40;
    private int currentHealth;
    public Slider healthSlider; // 血量条
    public GameManager gameManager; // 引用 GameManager
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
        Debug.Log($"玩家受到了 {damage} 点伤害，剩余血量：{currentHealth}");

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
        Debug.Log("玩家死亡！");
        gameManager.EndGame(false); // 调用游戏失败逻辑
    }
}