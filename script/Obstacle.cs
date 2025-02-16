using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int health = 15;
    public GameObject dropItemPrefab; // ����ĵ���

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"�ϰ����ܵ��� {damage} ���˺���ʣ��Ѫ����{health}");

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