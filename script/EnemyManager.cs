using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void ProcessEnemyTurn()
    {
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        foreach (Enemy enemy in enemies)
        {
            yield return new WaitForSeconds(0.5f);
            enemy.TakeAction();
        }

        GameManager.Instance.OnEnemyTurnEnd();
        GameManager.Instance.EndTurn();
    }
}