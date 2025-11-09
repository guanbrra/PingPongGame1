using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    public float moveSpeed = 20f;
    private bool isHaveBall = true;

    public float force = 5f;
    private GameObject ball;
    private Rigidbody2D rb;
    public static int life = 3;
    public static int score = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //这里不初始化的话就没法重新开始游戏了
        life = 3;
        score = 0;
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);
        //移动
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        isHaveBall = true;
        //如果有球可以发射
        if (isHaveBall && Input.GetKeyDown(KeyCode.G))
        {
            ball = GameObject.Instantiate(Resources.Load<GameObject>("ArtRes/Perfebs/Ball/Ball"));
            if (ball != null)
            {
                // 2. 设置父物体为平板（实现跟随移动）
                ball.transform.SetParent(this.transform, false);
                // 3. 计算球在平板下的局部位置（中间之上，不重叠）
                // 平板半高：this.transform.localScale.y / 2（确保在平板顶部边缘）
                // 额外偏移：0.3f（球与平板的间距，可根据球大小调整）
                float spawnYOffset = this.transform.localScale.y - 0.75f;
                // 局部位置：x=0（水平居中），y=偏移量（上方），z=0（2D场景忽略z）
                ball.transform.localPosition = new Vector2(0, spawnYOffset);

                isHaveBall = false;
            }
            else
            {
                Debug.LogError("球预制体加载失败！检查路径是否正确");
            }

        }
        if (ball != null && Input.GetKeyDown(KeyCode.Space))
        {
            //获得身上的Rigidbody2D
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ball.transform.SetParent(null, true);//脱离父物体，保持原有状态
            //发射球\
            ballRb.bodyType = RigidbodyType2D.Dynamic;
            ballRb.AddForce(new Vector2(0, force));
            //ballRb.velocity = new Vector2(0, moveSpeed);
            //ball.GetComponent<Ball>().speed = force;
            ball = null;
        }

        if (life <= 0)
        {
            print("游戏结束");
            this.gameObject.SetActive(false);
            TipsPanel tips = UIManager.Instance.ShowPanel<TipsPanel>();
            tips.ChangeInfo("游戏结束，您的得分是:" + score.ToString());
            tips.OnConfirmBack += () => {
                SceneManager.LoadScene("BeginScene");
                UIManager.Instance.ShowPanel<BeginPanel>();
                UIManager.Instance.HidePanel<GamePanel1>();

                //UIManager.Instance.HidePanel<TipsPanel>();
            };
            tips.OnConfirmSure += () => {
                SceneManager.LoadScene("BeginScene");
                UIManager.Instance.ShowPanel<BeginPanel>();
                UIManager.Instance.HidePanel<GamePanel1>();
                //UIManager.Instance.HidePanel<TipsPanel>();
            };
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ClearAllChildren();
                Debug.Log("已失活 GameManager（单例）下所有子物体");
            }
        }

    }
}
