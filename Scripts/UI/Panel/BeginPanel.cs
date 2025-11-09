using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnBegin;
    public Button btnEnd;
    public override void Init()
    {
        btnBegin.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GameScene");
            UIManager.Instance.HidePanel<BeginPanel>();
            UIManager.Instance.ShowPanel<BGPanel>();
            UIManager.Instance.ShowPanel<ChooseGamePanel>();

        });
        btnEnd.onClick.AddListener(() =>
        {
            //UIManager.Instance.ShowPanel<TipsPanel>().ChangeInfo("确定退出游戏？");
            TipsPanel tipsPanel = UIManager.Instance.ShowPanel<TipsPanel>();
            tipsPanel.ChangeInfo("确定退出游戏？");

            tipsPanel.OnConfirmSure = () =>
            {
                print("退出游戏");
                Application.Quit();
            };
            
        });
    }


}
