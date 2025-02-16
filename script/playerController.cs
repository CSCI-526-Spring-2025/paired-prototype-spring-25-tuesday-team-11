using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.EventSystems.EventTrigger;

public class playerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Tilemap tilemap;            // 拖进 Inspector
    public Vector3Int currentCellPos;  // 当前所在的 Tilemap 网格坐标
    public Vector3Int lastDirection=Vector3Int.right;
    public GameObject projectilePrefab;
    public GameObject attackPre;
    private Quaternion lastRotation = Quaternion.Euler(0, 0, 0);
    private void Start()
    {
        // 获取角色当前所在的 Tilemap 单元格，并对齐到格子中心
        currentCellPos = tilemap.WorldToCell(transform.position);
        transform.position = tilemap.GetCellCenterWorld(currentCellPos);
    }

    public void MoveUp()
    {
        MoveByOffset(Vector3Int.up);
        transform.rotation = Quaternion.Euler(0, 0, 90);
        lastRotation = transform.rotation;
        lastDirection = Vector3Int.up; // 更新朝向
        
    }

    public void MoveDown()
    {
        MoveByOffset(Vector3Int.down);
        transform.rotation = Quaternion.Euler(0, 0, 270);
        lastRotation = transform.rotation;
        lastDirection = Vector3Int.down; // 更新朝向
        
    }

    public void MoveLeft()
    {
        MoveByOffset(Vector3Int.left);
        transform.rotation = Quaternion.Euler(0, 0,180);
        lastRotation = transform.rotation;
        lastDirection = Vector3Int.left; // 更新朝向
        
    }

    public void MoveRight()
    {
        MoveByOffset(Vector3Int.right);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        lastRotation = transform.rotation;
        lastDirection = Vector3Int.right; // 更新朝向
        
    }

    private void MoveByOffset(Vector3Int offset)
    {
        Vector3Int targetCell = currentCellPos + offset;
        if (IsCellWalkable(targetCell))
        {
            currentCellPos = targetCell;
            // 网格坐标 -> 世界坐标
            transform.position = tilemap.GetCellCenterWorld(currentCellPos);
        }
        else
        {
            Debug.LogWarning("目标不可走");
        }
    }

    private bool IsCellWalkable(Vector3Int cell)
    {
        if (!tilemap.cellBounds.Contains(cell))
        {
            return false;
        }

        Vector3 worldPosition = tilemap.GetCellCenterWorld(cell);
        Collider2D hit = Physics2D.OverlapBox(worldPosition, new Vector2(0.8f, 0.8f), 0f,
                                              LayerMask.GetMask("Obstacle", "Enemy", "Exit"));

        if (hit != null)
        {
            if (hit.gameObject.CompareTag("Exit"))
            {
                Debug.Log("玩家到达出口，游戏胜利！");
                GameManager.Instance.EndGame(true); // 游戏胜利
                return false;
            }
            return false;
        }

        var tile = tilemap.GetTile(cell);
        return tile != null;
    }
    public void UseSkill(string skillName)
    {
        switch (skillName.ToLower())
        {
            case "left":
                
                MoveLeft();
                break;
            case "right":
                
                MoveRight();
                break;
            case "up":
                
                MoveUp();
                break;
            case "down":
                
                MoveDown();
                break;
            case "attack":
                //ShootProjectile(5);
                MeleeAttack(lastDirection);
                break;
            case "combo":
                
               // PerformCombo();
                break;
            default:
                
                break;
        }

    }

    private void move(Vector3Int d)
    {
        if(d == Vector3Int.right)
        {
            MoveRight();
        }
        else if(d == Vector3Int.left)
        {
            MoveLeft();
        }
        else if (d == Vector3Int.up)
        {
            MoveUp();
        }
        else if(d == Vector3Int.down)
        {
            MoveDown();
        }
    }

    static bool IsPureInteger(string str)
    {
        return int.TryParse(str, out _);
    }
    public bool performCombo(string s1, string s2)
    {
        int dis = -1;
        bool atc = false;
        Vector3Int dir = new Vector3Int();
        if (IsPureInteger(s1))
        {
            dis = int.Parse(s1);
        }
        if (IsPureInteger(s2))
        {
            dis = int.Parse(s2);
        }
            switch (s1.ToLower())
        {
            case "left":

                dir = Vector3Int.left;
                break;
            case "right":

                dir = Vector3Int.right;
                break;
            case "up":

                dir = Vector3Int.up;
                break;
            case "down":

                dir = Vector3Int.down;
                break;
            case "attack":
                atc = true;
                break;
            default:

                break;
        }
        switch (s2.ToLower())
        {
            case "left":

                dir = Vector3Int.left;
                break;
            case "right":

                dir = Vector3Int.right;
                break;
            case "up":

                dir = Vector3Int.up;
                break;
            case "down":

                dir = Vector3Int.down;
                break;
            case "attack":
                atc = true;
                break;
            default:

                break;
        }
        if(atc == true && dis > 0)
        {
            ShootProjectile(dis);
            return true;
        }
        else if(atc==true && dir != Vector3Int.zero)
        {
            MeleeAttack(dir);
            return true;
        }
        else if(dir !=Vector3Int.zero && dis > 0)
        {
            for(int i = 0; i < dis; i++)
            {
                move(dir);
            }
            return true;
        }
        return false;

    }


    void MeleeAttack(Vector3Int dir)
    {
    

  
            Vector3Int attackCell = currentCellPos + dir;
            Vector3 attackWorldPosition = tilemap.GetCellCenterWorld(attackCell);
        Vector3 tempo = new Vector3(lastDirection.x, lastDirection.y, lastDirection.z) * (float)0.4f;
        GameObject sword = Instantiate(
   attackPre,
   this.transform.position+tempo ,
   this.transform.rotation // 随机旋转角度
);
        Destroy(sword, 0.5f);
        Collider2D hit = Physics2D.OverlapCircle(attackWorldPosition, 0.2f, LayerMask.GetMask("Enemy", "Obstacle"));

            if (hit)
            {

            if (hit.CompareTag("Enemy"))
                {
                    Enemy enemy = hit.GetComponent<Enemy>();
                    if (enemy)
                    {
                        enemy.TakeDamage(10);
                        Debug.Log("近战攻击命中敌人！");
                    }
                }
                else if (hit.CompareTag("Obstacle"))
                {
                    Obstacle obstacle = hit.GetComponent<Obstacle>();
                    if (obstacle)
                    {
                        obstacle.TakeDamage(10);
                        Debug.Log("近战攻击命中障碍物！");
                    }
                }
            }
        
    }

    void ShootProjectile(int range)
    {
        int projectileRange = range; // 这里可以改成不同技能的射程
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(lastDirection, tilemap, projectileRange);
        //GameManager.Instance.EndTurn();
    }

}
