using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIController : MonoBehaviour
{
    public GameObject youWinPanel; // "You Win" 面板

    private void Start()
    {
        youWinPanel.SetActive(false); // 开始时隐藏
    }

    public void ShowYouWinUI()
    {
        youWinPanel.SetActive(true);
        Time.timeScale = 0f; // 暂停游戏
    }


    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 重新加载场景
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("退出游戏");
    }
}