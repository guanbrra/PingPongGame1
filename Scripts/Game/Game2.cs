using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class Game2 : MonoBehaviour
{
    private Transform player1;
    private Transform player2;
    private Transform point1;
    private Transform point2;
    public float currentTime = 30f;
    private int showTime;
    private bool isFirstIn = true;
    public static bool isCountingDown = false;
    void Start()
    {
        InitItems();
    }

    // Update is called once per frame
    void Update()
    {
        // 减去每帧流逝的真实时间（核心：确保1秒=1秒）
        if (isCountingDown)
            currentTime -= Time.deltaTime;
        // 处理倒计时结束（防止时间为负数）
        if (currentTime <= 0 && isFirstIn)
        {
            currentTime = 0; // 强制设为0，避免UI显示负数
            //isCountingDown = false; // 关闭倒计时
            OnCountdownEnd(); // 执行倒计时结束后的逻辑（比如游戏结束、弹窗等）
            isFirstIn = false;
        }
        if (isFirstIn)
        {
            //currentTime = 0;
            // 更新UI显示（转成整数，避免显示小数）
            showTime = Mathf.CeilToInt(currentTime); // 向上取整（比如2.1秒显示3，0.5秒显示1）
            // 也可以用 Mathf.FloorToInt(currentTime) 向下取整（2.9秒显示2）
            UIManager.Instance.GetPanel<GamePanel2>().ChangeTime(showTime);
        }
        //// 更新UI显示（转成整数，避免显示小数）
        //int showTime = Mathf.CeilToInt(currentTime); // 向上取整（比如2.1秒显示3，0.5秒显示1）
        //// 也可以用 Mathf.FloorToInt(currentTime) 向下取整（2.9秒显示2）
        //UIManager.Instance.GetPanel<GamePanel2>().ChangeTime(showTime);
    }

    private void InitItems()
    {
        //GameObject player1 = GameObject.Instantiate(Resources.Load<GameObject>("ArtRes/Perfebs/Player/Player1"));
        //player1.transform.SetParent(this.transform);
        player1 = transform.Find("Player1");
        player2 = transform.Find("Player2");
        point1 = transform.Find("Point1");
        point2 = transform.Find("Point2");
        Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        player1.transform.position = new Vector3(0, -tempPos.y + 1, 0);
        player2.transform.position = new Vector3(0, tempPos.y - 1, 0);
        point1.transform.position = new Vector3(0, -tempPos.y + 0.12f, 0);
        point2.transform.position = new Vector3(0, tempPos.y - 0.13f, 0);
    }
    /// <summary>
    /// 倒计时结束后的处理逻辑
    /// </summary>
    private void OnCountdownEnd()
    {
        Debug.Log("30秒倒计时结束！");
        // 这里可以加：游戏结束、关卡失败、弹出倒计时结束弹窗等逻辑
        // 示例：UIManager.Instance.ShowPanel<GameOverPanel>();
        GameObject.Find("Ball2").SetActive(false);
        GamePanel2 gamePanel2 = UIManager.Instance.GetPanel<GamePanel2>();
        TipsPanel tipsPanel = UIManager.Instance.ShowPanel<TipsPanel>();
        tipsPanel.ChangeInfo($"游戏结束，分数 {gamePanel2.txtScore1.text} : {gamePanel2.txtScore2.text}");
        tipsPanel.OnConfirmBack += () => {
            SceneManager.LoadScene("BeginScene");
            UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<GamePanel2>();
        };
        tipsPanel.OnConfirmSure += () => {
            SceneManager.LoadScene("BeginScene");
            UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<GamePanel2>();
        };

    }
}
