using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] GameObject left_up;
    [SerializeField] GameObject right_up;
    [SerializeField] GameObject left_down;
    [SerializeField] GameObject right_down;
    private float force = 10f;
    private int speed = 8;
    private bool OnGround = true;
    private bool OnTouch = true;
    private PhysicsMaterial2D noFrick;
    private float rotation = 0;
    private Quaternion trotation;
    float gravity;
    Rigidbody2D rb;
    BoxCollider2D coll;
    float lastJump = 0f;
    readonly float jumpDown = 0.4f;

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
        else
        {
            return false;
        }
    }

    void Update()
    {
        OnGround = ColliderCheck();
        Vector2 vel = rb.velocity;
        vel.x = speed;
        rb.velocity = vel;

        //transform.position += speed * Time.deltaTime * Vector3.right;   
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && OnTouch && (Time.time - lastJump) >= jumpDown)
        {
            Quaternion rot = transform.rotation;
            rot.z = Mathf.Round(rot.z / 90) * 90;
            transform.rotation = rot;
            rb.velocity = Vector3.zero;

            lastJump = Time.time;
            rb.AddForce(Vector2.up * force * 2f, ForceMode2D.Impulse);
            OnGround = false;
        }
        if (!OnGround) 
        {
            transform.Rotate(-Vector3.back, 1f);
            rb.gravityScale =  10;
        }
        if (OnGround && OnTouch)
        {
            // Пока кубик на земле, применять к нему силу
            //rb.AddForce(Vector2.down * 1000, ForceMode2D.Force);
        }
        if (OnTouch)
        {
            rb.rotation = rotation;
            transform.rotation = trotation;
            rb.gravityScale = gravity;
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
        Vector2 jump = new Vector2(cube.x + 3, cube.y + 3);
        float Length = Vector2.Distance(cube, jump);
        while (Vector2.Distance(cube, jump) > 0.01)
        {
            transform.localPosition = Vector3.Slerp(cube, jump, 0.05f);
            cube = transform.localPosition;
            yield return null;
        }
        Debug.Log("Finish");
    }

    private void LateUpdate()
    {
        Vector2 cube = transform.position;
        Vector2 new_pos = Vector2.Lerp(cam.position, cube, 1f);
        cam.position = new Vector3(new_pos.x, cam.position.y, -10);
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.01f);
        OnGround= true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTouch = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnTouch= false;
    }
}
