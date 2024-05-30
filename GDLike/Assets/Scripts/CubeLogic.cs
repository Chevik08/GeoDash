using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject left_up;
    [SerializeField] private GameObject right_up;
    [SerializeField] private GameObject left_down;
    [SerializeField] private GameObject right_down;

    private PhysicsMaterial2D noFrick;
    private Rigidbody2D rb;

    private float cube_y;
    private float cube_x;
    private float lastJump = 0f;
    private int count = 0;

    private bool isJumping = false;
    private bool OnGround = true;
    private bool OnTouch = true;
    private bool isFalling = false;

    private readonly float speed = 8f;
    private readonly float force = 20f;
    private readonly float jumpDown = 0.3f;
    private readonly float jumpHeight = 2.5f;
    private readonly float jumpDistance = 3.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        noFrick = new()
        {
            friction = 0,
            bounciness = 0
        };

        rb.gravityScale = 8;
        rb.sharedMaterial= noFrick;
        rb.velocity = Vector2.zero;
    }

    bool ColliderCheck()
    {
        if (left_down.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (left_up.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (right_up.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (right_down.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void FixedUpdate()
    {
        OnGround = ColliderCheck();
        Vector2 vel = rb.velocity;
        vel.x = speed;
        rb.velocity = vel;
        
        // Нажатие пробела, если игрок на земле и кулдаун прыжка не истёк
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && OnGround && (Time.time - lastJump) > jumpDown)
        {
            isJumping = true;
            // Смотрим, откуда совершается прыжок
            cube_y = transform.position.y;
            cube_x = transform.position.x;
            // Прикладываем вектор силы вертикально вверх
            rb.velocity = Vector2.up * force;
            // Счётчик кадров вращения
            Debug.Log($"Count: {count}");
            count = 0;
            lastJump = Time.time;
            OnGround = false;
        }
        // Если не на земле, но после прыжка
        if (!OnGround && isJumping && !isFalling) 
        {
            // Ставим потолок прыжка
            if (transform.position.y >= cube_y + jumpHeight)
            {
                transform.position = new Vector2(transform.position.x, cube_y + jumpHeight);
            }
            // Ставим мягкое ограничение на длину прыжка
            if (transform.position.x > cube_x + jumpDistance)
            {
                transform.position = new Vector2(cube_x + jumpDistance, transform.position.y);
            }
            // Если падаем
            if (transform.position.x >= cube_x + jumpDistance && transform.position.y < cube_y)
            {
                isFalling = true;
            }
        }
        // Если летим вниз
        if (isFalling)
        {
            transform.Rotate(Vector3.back, 180f / 20);
        }
        // Если не на земле, но после прыжка
        if (!OnGround && !OnTouch && isJumping)
        {
            count++;
            // Если мы прыгаем менее, чем на 20 кадров, то вращение контролируемое
            if (count <= 20)
            {
                transform.Rotate(Vector3.back, 180f / 20);
            }
        }
        // Если точно на земле
        if (OnGround && OnTouch)
        {
            isJumping= false;
            isFalling = false;
        }
        // Если с чем угодно соприкосновение
        if (OnTouch)
        {
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTouch = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnTouch= false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            transform.position = new Vector2(-14.5f, -3.36f);
        }
    }
}
