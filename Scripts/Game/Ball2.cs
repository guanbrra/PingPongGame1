using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isStart = true;

    //public float force = 20f;
    //public float speed = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStart)
        {
            Game2.isCountingDown = true;
            int random = Random.Range(0, 2);
            if (random == 1)
            {
                rb.velocity = new Vector2(0, 10);
                //rb.AddForce(new Vector2(0, 100)); //, ForceMode2D.Impulse
            }
            else
            {
                rb.velocity = new Vector2(0, -10);
                //rb.AddForce(new Vector2(0, -100)); //, ForceMode2D.Impulse
            }
            isStart = false;
        }
        Vector2 velocity = rb.velocity;
        if (velocity.magnitude < 10 && velocity.magnitude != 0)
        {
            if (velocity.y > 0) 
            { 
                velocity.y = 10; 
            }
            else 
            { 
                velocity.y = -10; 
            }
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioClip clip = Resources.Load<AudioClip>("ArtRes/Audios/Click");
        AudioSource.PlayClipAtPoint(clip, transform.position);
        if (collision.collider.tag == "Player")
        {
            Vector2 velocity = rb.velocity;
            velocity.x = (velocity.x + collision.rigidbody.velocity.x) / 2;
            rb.velocity = velocity;
            //print("velocity.x = " + collision.rigidbody.velocity.x);

        }
        if (collision.gameObject.name == "Point1")
        {
            UIManager.Instance.GetPanel<GamePanel2>()?.ChangeScore1();
        }
        if (collision.gameObject.name == "Point2")
        {
            UIManager.Instance.GetPanel<GamePanel2>()?.ChangeScore2();
        }
    }
}
