using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private GameObject left_up;
    [SerializeField] private GameObject right_up;
    [SerializeField] private GameObject left_down;
    [SerializeField] private GameObject right_down;
    public UnityEvent myEvent;
    public UnityEvent camEvent;
    public UnityEvent pauseEvent;
    public UnityEvent continueEvent;

    private PhysicsMaterial2D noFrick;
    private Rigidbody2D rb;

    private float cube_y;
    private float cube_x;
    private float lastJump = 0f;
    private int count = 0;
    private float speed = 8f;
    private float lastDeath = 0f;

    private bool isJumping = false;
    private bool OnGround = true;
    private bool OnTouch = true;
    private bool isFalling = false;
    private bool control = false;
    private bool isPaused = false;

    private readonly float force = 20f;
    private readonly float jumpDown = 0.3f;
    private readonly float jumpHeight = 2.5f;
    private readonly float jumpDistance = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        noFrick = new()
        {
            friction = 0f,
            bounciness = 0f
        };

        rb.gravityScale = 8f;
        rb.sharedMaterial= noFrick;
        rb.velocity = Vector2.zero;
        lastDeath = Time.time;
    }

    public void Control(bool mode)
    {
        control = mode;
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

    private void Jump(float force)
    {
        isJumping = true;
        // Смотрим, откуда совершается прыжок
        cube_y = transform.position.y;
        cube_x = transform.position.x;
        // Прикладываем вектор силы вертикально вверх
        rb.velocity = Vector2.up * force;
        // Счётчик кадров вращения
        count = 0;
        lastJump = Time.time;
        OnGround = false;
    }

    public void ForceContinue()
    {
        isPaused= false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                continueEvent.Invoke();
                isPaused = false;
            }
            else
            {
                pauseEvent.Invoke();
                isPaused= true;
            }
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
            Jump(force);
        }
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) && OnGround && (Time.time - lastJump) > jumpDown)
        {
            Jump(force);
        }
        // Если не на земле, но после прыжка
        if (!OnGround && isJumping && !isFalling) 
        {
            // Ставим потолок прыжка
            if (transform.position.y >= cube_y + jumpHeight)
            {
                //transform.position = new Vector2(transform.position.x, cube_y + jumpHeight);
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
            //rb.AddForce(Vector3.down * 2f, ForceMode2D.Force);
            isJumping= false;
            isFalling = false;
        }
        // Если с чем угодно соприкосновение
        if (OnTouch)
        {
        }
        if (transform.position.y < -24 | transform.position.x > 312 | transform.position.x < -20)
        {
            camEvent.Invoke();
            myEvent.Invoke();
        }
    }

    // Функция довращения кубика
    private void DoRotate()
    {
        // FIXME: Когда-нибудь
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            OnTouch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            OnTouch = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            if (Time.time - lastDeath > 0.1)
            {
                lastDeath = Time.time;
                myEvent.Invoke();
            }
        }
        if (collision.gameObject.CompareTag("Jumper"))
        {
            Jump(force * 2f);
        }
        if (collision.gameObject.CompareTag("Booster"))
        {
            StartCoroutine(CoolDown(1, 3));
            speed += 3f;
        }
        if (collision.gameObject.CompareTag("Reverse"))
        {
            StartCoroutine(Reverse(collision, 0.5f));
        }
        if (collision.gameObject.CompareTag("finish"))
        {
            Debug.Log("WIN");
            speed = 8f;
            myEvent.Invoke();
        }
    }

    private IEnumerator CoolDown(float time, float minus)
    {
        yield return new WaitForSeconds(time);
        speed -= minus;

    }

    private IEnumerator Reverse(Collider2D collision, float time)
    {
        yield return new WaitForSeconds(time);
        //collision.gameObject.SetActive(false);
        speed = -speed;

    }
}
