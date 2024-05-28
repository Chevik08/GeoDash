using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] Transform cam;
    private int height = 6;
    private int speed = 6;
    private bool OnGround = true;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && OnGround)
        {
            rb.AddForce(Vector2.up * height, ForceMode2D.Impulse);
            rb.AddTorque(-22.5f);
            OnGround= false;
        }
    }

    private void LateUpdate()
    {
        Vector2 cube = transform.position;
        Vector2 new_pos = Vector2.Lerp(cam.position, cube, 0.125f);
        cam.position = new Vector3(new_pos.x, new_pos.y, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Floor")
        {
            OnGround = true;
            rb.AddForce(Vector2.down * 2, ForceMode2D.Impulse);
        }
    }
}
