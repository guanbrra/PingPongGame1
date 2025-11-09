using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelPanel : BasePanel
{
    public ScrollRect scroll;
    private List<GameObject> levelList = new List<GameObject>();
    public Button btnBack;
    public override void Init()
    {
        // 1. 获取 "LevelItem" 文件夹路径（需在 Resources 目录下）
        string folderPath = "ArtRes/Perfebs/UI/LevelItem";
        // 2. 遍历文件夹内所有以 "btnLevelInfo" 开头的资源
        // （注意：Resources.LoadAll 返回指定路径下所有资源，需过滤名称和类型）
        GameObject[] levelButtons = Resources.LoadAll<GameObject>(folderPath)
            .Where(asset => asset.name.StartsWith("btnLevelInfo"))
            .ToArray();

        // 3. 输出结果（示例：打印按钮名称）
        foreach (GameObject btn in levelButtons)
        {
            Debug.Log("Found Button: " + btn.name);
            // 可在此处进行其他操作，如实例化按钮等
            GameObject item = Instantiate(btn);
            item.transform.SetParent(scroll.content, false);
        }
        btnBack.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<ChooseGamePanel>();
            UIManager.Instance.HidePanel<ChooseLevelPanel>();

        });
    }
}
