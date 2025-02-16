using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health = 15;
    public GameObject dropItemPrefab; // 掉落的道具

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"障碍物受到了 {damage} 点伤害，剩余血量：{health}");

        if (health <= 0)
        {
            Destroy(gameObject);
            DropItem();
        }
    }

    void DropItem()
    {
        if (dropItemPrefab != null)
        {
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        }
    }
}