using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    private int height = 8;
    private bool OnGround = true;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && OnGround)
        {
            rb.AddForce(Vector2.up * height, ForceMode2D.Impulse);
            rb.AddTorque(16.5f);
            OnGround= false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Floor")
        {
            OnGround = true;
            rb.AddForce(Vector2.down, ForceMode2D.Impulse);
        }
    }
}
