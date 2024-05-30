using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] private GameObject left_up;
    [SerializeField] private GameObject right_up;
    [SerializeField] private GameObject left_down;
    [SerializeField] private GameObject right_down;
    private float force = 20f;
    private float speed = 8f;
    private bool OnGround = true;
    private bool OnTouch = true;
    private PhysicsMaterial2D noFrick;
    private float rotation = 0;
    private Quaternion trotation;
    float gravity;
    Rigidbody2D rb;
    BoxCollider2D coll;
    float lastJump = 0f;
    readonly float jumpDown = 0.3f;
    readonly float jumpHeight = 2.5f;
    readonly float jumpDistance = 3f;
    bool isJumping = false;
    bool predJump = false;
    float height = 0;
    float down = 0;
    float cube_y;
    float cube_x;
    int count = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        noFrick = new();
        noFrick.friction = 0;
        noFrick.bounciness= 0;
        rb.sharedMaterial= noFrick;
        rotation = rb.rotation;
        trotation = transform.rotation;
        gravity = rb.gravityScale;

        //Quaternion rot = transform.rotation;
        //rot.z = 0;
        //transform.rotation = rot;
        rb.velocity = Vector2.zero;
    }

    bool ColliderCheck()
    {
        if (left_down.GetComponent<Circle>().GetState() && right_down.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (left_down.GetComponent<Circle>().GetState() && left_up.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (left_up.GetComponent<Circle>().GetState() && right_up.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (right_up.GetComponent<Circle>().GetState() && right_down.GetComponent<Circle>().GetState())
        {
            return true;
        }
        else if (left_down.GetComponent<Circle>().GetState())
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

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && OnGround && (Time.time - lastJump) > jumpDown)
        {
            isJumping = true;
            //rb.velocity = Vector2.zero;
            cube_y = transform.position.y;
            cube_x = transform.position.x;
            rb.velocity = Vector2.up * force;
            count = 0;
            lastJump = Time.time;
            OnGround = false;
        }
        if (!OnGround && isJumping) 
        {
            height = transform.position.y;
            if (transform.position.y >= cube_y + jumpHeight)
            {
                transform.position = new Vector2(transform.position.x, cube_y + jumpHeight);
            }
            if (transform.position.x >= cube_x + jumpDistance)
            {
                transform.position = new Vector2(cube_x + jumpDistance, transform.position.y);
            }
            rb.gravityScale = 8;
        }
        if (!OnGround && !OnTouch && isJumping)
        {
            count++;
            if (count <= 20)
            {
                transform.Rotate(Vector3.back, 180f / 20);
            }
        }
        if (OnGround && OnTouch)
        {
            isJumping= false;
            //down = transform.position.y;
            rotation = rb.rotation;

        }
        if (OnTouch)
        {
            //rb.rotation = rotation;
            //isJumping = false;
        }
    }

    private IEnumerator Backflip()
    {
        // Попытаемся сделать сальто не законами физики, а чисто скриптово
        int height = 1;
        int width = 1;
        // Поднимем куб
        float startTime = Time.time;
        float movSpeed = 30f;
        Vector2 cube = transform.localPosition;
        Vector2 jump = new Vector2(cube.x + 2, cube.y + 2);
        Vector2 center = new Vector2(cube.x + 2, cube.y);
        float Length = Vector2.Distance(cube, jump);
        float angle = 0f;
        float radius = 2f;
        while (Vector2.Distance(transform.position, jump) >= 0.5)
        {
            angle += Time.deltaTime * 0.5f;
            Vector3 new_pos = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            transform.position = Vector3.Slerp(cube, new_pos, Time.deltaTime * 5f);
            cube = transform.position;
            yield return null;
        }
        Debug.Log("Finish");
    }

    private IEnumerator TimeFlip()
    {
        float duration = 0.08f;
        float startTime = Time.time;

        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x + 1, start.y + 2, start.z);
        Vector2 center = new Vector2(transform.position.x + 1, transform.position.y + 2f);
        float rotationAngle = 90f;
        float rotationCounter = 0;
        float jumpTime = 6f;
        float angle = 0f;
        int frames = 90;
        float radius = 2f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;

            angle += 1f;
            //if (angle > frames)
            //{
                //transform.Rotate(Vector3.back, (rotationAngle - angle) * (rotationAngle / frames));
                //break;
            //}
            Vector3 new_pos = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            Debug.Log($"CENTER: {center}");
            Debug.Log($"Angle: {angle}");
            Debug.Log($"NOW: {transform.position}; NEXT: {new_pos}");
            transform.position = Vector3.Slerp(transform.position, new_pos, t);
            //transform.Rotate(Vector3.back, rotationAngle / frames);
            yield return null;
        }
        //transform.position = end;
        rb.bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("Finish");
    }

    private IEnumerator BackFlipFixed()
    {
        isJumping= true;
        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x + 1, start.y + 2, start.z);
        Vector2 center = new Vector2(transform.position.x + 1, transform.position.y + 2f);
        float rotationAngle = 90f;
        float jumpTime = 8f;
        float angle = 0f;
        int frames = 120;
        float radius = 2f;
        float Length = Vector2.Distance(transform.position, end);
        rb.bodyType = RigidbodyType2D.Kinematic;

        for (int i = 0; i < frames; i++)
        {
            // !Если в процессе прыжка что-то задел, то надо выходить!
            //if (!OnTouch && !OnGround)
            //{
                //Debug.Log("WHERE MY LAND");
                //isJumping= false;
                //rb.bodyType = RigidbodyType2D.Dynamic;
                //break;
            //}
            angle += 0.5f;
            Vector3 new_pos = center + new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            transform.position = Vector3.Slerp(transform.position, new_pos, Time.deltaTime * Length);
            // Нужно на середине повернуть куб ровно на 90 градусов
            //transform.Rotate(Vector3.back, 2f);
            yield return null;
        }
        transform.position = end;

        // Теперь вниз
        Vector3 next_start = transform.position;
        Vector3 next_end = new Vector3(next_start.x + 1, next_start.y - 2, next_start.z);
        Vector2 next_center = new Vector2(transform.position.x + 1, transform.position.y - 2f);
        float next_angle = 0f;
        float next_radius = 2f;
        for (int i = 0; i < frames; i++)
        {
            next_angle += 1f;
            Vector3 new_pos = next_center + new Vector2(Mathf.Cos(next_angle) * next_radius, Mathf.Sin(next_angle) * next_radius);
            transform.position = Vector3.Slerp(transform.position, new_pos, Length / frames);
            transform.Rotate(Vector3.back, 2f);
            yield return null;
        }
        transform.position = next_end;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isJumping= false;
        Debug.Log("Finish");
    }

    private void LateUpdate()
    {
        Vector2 cube = transform.position;
        Vector2 new_pos = Vector2.Lerp(cam.position, cube, 1f);
        cam.position = new Vector3(new_pos.x, cube.y, -10);
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.01f);
        OnGround= true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
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
