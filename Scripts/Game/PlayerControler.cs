using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public KeyCode rightKey;
    public KeyCode leftKey;
    public float speed = 5f;
    private Rigidbody2D rigidbodY;
    private void Start()
    {
        rigidbodY = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(rightKey))
        {
            rigidbodY.velocity = new Vector2(speed, 0);
        }
        else if (Input.GetKey(leftKey))
        {
            rigidbodY.velocity = new Vector2(-speed, 0);
        }
        else
        {
            rigidbodY.velocity = new Vector2(0, 0);
        }


        
    }
}
