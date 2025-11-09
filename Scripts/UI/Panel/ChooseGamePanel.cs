using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseGamePanel : BasePanel
{
    public Button btnSingle;
    public Button btnDouble;
    public Button btnBack;

    public override void Init()
    {
        btnSingle.onClick.AddListener(() =>
        {
            //print("开始游戏");
            //SceneManager.LoadScene("GameScene");
            UIManager.Instance.HidePanel<ChooseGamePanel>();
            UIManager.Instance.ShowPanel<BGPanel>();
            UIManager.Instance.ShowPanel<ChooseLevelPanel>();
        });

        btnDouble.onClick.AddListener(() =>
        {
            //print("开始游戏");
            //SceneManager.LoadScene("GameScene");
            UIManager.Instance.HidePanel<ChooseGamePanel>();
            UIManager.Instance.ShowPanel<BGPanel>();
            UIManager.Instance.ShowPanel<GamePanel2>();
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("ArtRes/Perfebs/Game2"));
            print(obj.name);
        });
        btnBack.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BeginScene");
            UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<ChooseGamePanel>();

        });
    }



}
