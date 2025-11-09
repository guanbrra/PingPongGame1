using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnLevelInfo : MonoBehaviour
{
    private Button btn;
    //private TextMeshProUGUI txtLevel;

    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            ////设置主相机深度
            //Camera.main.depth = 1;
            //获得本体名字
            //print("名字：" + gameObject.name);
            //清除(Clone)
            string cleanName = name.Replace("(Clone)", "");
            // 使用正则表达式匹配末尾的数字
            Regex regex = new Regex(@"\d+$");
            Match match = regex.Match(cleanName);
            print("匹配到：" + $"ArtRes/Perfebs/GameLevel{match.Value}");
            //实例化地图
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>($"ArtRes/Perfebs/GameLevel/GameLevel{match.Value}"));
            obj.transform.SetParent(GameObject.Find("GameManager").transform);
            //实例化玩家
            GameObject player = GameObject.Instantiate(Resources.Load<GameObject>("ArtRes/Perfebs/Player/PlayerN"));
            player.transform.SetParent(GameObject.Find("GameManager").transform);
            Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            player.transform.position = new Vector3(0, -tempPos.y + 1, 0);

            UIManager.Instance.HidePanel<ChooseLevelPanel>();
            UIManager.Instance.ShowPanel<GamePanel1>();

        });
    }

}
