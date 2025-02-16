using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    public Slider healthSlider; // 血量条
    public Vector3 healthBarOffset = new Vector3(0, 0.5f, 0); // 血条偏移

    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        currentHealth = maxHealth;
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
        Debug.Log($"敌人受到了 {damage} 点伤害，剩余血量：{currentHealth}");

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
        Debug.Log("敌人死亡！");
        Destroy(gameObject);
    }
}