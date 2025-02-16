using UnityEngine;
using UnityEngine.UI;
public class EndTurn : MonoBehaviour
{
    public Button buttonW;
    public GameManager gm;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonW.onClick.AddListener(() => gm.EndTurn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
