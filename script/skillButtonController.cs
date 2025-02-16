using UnityEngine;
using UnityEngine.UI;
public class skillButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button buttonW;
    public Button buttonA;
    public Button buttonS;
    public Button buttonD;

    // �� Inspector ������ PlayerController �ű����ڵĶ���
    public playerController player;

    void Start()
    {
        buttonW.onClick.AddListener(() => player.MoveUp());
        buttonA.onClick.AddListener(() => player.MoveLeft());
        buttonS.onClick.AddListener(() => player.MoveDown());
        buttonD.onClick.AddListener(() => player.MoveRight());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
