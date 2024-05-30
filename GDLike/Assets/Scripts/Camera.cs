using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject cubeObj;
    private bool work = false;

    public void StartWork()
    {
        work = true;
    }

    public void StopWork()
    {
        work = false; 
    }
    void Update()
    {
        if (work)
        {
            Vector2 cube = cubeObj.transform.position; ;
            Vector2 new_pos = Vector2.Lerp(cam.position, cube, 1f);
            cam.position = new Vector3(new_pos.x, cube.y, -10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        work = true;
    }
}
