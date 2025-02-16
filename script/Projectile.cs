using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public float moveSpeed = 0.2f; // ÿ���ƶ�����һ�� Tilemap λ�õ�ʱ��
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

            // ����Ƿ�ײ�����ϰ�������
            Collider2D hit = Physics2D.OverlapCircle(nextPosition, 0.6f, LayerMask.GetMask("Enemy", "Obstacle"));
            Debug.Log($"��⹥������: {nextPosition}, ��⵽����: {hit?.gameObject.name}");
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
                yield break; // ��ֹЭ��
            }

            // ����λ��
            currentCell = nextCell;
            transform.position = nextPosition;
        }

        // ������ɺ������ӵ�
        Destroy(gameObject);
    }
}