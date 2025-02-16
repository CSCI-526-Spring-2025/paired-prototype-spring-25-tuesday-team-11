using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public int health = 20;
    public int damage = 5;
    public Vector3Int currentCell;
    public Tilemap tilemap;
    public Transform player;
    public EnemyHealth eHealth;

    private static readonly Vector3Int[] directions = new Vector3Int[]
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };

    private void Start()
    {
        currentCell = tilemap.WorldToCell(transform.position);
        transform.position = tilemap.GetCellCenterWorld(currentCell);
        EnemyManager.Instance.RegisterEnemy(this);
    }

    public void TakeAction()
    {
        if (Vector3Int.Distance(currentCell, tilemap.WorldToCell(player.position)) <= 1)
        {
            AttackPlayer();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }


    void MoveTowardsPlayer()
    {
        Vector3Int direction = tilemap.WorldToCell(player.position) - currentCell;
        direction.Clamp(Vector3Int.one * -1, Vector3Int.one); // 限制只能移动 1 格（上下左右）

        List<Vector3Int> possibleMoves = new List<Vector3Int>();

        
        if (IsCellWalkable(currentCell + direction))
        {
            possibleMoves.Add(direction);
        }


        foreach (var dir in directions)
        {
            Vector3Int nextCell = currentCell + dir;
            if (IsCellWalkable(nextCell))
            {
                possibleMoves.Add(dir);
            }
        }


        if (possibleMoves.Count > 0)
        {
            Vector3Int chosenDirection = possibleMoves[Random.Range(0, possibleMoves.Count)];
            currentCell += chosenDirection;
            transform.position = tilemap.GetCellCenterWorld(currentCell);
        }
    }

    bool IsCellWalkable(Vector3Int cell)
    {
        if (!tilemap.cellBounds.Contains(cell))
            return false;

        Vector3 worldPosition = tilemap.GetCellCenterWorld(cell);

        Collider2D hit = Physics2D.OverlapCircle(worldPosition, 0.2f, LayerMask.GetMask("Obstacle", "Player", "Enemy"));
        return hit == null; // 只有在没有障碍物、玩家或敌人的情况下才能行走
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        eHealth.TakeDamage(damage);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
