using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnTurnStart;
    public bool isPlayerTurn = true; // true = 玩家回合, false = 敌人回合
    public int rollCount = 3; // 每回合剩余 Roll 次数
    public int maxRollsPerTurn = 3;
    public GameOverUIController gameOverUIController;
    private void Awake()
    {
        Instance = this;
    }
    public bool TryRoll()
    {
        if (rollCount > 0 && isPlayerTurn)
        {
            rollCount--;
            UpdateRollUI();
            return true;
        }
        return false;
    }
    public void StartNewTurn()
    {
        OnTurnStart?.Invoke(); // 通知所有监听者回合开始
    }
    // 重置回合时调用
    public void ResetRolls()
    {
        rollCount = maxRollsPerTurn;
        UpdateRollUI();
    }

    // 更新 Roll 次数 UI
    public void UpdateRollUI()
    {
        if (GameManagerListener.Instance != null)
        {
            GameManagerListener.Instance.UpdateRollCounter(rollCount);
        }
    }
    public void EndGame(bool isWin)
    {
        if (isWin)
        {
            Debug.Log(" 游戏胜利！");
            gameOverUIController.ShowYouWinUI(); // 显示 YOU WIN 界面
        }
        else
        {
            Debug.Log(" 游戏失败！");
            gameOverUIController.ShowYouWinUI();
        }

        Time.timeScale = 0f; // 暂停游戏
    }
    public void EndTurn()
    {
        StartNewTurn();
        Debug.Log("【DEBUG】EndTurn() 被调用");
        isPlayerTurn = !isPlayerTurn;

        if (isPlayerTurn)
        {
            Debug.Log("玩家回合");
        }
        else
        {
            Debug.Log("敌人回合");
            EnemyManager.Instance.ProcessEnemyTurn();
        }
    }
    public void OnEnemyTurnEnd()
    {
        Debug.Log("敌人回合结束，重置 Roll 次数");
        ResetRolls(); // 敌人行动后重置 Roll 次数
    }
}