using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    //public float force = 20f;
    public float speed = 0;
    [Header("手动拖拽场景中的Tilemap到这里")]
    [SerializeField] private Tilemap targetTilemap; // 优先手动赋值
    //public static int score = 0;
    //private Vector3 tileCellSize; // Tile 实际大小

    private GamePanel1 gamePanel = UIManager.Instance.ShowPanel<GamePanel1>();

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        // 获取场景中目标Tilemap（需提前给Tilemap命名或通过标签查找）
        // 容错：如果没手动赋值，自动查找（仅作备份，建议还是手动拖）
        //if (targetTilemap == null)
        //{
        //    targetTilemap = FindObjectOfType<Tilemap>();

        //    if (targetTilemap == null)
        //    {
        //        Debug.LogError("场景中没有找到Tilemap！");
        //    }
        //}
        // 优先查找名为 "tilemap1" 的 Tilemap
        if (targetTilemap == null)
        {
            GameObject tilemapObj = GameObject.Find("tilemap1");
            if (tilemapObj != null)
            {
                targetTilemap = tilemapObj.GetComponent<Tilemap>();
            }

            if (targetTilemap == null)
            {
                Debug.LogError("场景中没有找到名为 'tilemap1' 的 Tilemap！");
            }
        }
        //speed = rb.velocity.y;

    }

    // Update is called once per frame
    void Update()
    {
        //if (speed > 0)
        //{
        //    //AddSpeed(speed);
        //    //this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        //    this.rb.velocity.magnitude = speed;
        //}
        gamePanel.ChangeInfo($"LIfe:{Player.life}  Score:{Player.score}");
        //Vector2 velocity = rb.velocity;
        //if (velocity.magnitude < 10 && velocity.magnitude != 0)
        //{
        //    if (velocity.y > 0)
        //    {
        //        velocity.y = 10;
        //    }
        //    else
        //    {
        //        velocity.y = -10;
        //    }
        //    rb.velocity = velocity;
        //}
    }

    public void AddForce1(Vector2 force)
    {
        rb.AddForce(force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //{
        //    Vector2 velocity = rb.velocity;
        //    velocity.x = (velocity.x + collision.rigidbody.velocity.x) / 2;
        //    rb.velocity = velocity;
        //    print("velocity.x = " + velocity.x);
        //}
        //if (collision.collider.tag == "Player")
        //{
        //    Vector2 velocity = rb.velocity;
        //    velocity.x = (velocity.x + collision.rigidbody.velocity.x) / 2;
        //    rb.velocity = velocity;
        //    print("velocity.x = " + collision.rigidbody.velocity.x);
        //}
        AudioClip clip = Resources.Load<AudioClip>("ArtRes/Audios/Click");
        AudioSource.PlayClipAtPoint(clip, transform.position);

        // 检查是否与TileMap碰撞
        if (collision.collider.GetComponent<TilemapCollider2D>() != null)
        {
            // 获取碰撞点（取第一个接触点）
            ContactPoint2D contact = collision.contacts[0];
            Vector3 hitPosition = contact.point;

            // 将世界坐标转换为TileMap坐标
            Vector3Int cellPosition = targetTilemap.WorldToCell(hitPosition);

            // 在 3x3 区域内搜索有效瓦片（解决浮点/边缘问题）
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    Vector3Int cell = cellPosition + new Vector3Int(dx, dy, 0);

                    if (targetTilemap.HasTile(cell))
                    {
                        TileBase hitTile = targetTilemap.GetTile(cell);
                        //Debug.Log($"击中瓦片: {hitTile.name} @ ({cell.x}, {cell.y})");

                        targetTilemap.SetTile(cell, null);
                        Player.score++;
                        //gamePanel.ChangeInfo($"LIfe:{Player.life}  Score:{Player.score}");

                        // 显示 UI 消息...
                        //Debug.Log($"成功获取瓦片: {hitTile.name}");
                        return; // 只处理一个瓦片
                    }
                }
            }
        }

        GameObject hitObject = collision.collider.gameObject;
        if (hitObject.name == "BottomWall")
        {
            // 扣除生命

            Player.life--;
        }
    }

    //// 核心方法：维持速度大小恒定
    //private void MaintainConstantSpeed()
    //{
    //    // 1. 排除速度接近0的情况（避免归一化出错）
    //    if (rb.velocity.magnitude < 0.01f)
    //    {
    //        Debug.LogWarning("小球速度接近0，无法维持恒定速度！");
    //        return;
    //    }

    //    // 2. 保留方向（归一化），乘以目标速度大小 → 新速度
    //    Vector3 newVelocity = rb.velocity.normalized * speed;

    //    // 3. 应用新速度到Rigidbody
    //    rb.velocity = newVelocity;
    //}
    //// 碰撞发生时调用（每次碰撞只触发一次）
    //void OnCollisionEnter(Collision other)
    //{
    //    // 关键：保持方向，重置速度大小
    //    MaintainConstantSpeed();
    //}

    //// （可选）若碰撞后有持续接触（如沿斜面滑动），补充此回调
    //void OnCollisionStay(Collision other)
    //{
    //    // 每隔固定帧调用一次，避免每帧频繁修改（优化性能）
    //    if (Time.frameCount % 2 == 0)
    //    {
    //        MaintainConstantSpeed();
    //    }
    //}
}
