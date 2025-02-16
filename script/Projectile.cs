using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float moveSpeed = 0.2f; // 每次移动到下一个 Tilemap 位置的时间
    private int maxTilesToTravel;
    private Vector3Int direction;
    private Vector3Int currentCell;
    private Tilemap tilemap;

    public void Initialize(Vector3Int dir, Tilemap tilemap, int maxDistance)
    {
        this.direction = dir;
        this.tilemap = tilemap;
        this.maxTilesToTravel = maxDistance;
        currentCell = tilemap.WorldToCell(transform.position);
        StartCoroutine(MoveProjectile());
    }

    private IEnumerator MoveProjectile()
    {
        for (int i = 0; i < maxTilesToTravel; i++)
        {
            yield return new WaitForSeconds(moveSpeed);

            Vector3Int nextCell = currentCell + direction;
            Vector3 nextPosition = tilemap.GetCellCenterWorld(nextCell);

            // 检查是否撞到了障碍物或敌人
            Collider2D hit = Physics2D.OverlapCircle(nextPosition, 0.6f, LayerMask.GetMask("Enemy", "Obstacle"));
            Debug.Log($"检测攻击格子: {nextPosition}, 检测到对象: {hit?.gameObject.name}");
            if (hit)
            {
                if (hit.CompareTag("Enemy"))
                {
                    hit.GetComponent<Enemy>().TakeDamage(damage);
                }
                else if (hit.CompareTag("Obstacle"))
                {
                    hit.GetComponent<Obstacle>().TakeDamage(damage);
                }

                Destroy(gameObject);
                yield break; // 终止协程
            }

            // 更新位置
            currentCell = nextCell;
            transform.position = nextPosition;
        }

        // 飞行完成后销毁子弹
        Destroy(gameObject);
    }
}