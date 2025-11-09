using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BoxCollider2D rightWall;
    private BoxCollider2D leftWall;
    private BoxCollider2D topWall;
    private BoxCollider2D bottomWall;
    private Transform palyer;

    private static GameManager instance;
    public static GameManager Instance => instance;
    private void Awake()
    {
        /// 校验：确保全局唯一（避免场景中多个 GameManager 对象）
        if (instance == null)
        {
            // 给 instance 赋值为当前脚本实例
            instance = this;
            // 可选：切换场景时不销毁 GameManager（根据需求决定是否启用）
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 若已有实例，销毁当前重复的对象（保证唯一）
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetWall();
        //InitPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ResetWall()
    {
        rightWall = transform.Find("RightWall").GetComponent<BoxCollider2D>();
        leftWall = transform.Find("LeftWall").GetComponent<BoxCollider2D>();
        topWall = transform.Find("TopWall").GetComponent<BoxCollider2D>();
        bottomWall = transform.Find("BottomWall").GetComponent<BoxCollider2D>();//x = Screen.width / 2, y = Screen.height

        //设置墙面位置
        //Vector3 topWallPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height));
        //topWall.transform.position = topWallPos + new Vector3(0, 0.5f, 0);
        //Vector3 bottomWallPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0));
        //bottomWall.transform.position = bottomWallPos - new Vector3(0, 0.5f, 0);
        //Vector3 rightWallPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2));
        //rightWall.transform.position = rightWallPos + new Vector3(0.5f, 0, 0);
        //Vector3 leftWallPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2));
        //leftWall.transform.position = leftWallPos - new Vector3(0.5f, 0, 0);
        Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        rightWall.transform.position = new Vector3(-tempPos.x - 0.5f, 0, 0);
        leftWall.transform.position = new Vector3(tempPos.x + 0.5f, 0, 0);
        topWall.transform.position = new Vector3(0, tempPos.y + 0.5f, 0);
        bottomWall.transform.position = new Vector3(0, -tempPos.y - 0.5f, 0);


        //得到屏幕的宽
        float width = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x *2;
        topWall.size = new Vector2(width, 1);
        bottomWall.size = new Vector2(width, 1);
        float height = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y * 2;
        rightWall.size = new Vector2(1, height);
        leftWall.size = new Vector2(1, height);


    }
    // 新增：失活所有子物体
    public void ClearAllChildren()
    {
        // 从后往前销毁，避免索引变化
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;

            // 可选：只销毁特定类型的子物体
            if (child.name == "RightWall" ||
                child.name == "LeftWall" ||
                child.name == "TopWall" ||
                child.name == "BottomWall")
            {
                //Destroy(child);
                //失活子物体
                //child.SetActive(false);
                continue;
            
            }
            else
                child.SetActive(false);
            // 如果需要销毁所有子物体，直接使用：Destroy(child);
        }
    }
}
